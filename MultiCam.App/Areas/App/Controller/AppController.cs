using System;
using System.Linq;
using DirectX.Capture;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing.Imaging;

using MultiCam.View;
using MultiCam.Model;
using MultiCam.Model.Enums;

using MultiCam.Server.Controller;
using MultiCam.Config.Controller;
using MultiCam.Notify.Controller;
using MultiCam.Grid.Controller;
using MultiCam.Info.Controller;

using MultiCam.Domain.Entities;
using MultiCam.Domain.Repository;
using MultiCam.Domain;
using System.Threading.Tasks;

namespace MultiCam.Controller
{
    /// <summary>
    /// Interface controller by inject dependecies
    /// </summary>
    public interface IAppController
    {
        Form Run();

        /// <summary>Current device</summary>
        int CurrentVideoCapture { get; set; }

        /// <summary>Configuration data</summary>
        Configuration Config { get; set; }

        /// <summary>
        /// Update config defined
        /// </summary>
        Task ApplyContext();

        /// <summary>
        /// Save context config
        /// </summary>
        Task SaveConfig();

        /// <summary>
        /// Define view
        /// </summary>
        /// <param name="view">View by controll</param>
        Task SetView(IMultipleCaptureView view);

        /// <summary>
        /// Update device
        /// </summary>
        /// <param name="videoCapture">Instance to update</param>
        Task SaveDevice(VideoCapture videoCapture);

        /// <summary>
        /// Create new device
        /// </summary>
        /// <param name="videoCapture">Device by create</param>
        Task NewDevice(VideoCapture videoCapture);

        /// <summary>
        /// Delete by key id
        /// </summary>
        /// <param name="id">Device key</param>
        Task RemoveDevice(int id);

        /// <summary>
        /// Run device by key
        /// </summary>
        /// <param name="id">Device key</param>
        void StartDevice(int id);

        /// <summary>
        /// Stop device by key
        /// </summary>
        /// <param name="id">Device key</param>
        void StopDevice(int id);

        /// <summary>
        /// Run video by key
        /// </summary>
        /// <param name="id">Device key</param>
        void StartVideo(int id);
        
        /// <summary>
        /// Stop video by key
        /// </summary>
        /// <param name="id">Device key</param>
        void StopVideo(int id);

        /// <summary>
        /// Trim inner video by key
        /// </summary>
        void ShareVideos();

       /// <summary>
       /// Get device by id
       /// </summary>
       /// <param name="id">identifier</param>
        void SelectDevice(int id);

        /// <summary>
        /// Modify state by update into interface
        /// </summary>
        /// <param name="deviceState"></param>
        /// <param name="id"></param>
        void UpdateStateDevice(EDeviceState deviceState, int id);

        /// <summary>
        /// Get current frame and open folder exprlorer by save image
        /// </summary>
        /// <param name="id">identifier</param>
        void SaveImage(int id);

        /// <summary>
        /// Get name devices
        /// </summary>
        /// <returns>key and name device</returns>
        IDictionary<int, string> GetNames();

        /// <summary>
        /// Get images devices
        /// </summary>
        /// <returns>key and x64 encoded string image</returns>
        IDictionary<int, string> GetImages();

        /// <summary>
        /// Get device by identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns></returns>
        VideoCapture GetVideoCapture(int id);

        /// <summary>
        /// View property page
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="elm">control</param>
        void ShowPropertyPage(int id, Control elm);

        /// <summary>
        /// Get current device and return state
        /// </summary>
        /// <returns>EDeviceState enum</returns>
        EDeviceState GetStateCurrentDevice();

        /// <summary>
        /// Get devices by use
        /// </summary>
        /// <returns>instance of Filter</returns>
        IEnumerable<Filter> GetDevices();

        /// <summary>
        /// End application
        /// </summary>
        void Exit();

        /// <summary>
        /// Run About dialog
        /// </summary>
        void ShowAboutDialog();

        /// <summary>
        /// Run config dialog
        /// </summary>
        void ShowConfigDialog();

        /// <summary>
        /// Run config dialog
        /// </summary>
        void ShowGridDialog();
    }

    /// <summary>
    /// Control all aplication running
    /// </summary>
    public class AppController : IAppController
    {
        public Configuration Config { get; set; }
        public int CurrentVideoCapture { get; set; }

        private readonly INotifyController _notify;
        private readonly IConfigController _config;
        private readonly IAboutController _about;
        private readonly IGridController _grid;

        private readonly IRepository<Device> _deviceRepository;

        private IEnumerable<Filter> _devices;
        private IEnumerable<VideoCapture> _videosCapture;
        private IMultipleCaptureView _view;

        /// <summary>
        /// Start principal application
        /// </summary>
        /// <param name="notify"></param>
        /// <param name="config"></param>
        /// <param name="about"></param>
        /// <param name="grid"></param>
        /// <param name="deviceRepository"></param>
        public AppController(
            INotifyController notify,
            IConfigController config,
            IAboutController about,
            IGridController grid,
            IRepository<Device> deviceRepository
        )
        {
            this._notify = notify;
            this._config = config;
            this._about = about;
            this._grid = grid;
            this._deviceRepository = deviceRepository;
        }

        /// <summary>
        /// Run controller after load application
        /// </summary>
        public Form Run()
        {
            return new MultipleCaptureForm(this);
        }

        /// <summary>
        /// Define View to controll
        /// </summary>
        /// <param name="view">View by controll</param>
        public async Task SetView(IMultipleCaptureView view)
        {
            _view = view;
            
            _devices = new Filters().VideoInputDevices.OfType<Filter>();
            
            var devices = await _deviceRepository.GetAllAsync();
            _videosCapture = devices
                .Select(x => {
                    _devices = _devices.Where(d => d.MonikerString != x.MonikerString);
                    return new VideoCapture(new Filter(x.MonikerString))
                    {
                        Id = x.Id,
                        Size = x.Size,
                        CodNome = x.CodNome,
                        MonikerString = x.MonikerString
                    }
                    .Subscribe(_view);
                }).ToList();

            // Controls
            _view.FillListDevices(_devices);
            _view.FillListResolutions(Consts.RESOLUTION);
            _view.FillGridDevices(_videosCapture);

            await ApplyContext();
        }
        
        #region Configuration
        public async Task ApplyContext()
        {
            Config = await _config.LoadAsync();

            // Services
            try
            {
                if (Config.EnableServer)
                    ServerHttpListener.StratThread();
                else
                    ServerHttpListener.StopThread();
            }
            catch (Exception)
            {
                _notify.FailMessage("Falha ao inicializar serviços!");
            }

            //Devices
            foreach(var videoCapture in _videosCapture) { 
                // Timer
                if (Config.EnableInterval)
                {
                    videoCapture.StopVideo();
                    videoCapture.StartVideo();
                }

                videoCapture.UpdateConfig();
            }
            
            _view.UpdateConfig();
        }
        public async Task SaveConfig() => await _config.Save(Config);
        public EDeviceState GetStateCurrentDevice()
        {
            if((_videosCapture?.Count() ?? 0) == 0 || CurrentVideoCapture == 0)
                return EDeviceState.Stoped;

            return _videosCapture
                .First(x => x.Id == CurrentVideoCapture)
                .DeviceState;
        }
        #endregion

        #region Control Devices
        public void SelectDevice(int id) {
            CurrentVideoCapture = id;
            var videoCapture = _videosCapture.First(v => v.Id == id);
            _view.FillListDevices(_devices);
            _view.SetDevice(videoCapture);
        }
        public void UpdateStateDevice(EDeviceState deviceState, int id)
        {
            _view.UpdateStateDevice(deviceState, id);
        }
        public void ShowPropertyPage(int id, Control elm)
        {
            _videosCapture.First(v => v.Id == id).ShowPropertyPage(elm);
        }
        public IEnumerable<Filter> GetDevices()
        {
            return _devices;
        }
        public VideoCapture GetVideoCapture(int id)
        {
            return _videosCapture.FirstOrDefault(x => x.Id == id);
        }
        public void Exit()
        {
            foreach(var videoCapture in _videosCapture)
                videoCapture.StopDevice();
        }
        public void StartDevice(int id) {
            _videosCapture.First(v => v.Id == id).StartDevice();
        }
        public void StopDevice(int id)
        {
            _videosCapture.First(v => v.Id == id).StopDevice();
        }

        public async Task NewDevice(VideoCapture videoCapture)
        {
            await _deviceRepository.InsertAsync(videoCapture);
            _videosCapture = _videosCapture.Concat(new[] { videoCapture });
            _devices = _devices.Where(x => x.MonikerString != videoCapture.MonikerString);
            CurrentVideoCapture = videoCapture.Id;
            _view.AppendItemGridDevices(videoCapture.Subscribe(_view));
            _view.FillListDevices(_devices);
        }
        public async Task SaveDevice(VideoCapture videoCapture)
        {
            var _videoCapture = _videosCapture.First(x => x.Id == videoCapture.Id);

            await _deviceRepository.UpdateAsync(videoCapture);
            _devices = _devices.Concat(new[] { _videoCapture.Info });
            _devices = _devices.Where(x => x.MonikerString != videoCapture.MonikerString);
            _videoCapture.Info = videoCapture.Info;
            _videoCapture.MonikerString = videoCapture.MonikerString;
            _videoCapture.CodNome = videoCapture.CodNome;
            _videoCapture.Size = videoCapture.Size;
            _view.UpdateItemGridDevices(videoCapture);
            _view.FillListDevices(_devices);
            _view.SetDevice(videoCapture);
        }
        public async Task RemoveDevice(int id)
        {
            await _deviceRepository.DeleteAsync(id);
            _videosCapture.First(v => v.Id == id).Unsubscribe(_view).StopDevice();
            _devices = _devices.Concat(new []{ _videosCapture.First(v => v.Id == id).Info });
            _videosCapture = _videosCapture.Where(v => v.Id != id);
            CurrentVideoCapture = _videosCapture.LastOrDefault()?.Id ?? 0;
            _view.RemoveItemGridDevices(id);
            _view.FillListDevices(_devices);
        }
        #endregion

        #region Control Video
        public void ShareVideos()
        {
            foreach(var videoCapture in _videosCapture) { 
                videoCapture.StopVideo();
                videoCapture.StartVideo();
            }

        }
        public void StartVideo(int id)
        {
            _videosCapture.First(v => v.Id == id).StartVideo();
        }
        public void StopVideo(int id)
        {
            _videosCapture.First(v => v.Id == id).StopVideo();
        }
        #endregion

        #region Dialogs
        public void ShowAboutDialog()
        {
            using (var frm = _about.Run())
                frm.ShowDialog();
        }
        public void ShowConfigDialog()
        {
            using (var config = _config.Run())
            {
                config.ShowDialog();
                ApplyContext();
            }
        }
        public void ShowGridDialog()
        {
            using (var grid = _grid.Run()) {
                _grid.SetVideosCapture(_videosCapture);
                grid.ShowDialog();
            }
        }
        #endregion

        #region Integrate Services
        public void SaveImage(int id)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "Images|*.jpg";
            if (file.ShowDialog() == DialogResult.OK) { 
                _videosCapture
                    .First(x => x.Id == id).CurrentFrame
                    .Save(file.FileName, ImageFormat.Jpeg);
            }
        }
        public IDictionary<int, string> GetNames()
        {
            return _videosCapture.ToDictionary(v => v.Id, v => v.CodNome);
        }

        public IDictionary<int, string> GetImages()
        {
            return _videosCapture
                .ToDictionary(v => v.Id, v => v.GetImageBase64());
        }
        #endregion
    }
}
