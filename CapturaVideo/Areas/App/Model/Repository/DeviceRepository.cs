using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;

using MultiCam.DataContext;
using MultiCam.Model.Entities;

namespace MultiCam.Repository
{
    public interface IDeviceRepository : IRepository<Device>
    {

    }
    public class DeviceRepository : IDeviceRepository
    {
        private readonly IContextDb _context;

        public DeviceRepository(IContextDb context) { 
            _context = context;
        }

        public void Insert(Device entity)
        {
            var sqlInsert = @"
                insert into devices(moniker_string,cod_nome,width,height)
                values(@MonikerString,@CodNome,@Width,@Height);";

            var sqlIdent = @"select last_insert_rowid();";

            using (var cnn = _context.NewConnection())
            {
                cnn.Open();
                cnn.Execute(sqlInsert, Bind(entity));
                entity.Id = cnn.Query<int>(sqlIdent, null).First();
            }
        }

        public void Delete(int id)
        {
            var sql = @"delete from devices where id=@Id";

            using (var cnn = _context.NewConnection())
            {
                cnn.Open();
                cnn.Execute(sql, new { Id = id });
            }
        }

        public IEnumerable<Device> Find(Func<Device, bool> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IEnumerable<Device> GetAll()
        {
            var sql = @"
                select
                    device.id as Id,
                    device.moniker_string as MonikerString,
                    device.cod_nome as CodNome,
                    device.width as _width,
                    device.height as _height
                from devices device";
            
            var data = null as IEnumerable<Device>;
            using (var cnn = _context.NewConnection())
            {
                cnn.Open();
                data = cnn.Query<Device>(sql, null);
            }

            return data;
        }

        public Device GetById(int id)
        {
            var sql = @"
                select
                    device.id as Id,
                    device.moniker_string as MonikerString,
                    device.cod_nome as CodNome,
                    device.width as _width,
                    device.height as _height
                from devices device
                where device.id=@Id";

            var data = null as Device;
            using (var cnn = _context.NewConnection())
            {
                cnn.Open();
                data = cnn.Query<Device>(sql, new { Id = id }).FirstOrDefault();
            }

            return data;
        }

        public void Update(Device entity)
        {
            var sql = @"
                update devices set
                    moniker_string=@MonikerString,
                    cod_nome=@CodNome,
                    width=@Width,
                    height=@Height
                where devices.id=@Id";

            using (var cnn = _context.NewConnection())
            {
                cnn.Open();
                cnn.Execute(sql, Bind(entity));
            }
        }

        private object Bind(Device entity) { 
            return new
            {
                entity.Id,
                entity.MonikerString,
                entity.CodNome,
                entity.Size.Width,
                entity.Size.Height
            };
        }
    }
}
