using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DirectX.Capture;

using MultiCam.Controller;
using MultiCam.Model;
using MultiCam.Model.Dtos;
using MultiCam.Model.Entities;
using MultiCam.Model.Enums;
using MultiCam.Notify.Controller;
using MultiCam.Server.Controller;
using Ninject;

namespace MultiCam.View
{
    /// <summary>
    /// Interface view by inject dependecies
    /// </summary>
    public interface IMultipleCaptureView : IView, FrameObservable
    {
        /// <summary>
        /// Update values config view
        /// </summary>
        void UpdateConfig();

        /// <summary>
        /// Update resolutions list interface
        /// </summary>
        /// <param name="sizes">list sizes</param>
        void FillListResolutions(IEnumerable<Size> sizes);

        /// <summary>
        /// Update devices list interface
        /// </summary>
        /// <param name="devices">list devices</param>
        void FillListDevices(IEnumerable<Filter> devices);

        /// <summary>
        /// Update devices grid interface
        /// </summary>
        /// <param name="videosCapture"></param>
        void FillGridDevices(IEnumerable<VideoCapture> videosCapture);

        /// <summary>
        /// Remove item by key id
        /// </summary>
        /// <param name="id">id key device</param>
        void RemoveItemGridDevices(int id);

        /// <summary>
        /// New item by list
        /// </summary>
        /// <param name="videoCapture">Item by append</param>
        void AppendItemGridDevices(VideoCapture videoCapture);

        /// <summary>
        /// Change date by item
        /// </summary>
        /// <param name="videoCapture">Item by change</param>
        void UpdateItemGridDevices(VideoCapture videoCapture);

        /// <summary>
        /// Define icon to all devices
        /// </summary>
        /// <param name="st">icon state</param>
        void UpdateStateDevice(EDeviceState deviceState, int id);

        //TODO: Remove
        /// <summary>
        /// Get key selected device interface. Default is zero
        /// </summary>
        /// <returns>key associated item selected</returns>
        int GetTagSelectedDevice();

        /// <summary>
        /// Update options interface by device
        /// </summary>
        /// <param name="video">device config</param>
        void SetDevice(VideoCapture videoCapture);
    }

    /// <summary>
    /// Partial class global application
    /// </summary>
    public partial class MultipleCaptureForm : Form, IMultipleCaptureView
    {
        ///// <summary>Devices List View</summary>
        //public ListView ListViewDevices { get => list_view_devices; }

        ///// <summary>List devices</summary>
        //public ComboBox CmbDevices { get => cmb_device; }

        ///// <summary>List resolutions</summary>
        //public ComboBox CmbResolution { get => cmb_resolution; }

        private readonly IAppController _controller;
        private readonly INotifyController _notify;

        #region Form
        /// <summary>
        /// Contructor form application
        /// </summary>
        public MultipleCaptureForm(IAppController controller)
        {
            // Init
            InitializeComponent();
            _controller = controller;
            _controller.SetView(this);

            //Dependêncies
            _notify = RegServices.Ioc.Get<INotifyController>();
        }

        private void MultipleCaptureForm_Load(object sender, EventArgs e)
        {                       
            // Icons
            var images = new ImageList();
            images.Images.Add(Properties.Resources.cam_start);
            images.Images.Add(Properties.Resources.cam_stop);
            images.Images.Add(Properties.Resources.video_start);
            list_view_devices.SmallImageList = images;

            // Controls
            LoadControlsConfiguration();
            EnableDeviceButtons();
        }
        protected override void WndProc(ref Message m)
        {
            FormWindowState org = this.WindowState;
            base.WndProc(ref m);
            if (this.WindowState != org)
                this.OnFormWindowStateChanged(EventArgs.Empty);
        }

        protected virtual void OnFormWindowStateChanged(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            video_interval.Enabled = false;
            ServerHttpListener.StopThread();
            _controller.Exit();
            // TODO: Implements automatic bind devices
            //_controller.ctxApp.Config.Ctx.Devices = DeviceController.BindDeviceConfiguration();
            _controller.SaveConfig();
        }
        #endregion

        #region Control
        private void LoadControlsConfiguration()
        {
            mnu_start_window.Checked = _controller.Config.EnableStart;
            mnu_start_window_minimized.Checked = _controller.Config.EnableStartMinimized;
            if (_controller.Config.EnableStartMinimized) {
                this.WindowState = FormWindowState.Minimized;
                Hide();
            }
        }
        #endregion

        #region Device
        public void SetDevice(VideoCapture videoCapture) {
            cmb_device.Text = videoCapture.Info.Name;
            cmb_resolution.Text = $"{videoCapture.Size.Width} x {videoCapture.Size.Height}";
            txt_codnome.Text = videoCapture.CodNome;

            box_image.Text = $"Câmera - [{videoCapture.CodNome}]";
            UpdateStateDevice(videoCapture.DeviceState, videoCapture.Id);
        }
        #endregion

        #region Config
        public void UpdateConfig()
        {
            //link
            lbl_link.Visible = _controller.Config.EnableServer;

            //timer
            video_interval.Enabled = false;
            video_interval.Interval = (_controller.Config.TimeInterval * 60000);
            video_interval.Enabled = _controller.Config.EnableInterval;

            SelectLastDevice();
        }
        public void ShowConfigDevice()
        {
            var id = GetTagSelectedDevice();

            if (id > 0)
            {
                _controller.ShowPropertyPage( id, cmb_device );
            }
        }
        #endregion

        #region Enable Controls
        private void EnableDeviceButtons()
        {
            // Values
            var state = _controller.GetStateCurrentDevice();
            bool selected = list_view_devices.SelectedItems.Count > 0;

            bool runing = state != EDeviceState.Stoped;            
            bool recording = state == EDeviceState.Recording;

            //lateral buttons
            btn_tools.Enabled = selected && runing;
            btn_remove.Enabled = !runing && selected;
            btn_save.Enabled = !runing && selected;
            btn_add.Enabled = !selected && cmb_device.Items.Count > 0;

            //enable button image
            btn_font_start.Enabled = !runing && selected;
            btn_font_stop.Enabled = runing;
            btn_video_start.Enabled = !recording && runing;
            btn_video_stop.Enabled = recording;
        }
        private void EnableMenuButtons()
        {
            //define values attributs
            bool runing = _controller.GetStateCurrentDevice() != EDeviceState.Stoped;
            bool recording = _controller.GetStateCurrentDevice() == EDeviceState.Recording;

            //enable buttons menu
            tool_strip_start_font.Enabled = !runing;
            tool_strip_stop_font.Enabled = runing;
            tool_strip_capture.Enabled = runing;
            tool_strip_configure.Enabled = runing;
            tool_strip_start_video.Enabled = !recording && runing;
            tool_strip_stop_video.Enabled = recording;
        }
        #endregion

        #region ComboBox
        public void FillListDevices(IEnumerable<Filter> devices)
        {
            cmb_device.Items.Clear();

            cmb_device.DisplayMember = "Name";
            cmb_device.ValueMember = "Value";

            foreach (var device in devices)
            {
                cmb_device.Items.Add(new CboItemDto
                {
                    Name = device.Name,
                    Value = device
                });
            }

            AppendCurrent();

            if (devices.Count() > 0)
            {
                cmb_device.SelectedIndex = devices.Count() - 1;
            }
            else
            {
                cmb_device.Text = "[Nenhum dispositivo!]";
            }
        }
        public void FillListResolutions(IEnumerable<Size> sizes) { 
            cmb_resolution.Items.Clear();

            cmb_resolution.DisplayMember = "Name";
            cmb_resolution.ValueMember = "Value";

            foreach (var size in sizes) {
                cmb_resolution.Items.Add(new CboItemDto
                {
                    Name = $"{size.Width} x {size.Height}",
                    Value = size
                });
            }

            if(sizes.Count() > 0) {
                cmb_resolution.SelectedIndex = sizes.Count() -1;
            }
        }
        private void AppendCurrent() { 
            if(list_view_devices.SelectedItems.Count > 0) { 
                var id = (int)list_view_devices.SelectedItems[0].Tag;
                var videoCature = _controller.GetVideoCapture(id);

                cmb_device.Items.Add(new CboItemDto
                {
                    Name = videoCature.Info.Name,
                    Value = videoCature.Info
                });
            }
        }
        private void cmb_device_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO: Load resolutions
            var device = ((CboItemDto)cmb_device.SelectedItem);
            if (device != null && list_view_devices.SelectedItems.Count == 0)
            {
                txt_codnome.Text = device.GetValue<Filter>().Name.Replace(' ', '_');
            }
        }
        private void cmb_resolution_SelectedIndexChanged(object sender, EventArgs e) { }
        #endregion

        #region Buttons
        //button grid
        private void btn_tools_Click(object sender, EventArgs e)
        {
            ShowConfigDevice();
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            var device = ((CboItemDto)cmb_device.SelectedItem).GetValue<Filter>();
            var resolu = ((CboItemDto)cmb_resolution.SelectedItem).GetValue<Size>();
            var codnom = txt_codnome.Text.Replace(' ', '_');

            var videoCapture = new VideoCapture(device){ 
                Size = resolu,
                CodNome = codnom
            };

            _controller.NewDevice(videoCapture);
            EnableDeviceButtons();
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            var device = ((CboItemDto)cmb_device.SelectedItem)?.GetValue<Filter>();
            var resolu = ((CboItemDto)cmb_resolution.SelectedItem)?.GetValue<Size>();
            var codnom = txt_codnome.Text.Replace(' ', '_');
            var id = GetTagSelectedDevice();

            if(device == null) { 
                _notify.FailMessage("Dispositivo inválido");
                return;
            }

            if (resolu == null)
            {
                _notify.FailMessage("Resolução inválida");
                return;
            }

            var videoCapture = new VideoCapture(device)
            {
                Id = id,
                Size = resolu.Value,
                CodNome = codnom
            };

            _controller.SaveDevice(videoCapture);
        }
        private void btn_remove_Click(object sender, EventArgs e)
        {
            _controller.RemoveDevice(_controller.CurrentVideoCapture);
        }

        //button image
        private void btn_font_start_Click(object sender, EventArgs e)
        {
            _controller.StartDevice(_controller.CurrentVideoCapture);
        }

        private void btn_font_stop_Click(object sender, EventArgs e)
        {
            _controller.StopDevice(_controller.CurrentVideoCapture);
        }

        private void btn_video_start_Click(object sender, EventArgs e)
        {
            _controller.StartVideo(_controller.CurrentVideoCapture);
        }

        private void btn_video_stop_Click(object sender, EventArgs e)
        {
            _controller.StopVideo(_controller.CurrentVideoCapture);
        }

        //button zoom
        private void Btn_view_zoom_Click(object sender, EventArgs e)
        {
            _controller.ShowGridDialog();
        }
        #endregion

        #region Label
        private void lbl_link_VisibleChanged(object sender, EventArgs e)
        {
            lbl_link.Text = $"http://{ServerHttpListener.LocalIPAddress()}:80/";
        }
        private void lbl_link_Click(object sender, EventArgs e)
        {
            Process.Start(lbl_link.Text);
        }   
        #endregion

        #region Menu Primary
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void configuracoesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ShowConfigDialog();
            EnableDeviceButtons();
        }
        private void mnu_start_window_Click(object sender, EventArgs e)
        {
            if(Helpers.SetStartup(!_controller.Config.EnableStart))
                _controller.Config.EnableStart = mnu_start_window.Checked = !_controller.Config.EnableStart;
        }
        private void mnu_start_window_minimized_Click(object sender, EventArgs e)
        {
            _controller.Config.EnableStartMinimized = mnu_start_window_minimized.Checked = !_controller.Config.EnableStartMinimized;
        }
        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ShowAboutDialog();
        }
        #endregion

        #region Menu Device
        private void menu_view_image_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (list_view_devices.SelectedItems.Count > 0)
                EnableMenuButtons();
            else
                e.Cancel = true;
        }
        private void tool_strip_start_font_Click(object sender, EventArgs e) {
            _controller.StartDevice(_controller.CurrentVideoCapture);
        }
        private void tool_strip_stop_font_Click(object sender, EventArgs e)
        {
            _controller.StopDevice(_controller.CurrentVideoCapture);
        }
        private void tool_strip_start_video_Click(object sender, EventArgs e)
        {
            _controller.StartVideo(_controller.CurrentVideoCapture);
        }
        private void tool_strip_stop_video_Click(object sender, EventArgs e)
        {
            _controller.StopVideo(_controller.CurrentVideoCapture);
        }
        private void tool_strip_capture_Click(object sender, EventArgs e)
        {
            _controller.SaveImage(_controller.CurrentVideoCapture);
        }
        private void tool_strip_configure_Click(object sender, EventArgs e)
        {
            ShowConfigDevice();
        }
        #endregion

        #region Timer Video Interval
        private void video_interval_Tick(object sender, EventArgs e)
        {
            _controller.ShareVideos();
        }
        #endregion

        #region Icon Application Notify
        private void icon_notify_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }
        #endregion

        #region Grid
        private void list_view_devices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_view_devices.SelectedItems.Count > 0){
                var id = GetTagSelectedDevice();
                _controller.SelectDevice(id);
            }else{
                FillListDevices(_controller.GetDevices());
            }
            EnableDeviceButtons();
        }
        public void UpdateStateDevice(EDeviceState deviceState, int id) {
            foreach (ListViewItem row in list_view_devices.Items)
            {
                if ((int)row.Tag == id)
                {
                    row.ImageIndex = (int)deviceState;
                    break;
                }
            }
            if(_controller.CurrentVideoCapture == id)
            {
                switch (deviceState)
                {
                    case EDeviceState.Runing:
                        image_state.BackColor = Color.Green;
                        break;
                    case EDeviceState.Recording:
                        image_state.BackColor = Color.Red;
                        break;
                    case EDeviceState.Stoped:
                        image_grid.Image = null;
                        image_state.BackColor = SystemColors.Control;
                        break;
                }
            }
            EnableDeviceButtons();
        }
        public int GetTagSelectedDevice()
        {
            if (list_view_devices.SelectedItems.Count > 0)
                return (int)list_view_devices.SelectedItems[0].Tag;
            return _controller.CurrentVideoCapture;
        }
                
        public void UpdateFrame(int id, Image img)
        {
            if (id == _controller.CurrentVideoCapture) { 
                image_grid.Image?.Dispose();
                image_grid.Image = img;
            }
            else { 
                img.Dispose();    
            }
        }
        public void FillGridDevices(IEnumerable<VideoCapture> videosCapture)
        {
            list_view_devices.Clear();
            list_view_devices.HideSelection = false;

            var tam = list_view_devices.Width;
            list_view_devices.Columns.Add("Dispositivo", Helpers.ProportionalWidth(tam, 44));
            list_view_devices.Columns.Add("Resolução", Helpers.ProportionalWidth(tam, 28));
            list_view_devices.Columns.Add("Nome", Helpers.ProportionalWidth(tam, 28));

            //insert data
            foreach (var videoCapture in videosCapture)
                AppendItemGridDevices(videoCapture);
        }
        public void RemoveItemGridDevices(int id)
        {
            foreach(ListViewItem row in list_view_devices.Items)
            {
                if((int)row.Tag == id) {
                    list_view_devices.Items.Remove(row);
                    break;    
                }
            }

            SelectLastDevice();
        }
        public void AppendItemGridDevices(VideoCapture videoCapture)
        {
            var row = list_view_devices.Items.Add(new ListViewItem(new[] {
                    videoCapture.Info.Name, //name device
                    $"{videoCapture.Size.Width} x {videoCapture.Size.Height}", //resolution device
                    videoCapture.CodNome
                }));

            row.Tag = videoCapture.Id;

            row.ImageIndex = (int)videoCapture.DeviceState;
        }
        public void UpdateItemGridDevices(VideoCapture videoCapture)
        {
            foreach (ListViewItem row in list_view_devices.Items)
            {
                if ((int)row.Tag == videoCapture.Id)
                {
                    row.SubItems[0].Text = videoCapture.Info.Name;//Dispositivo
                    row.SubItems[1].Text = $"{videoCapture.Size.Width} x {videoCapture.Size.Height}";//Resolução
                    row.SubItems[2].Text = videoCapture.CodNome;
                    row.ImageIndex = (int)videoCapture.DeviceState;
                    break;
                }
            }
        }
        private void SelectLastDevice()
        {
            if (list_view_devices.Items.Count > 0)
            {
                list_view_devices.Items[list_view_devices.Items.Count - 1].Selected = true;
                list_view_devices.Select();
            }
        }
        #endregion
    }
}
