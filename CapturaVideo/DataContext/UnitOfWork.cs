using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiCam.Config.Repository;
using MultiCam.Repository;

namespace MultiCam.DataContext
{
    public class UnitOfWork : IUnitOfWork
    {
        private IContextDb _context;
        private DeviceRepository _deviceRepository;
        private ConfigRepository _configRepository;
        
        public UnitOfWork(IContextDb context) { 
            this._context = context;
        }
        public DeviceRepository DeviceRepository {
            get 
            { 
                if(this._deviceRepository == null)
                    this._deviceRepository = new DeviceRepository(_context);
                return this._deviceRepository;
            }    
        }

        public ConfigRepository ConfigRepository {
            get {
                if (this._configRepository == null)
                    this._configRepository = new ConfigRepository(_context);
                return this._configRepository;
            }
        }

        public void Commit()
        {
            //this._context.SaveChanges();
        }
    }
}
