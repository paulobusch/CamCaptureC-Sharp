using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Reflection;

namespace CapturaVideo.Model
{
    class SqLite
    {        
        private string _connectionString;
        private SQLiteConnection _connection;
        public SqLite()
        {
            _connectionString = $@"Data Source={Consts.DATA_PATH}\{Consts.NAME_FILE_DATA}; Version=3;";
            _connection = new SQLiteConnection(_connectionString);
        }
        public void Open()
        {
            _connection?.Open();
        }
        public void CreateTablesIfNotExists()
        {
            Open();

            foreach (KeyValuePair<string, Dictionary<string, Type>> table in Consts.TABLES_MAPPING)
            {
                if (!TableExists(table.Key))
                {
                    //prepare props
                    var _props = new List<string>();
                    foreach (var param in table.Value)
                        _props.Add($"{param.Key} {Consts.DB_TYPES_TEXT[param.Value]}");

                    //create query
                    var _query = $"create table {table.Key}({string.Join(",", _props.ToArray())})";

                    //execute
                    using (var _command = new SQLiteCommand(_query, _connection))
                        _command.ExecuteNonQuery();
                }
            }

            Close();
        }
        public DataTableSqLite Select(string table_name, string alias, string condition = null, params string[] columns)
        {
            //check connection
            if (_connection.State == ConnectionState.Closed)
                return null;

            //check properties
            if (string.IsNullOrEmpty(table_name) || string.IsNullOrEmpty(alias))
                return null;

            //vars
            var _query = string.Empty;
            var _columns = string.Empty;
            var _data = new DataTableSqLite(table_name);

            //prepare query
            if (columns.Count() == 0)
                columns = Consts.TABLES_MAPPING[table_name].Keys.ToArray();
            _columns = string.Join($",", columns.Select(col => { return $"{alias}.{col}"; }).ToArray());
            _query = $"select {_columns} from {table_name} {alias} {condition}";

            //execute
            var _adapter = new SQLiteDataAdapter(_query, _connection);
            _adapter.Fill(_data.Table);

            return _data;
        }
        public bool Update(SqlLiteData values, SqlLiteData condition = null) {
            if (string.IsNullOrEmpty(values.TableName))
                return false;

            //check connection
            if (_connection.State == ConnectionState.Closed)
                return false;

            //insert commands
            var _values = new List<string>();
            var _condition = new List<string>();
            using (var _command = new SQLiteCommand(_connection))
            {
                //values
                foreach (var param in values.DataTypes)
                {
                    _values.Add($"{param.Key}=@{param.Key}");
                    _command.Parameters.Add($"@{param.Key}", param.Value.Key).Value = param.Value.Value;
                }

                //conditions
                if(condition?.DataTypes.Count > 0)
                    foreach (var param in condition?.DataTypes)
                    {
                        _condition.Add($"{param.Key}=@{param.Key}");
                        _command.Parameters.Add($"@{param.Key}", param.Value.Key).Value = param.Value.Value;
                    }

                //update table
                _command.CommandText = $"" +
                    $"update {values.TableName} set {string.Join(",", _values.ToArray())} " +
                    (_condition.Count > 0 ? 
                    $"where {string.Join(" and ", _condition.ToArray())}" : "");
                _command.ExecuteNonQuery();
            }

            return true;
        }
        public bool Save(SqlLiteData data)
        {
            if (string.IsNullOrEmpty(data.TableName))
                return false;

            //check connection
            if (_connection.State == ConnectionState.Closed)
                return false;

            //insert commands
            var _values = new Dictionary<string,string>();
            using (var _command = new SQLiteCommand(_connection)) {
                //insert params
                foreach (var param in data.DataTypes){
                    _values.Add(param.Key, $"@{param.Key}");
                    _command.Parameters.Add($"@{param.Key}", param.Value.Key).Value = param.Value.Value;
                }
                _command.CommandText = $"" +
                    $"insert into {data.TableName}({string.Join(",", _values.Keys.ToArray())}) " +
                    $"values({string.Join(",", _values.Values.ToArray())})";
                _command.ExecuteNonQuery();
            }

            return true;
        }
        //public T Restore<T>(SqlLiteData data)
        //{
        //    //var t = data.GetType().ToString().ToLower();
        //    return (T)1;
        //}
        private bool TableExists(string tableName)
        {
            var _query = $"select count(*) from sqlite_master where type='table' and name='{tableName}'";
            using (var _command = new SQLiteCommand(_query, _connection))
                return Convert.ToInt32(_command.ExecuteScalar()) > 0;
        }
        public void Close()
        {
            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
    }
    class SqlLiteData
    {
        internal string TableName { get; set; }

        //property <name - sql type - value>
        internal Dictionary<string, KeyValuePair<DbType, object>> DataTypes
        {
            get { return _data_types?.Count > 0 ? _data_types : MapProperties(); }
        }

        private object _data;
        private Dictionary<string, KeyValuePair<DbType, object>> _data_types;
        public SqlLiteData(object Obj, string TableName = null)
        {
            if (Obj == null)
                return;

            //init values
            _data = Obj;
            _data_types = new Dictionary<string, KeyValuePair<DbType, object>>();

            //update properties
            var _type_name = _data.GetType().ToString();
            this.TableName = (TableName ?? (!_type_name.Contains("AnonymousType") ? _type_name : null))?.ToLower().Replace(" ", "_");
        }
        private Dictionary<string, KeyValuePair<DbType, object>> MapProperties()
        {
            if (_data == null)
                return null;

            var tp = _data.GetType();
            foreach (PropertyInfo prop in tp.GetProperties()) {
                var value = prop.GetValue(_data, null);
                var tipo = value.GetType();
                _data_types.Add(prop.Name, new KeyValuePair<DbType, object>( Consts.DB_TYPES[value.GetType()], value ));
            }
            return _data_types;
        }
    }
}
