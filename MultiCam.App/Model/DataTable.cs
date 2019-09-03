using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CapturaVideo.Model
{
    class PrimaryKey{ public Int32 Key { get; set; } }

    class ForeignKey{ public Int32 Key { get; set; } }

    class DataTableSqLite
    {
        public DataTable Table;

        private string TableName;
        public DataTableSqLite(string tableName)
        {
            TableName = tableName;
            Table = new DataTable();
        }
        public void Apply(Type classRef)
        {
            if (Table.Rows.Count > 0)
            {
                var row = Table.Rows[0];
                var fields = classRef.GetFields(BindingFlags.Static | BindingFlags.Public).ToList();
                foreach (DataColumn col in Table.Columns)
                {
                    var tryParseMethod = typeof(int).GetMethod("Parse");
                    var a = int.Parse(row[col]);
                    TypeCode

                    var parameters = new object[] { "1", null };
                    var success = (bool)tryParseMethod.Invoke(null, parameters);
                    var result = (int)parameters[1];
                    //cast and set value
                    var tp = Consts.TABLES_MAPPING[TableName][col.ColumnName];
                    var t = tp.GetMethod("Parse").Invoke(null, new[] { row[col] });
                    fields.Find(f => f.Name == col.ColumnName)?
                        .SetValue(classRef, t);                    //Convert.ChangeType(row[col],
                    //Consts.TABLES_MAPPING[TableName][col.ColumnName])
                }
            }
        }
    }

    class Devices {
        public Dictionary<string, System.Drawing.Size> List { get; set; }
    }
}
