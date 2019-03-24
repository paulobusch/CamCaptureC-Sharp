using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using CapturaVideo.Model;
using CapturaVideo.Janelas;
using WebServer.Models;
using DirectX.Capture;
using CapturaVideo.Model.Enums;

namespace CapturaVideo
{
    public partial class MultipleCaptureForm : Form
    {
        private static IConfiguration _configuration;

        #region Form
        public MultipleCaptureForm()
        {
            // Inject dependeicies
            var notification = new Notification();
            var context = new SqLite();
            _configuration = new Config(context, notification);

            InitializeComponent();
        }

        private void MultipleCaptureForm_Load(object sender, EventArgs e)
        {
            //start configuration devices
            DeviceController.list_view_devices = list_view_devices;
            DeviceController.timer_video_interval = video_interval;
            DeviceController.image_grid = image_grid;
            DeviceController.image_state = image_state;
            DeviceController.cmb_device = cmb_device;
            DeviceController.cmb_resolution = cmb_resolution;
            DeviceController.box_image = box_image;
            DeviceController.lbl_link = lbl_link;

            DeviceController.LoadConfiguration(_configuration);

            //image icons
            var images = new ImageList();
            images.Images.Add(Properties.Resources.cam_start);
            images.Images.Add(Properties.Resources.cam_stop);
            images.Images.Add(Properties.Resources.video_start);
            list_view_devices.SmallImageList = images;

            //load controls
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
            DeviceController.StopAllDevices(false);
            // TODO: Implements automatic bind devices
            DeviceController.Configuration.Devices = DeviceController.BindDeviceConfiguration();
            _configuration.SaveConfiguration(DeviceController.Configuration);
        }
        #endregion

        #region Control
        private void LoadControlsConfiguration()
        {
            mnu_start_window.Checked = DeviceController.Configuration.EnableStart;
            mnu_start_window_minimized.Checked = DeviceController.Configuration.EnableStartMinimized;
            if (DeviceController.Configuration.EnableStartMinimized) {
                this.WindowState = FormWindowState.Minimized;
                Hide();
            }
        }
        #endregion

        #region Enable Controls
        private void EnableDeviceButtons()
        {
            //values
            var state = DeviceController.GetStateCurrentDevice();
            bool selected = list_view_devices.SelectedItems.Count > 0;

            bool runing = state != EDeviceState.Stoped;            
            bool recording = state == EDeviceState.Recording;

            //lateral buttons
            btn_tools.Enabled = selected;
            btn_remove.Enabled = !runing && selected;
            btn_save.Enabled = !runing && selected;
            btn_add.Enabled = DeviceController.devices
                .Contains(DeviceController.device_interface.info);

            //enable button image
            btn_font_start.Enabled = !runing && selected;
            btn_font_stop.Enabled = runing;
            btn_video_start.Enabled = !recording && runing;
            btn_video_stop.Enabled = recording;
        }
        private void EnableMenuButtons()
        {
            //define values attributs
            bool runing = DeviceController.device_interface.font?.Running ?? false;
            var key = (int)list_view_devices.SelectedItems[0].Tag;
            bool recording = DeviceController.GetDevice(key).GetDeviceState() == EDeviceState.Recording;

            //enable buttons menu
            tool_strip_start_font.Enabled = !runing;
            tool_strip_stop_font.Enabled = runing;
            tool_strip_capture.Enabled = runing;
            tool_strip_start_video.Enabled = !recording && runing;
            tool_strip_stop_video.Enabled = recording;
        }
        #endregion

        #region ComboBox
        private void cmb_device_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceController.device_interface.GetInfoInteface();
        }
        private void cmb_resolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceController.device_interface.GetResolutionInteface();
        }

        #endregion

        #region Buttons
        //button grid
        private void btn_add_Click(object sender, EventArgs e)
        {
            DeviceController.NewDevice();
            EnableDeviceButtons();
        }
        private void btn_tools_Click(object sender, EventArgs e)
        {
            DeviceController.ShowConfigurationDevice();
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            DeviceController.SaveDevice();
        }
        private void btn_remove_Click(object sender, EventArgs e)
        {
            DeviceController.RemoveDevice();
            EnableDeviceButtons();
        }

        //button image
        private void btn_font_start_Click(object sender, EventArgs e)
        {
            ActionDevice(x => {
                x.StartDevice();
                return EDeviceState.Runing;
            });
        }

        private void btn_font_stop_Click(object sender, EventArgs e)
        {
            ActionDevice(x => {
                x.StopDevice();
                return EDeviceState.Stoped;
            });
        }

        private void btn_video_start_Click(object sender, EventArgs e)
        {
            ActionDevice(x => {
                x.StartVideo();
                return EDeviceState.Recording;
            });
        }

        private void btn_video_stop_Click(object sender, EventArgs e)
        {
            ActionDevice(x => {
                x.StopVideo();
                return EDeviceState.Runing;
            });
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
            using (var ConfigurationForm = new ConfigurationForm()) {
                ConfigurationForm.ShowDialog();
                if (ConfigurationForm.save) {
                    DeviceController.Configuration.State = EDbState.Update;
                    // TODO: Implements automatic bind devices
                    DeviceController.Configuration.Devices = DeviceController.BindDeviceConfiguration();
                    _configuration.SaveConfiguration(DeviceController.Configuration);
                    DeviceController.ApplyConfiguration();
                }
            }
            EnableDeviceButtons();
        }
        private void mnu_start_window_Click(object sender, EventArgs e)
        {
            if(Helpers.SetStartup(!DeviceController.Configuration.EnableStart))
                DeviceController.Configuration.EnableStart = mnu_start_window.Checked = !DeviceController.Configuration.EnableStart;
        }
        private void mnu_start_window_minimized_Click(object sender, EventArgs e)
        {
            DeviceController.Configuration.EnableStartMinimized = mnu_start_window_minimized.Checked = !DeviceController.Configuration.EnableStartMinimized;
        }
        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new AboutForm())
                frm.ShowDialog();
        }
        #endregion

        #region Menu Device
        //methods controls
        private void ActionDevice(Func<DeviceCapture, EDeviceState> task)
        {
            int key;
            if(list_view_devices.SelectedItems.Count > 0)
            {
                var item = list_view_devices.SelectedItems[0];
                key = (int)item.Tag;
                item.ImageIndex = (int)task(DeviceController.GetDevice(key));
            }else{
                key = DeviceController.selected_device;
                DeviceController.ChangeIconState(key, task(DeviceController.GetDevice(key)));
            }
            EnableDeviceButtons();
        }

        //controls
        private void menu_view_image_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (list_view_devices.SelectedItems.Count > 0)
                EnableMenuButtons();
            else
                e.Cancel = true;
        }
        private void tool_strip_start_font_Click(object sender, EventArgs e) {
            ActionDevice(x => {
                x.StartDevice();
                return EDeviceState.Runing;
            });
        }
        private void tool_strip_stop_font_Click(object sender, EventArgs e)
        {
            ActionDevice(x => {
                x.StopDevice();
                return EDeviceState.Stoped;
            });
        }
        private void tool_strip_start_video_Click(object sender, EventArgs e)
        {
            ActionDevice(x => {
                x.StartVideo();
                return EDeviceState.Recording;
            });
        }
        private void tool_strip_stop_video_Click(object sender, EventArgs e)
        {
            ActionDevice(x => {
                x.StopVideo();
                return EDeviceState.Runing;
            });
        }
        private void tool_strip_capture_Click(object sender, EventArgs e)
        {
            var key = (int)list_view_devices.SelectedItems[0].Tag;
            var img = DeviceController.GetDevice(key).process_image;

            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "Images|*.jpg";
            if (file.ShowDialog() == DialogResult.OK)
                img.Save(file.FileName, ImageFormat.Jpeg);

        }
        private void tool_strip_configure_Click(object sender, EventArgs e)
        {
            DeviceController.ShowConfigurationDevice();
        }
        #endregion

        #region Timer Video Interval
        private void video_interval_Tick(object sender, EventArgs e)
        {
            DeviceController.DividerVideo();
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
            if (list_view_devices.SelectedItems.Count > 0)
            {
                var key = (int)list_view_devices.SelectedItems[0].Tag;
                DeviceController.GetDevice(key).device.SetValuesInterface();
                DeviceController.selected_device = key;
                DeviceController.image_grid.Image = null;
                DeviceController.image_state.BackColor = SystemColors.Control;
            }
            else{
                DeviceController.LoadDevice();
            }
            EnableDeviceButtons();
        }
        #endregion
    }
}
