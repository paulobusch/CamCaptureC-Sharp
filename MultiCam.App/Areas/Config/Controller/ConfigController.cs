using System;
using System.Linq;
using System.Windows.Forms;
using Dapper;

using MultiCam.Config.Model.Dtos;
using MultiCam.Config.View;
using MultiCam.Model;
using MultiCam.Model.Dtos;
using MultiCam.Model.Enums;
using MultiCam.Notify.Controller;
using MultiCam.DataContext;
using MultiCam.Config.Repository;
using System.Collections.Generic;

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
        Configuration Load();

        /// <summary>
        /// Save Configuration application
        /// </summary>
        /// <param name="entity">Instance Configuration</param>
        void Save(Configuration entity);
    }

    /// <summary>
    /// Controller configuration global application
    /// </summary>
    public class ConfigController : IConfigController
    {
        private IConfigRepository _configRepository;
        private INotifyController _notify;
        private IConfigView _view;
        
        /// <summary>
        /// Contructor Configuration
        /// </summary>
        public ConfigController(
            INotifyController notify,
            IConfigRepository configRepository
        )
        {
            _notify = notify;
            _configRepository = configRepository;
        }
        public Form Run() => new ConfigurationForm(this);
        public void SetView(IConfigView view) => _view = view;
        public Configuration Load()
        {
            try
            {
                return _configRepository.GetAll().FirstOrDefault();
            } catch (Exception){
                _notify.FailMessage("Falha ao carregar as configurações!");
            }
            return new Configuration();
        }
        public void Save(Configuration entity)
        {
            try
            {
                _configRepository.Update(entity);
            }
            catch (Exception ex){
                _notify.FailMessage("Falha ao salvar configurações!");
            }
        }
    }
}
