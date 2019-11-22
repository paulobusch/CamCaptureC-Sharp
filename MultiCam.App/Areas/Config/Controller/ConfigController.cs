using System;
using System.Linq;
using System.Windows.Forms;

using MultiCam.Config.View;
using MultiCam.Notify.Controller;
using MultiCam.Domain.Entities;
using MultiCam.Domain.Repository;
using System.Threading.Tasks;

namespace MultiCam.Config.Controller
{

    /// <summary>
    /// Configuration interface by inject dependency
    /// </summary>
    public interface IConfigController
    {
        Form Run();

        /// <summary>
        /// Define view
        /// </summary>
        /// <param name="view">View by controll</param>
        void SetView(IConfigView view);

        /// <summary>
        /// Restore configuration by application
        /// </summary>
        /// <returns>The method return an instance ConfigurationDto</returns>
        Task<Configuration> LoadAsync();

        /// <summary>
        /// Save Configuration application
        /// </summary>
        /// <param name="entity">Instance Configuration</param>
        Task Save(Configuration entity);
    }

    /// <summary>
    /// Controller configuration global application
    /// </summary>
    public class ConfigController : IConfigController
    {
        private IRepository<Configuration> _configRepository;
        private INotifyController _notify;
        private IConfigView _view;
        
        /// <summary>
        /// Contructor Configuration
        /// </summary>
        public ConfigController(
            INotifyController notify,
            IRepository<Configuration> configRepository
        )
        {
            _notify = notify;
            _configRepository = configRepository;
        }
        public Form Run() => new ConfigurationForm(this);
        public void SetView(IConfigView view) => _view = view;
        public async Task<Configuration> LoadAsync()
        {
            try
            {
                var list = await _configRepository.GetAllAsync();
                return list.FirstOrDefault();
            } catch (Exception){
                _notify.FailMessage("Falha ao carregar as configurações!");
            }
            return new Configuration();
        }
        public async Task Save(Configuration entity)
        {
            try
            {
                await _configRepository.UpdateAsync(entity);
            }
            catch (Exception ex){
                _notify.FailMessage("Falha ao salvar configurações!");
            }
        }
    }
}
