using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

using MultiCam.Domain.DataContext;
using MultiCam.Domain.Entities;

namespace MultiCam.Domain.Repository
{
    public class DeviceRepository : IRepository<Device>
    {
        private readonly IContextDb _context;

        public DeviceRepository(IContextDb context) { 
            _context = context;
        }

        public async Task InsertAsync(Device entity)
        {
            var sqlInsert = @"
                insert into devices(moniker_string,cod_nome,width,height)
                values(@MonikerString,@CodNome,@Width,@Height);";

            var sqlIdent = @"select last_insert_rowid();";

            using (var cnn = _context.NewConnection())
            {
                cnn.Open();
                await cnn.ExecuteAsync(sqlInsert, Bind(entity));
                entity.Id = cnn.Query<int>(sqlIdent, null).First();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var sql = @"delete from devices where id=@Id";

            using (var cnn = _context.NewConnection())
            {
                cnn.Open();
                await cnn.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Device>> FindAsync(Func<Device, bool> predicate)
        {
            var list = await GetAllAsync();
            return list.Where(predicate);
        }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            var sql = @"
                select
                    device.id as Id,
                    device.moniker_string as MonikerString,
                    device.cod_nome as CodNome,
                    device.width as _width,
                    device.height as _height
                from devices device";
            
            using (var cnn = _context.NewConnection())
            {
                cnn.Open();
                return await cnn.QueryAsync<Device>(sql, null);
            }
        }

        public async Task<Device> GetByIdAsync(int id)
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

            using (var cnn = _context.NewConnection())
            {
                cnn.Open();
                return await cnn.QueryFirstAsync<Device>(sql, new { Id = id });
            }
        }

        public async Task UpdateAsync(Device entity)
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
                await cnn.ExecuteAsync(sql, Bind(entity));
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
