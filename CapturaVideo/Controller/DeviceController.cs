using CapturaVideo.Model.Dtos;
using CapturaVideo.Model.Enums;
using DirectX.Capture;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WebServer.Models;

namespace CapturaVideo.Model
{
    public static class DeviceController
    {
        //public
        public static int selected_device;
        public static List<Filter> devices;
        public static DeviceInterface device_interface;
        public static ConfigurationDto Configuration;

        //private
        private static Dictionary<int, DeviceCapture> devices_capture;

        //constrols
        public static ListView list_view_devices;
        public static Timer timer_video_interval;
        public static PictureBox image_grid;
        public static PictureBox image_state;

        public static ComboBox cmb_device;
        public static ComboBox cmb_resolution;
        public static GroupBox box_image;
        public static Label lbl_link;

        #region Configuration
        public static void LoadConfiguration()
        {
            selected_device = 0;
            device_interface = new DeviceInterface();
            devices_capture = new Dictionary<int, DeviceCapture>();
            devices = new List<Filter>();

            //check infraestructure
            //new SqLite().CreateTablesIfNotExists();

            //define devices
            var filtro = new Filters();
            Configuration = Config.LoadConfiguration();
            foreach (Filter cam in filtro.VideoInputDevices)
                devices.Add(cam);

            //init controls interface
            LoadResolutions();
            RestoreDevicesConfiguration();
            FillListView();
            ApplyConfiguration();
        }
        public static void ApplyConfiguration()
        {
            //start services
            try
            {
                if (Configuration.EnableServer)
                    ServerHttpListener.StratThread();
                else
                    ServerHttpListener.StopThread();
            }
            catch (Exception)
            {
                MessageBox.Show("Falha ao inicializar serviços!", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }


            //link
            lbl_link.Visible = Configuration.EnableServer;

            //update device
            IterateDevices(x => x.Value.UpdateConfiguration());

            //timer interval
            if (Configuration.EnableInterval) {
                    StartAllDevices();
                    StartAllVideo();
            }

            timer_video_interval.Enabled = false;
            timer_video_interval.Interval = (Configuration.TimeInterval * 60000);
            timer_video_interval.Enabled = Configuration.EnableInterval;
        }
        public static void RestoreDevicesConfiguration() {
            foreach (var cam in Configuration.Devices)
            {
                //aplly values interface
                var info = devices.Find(x => x.MonikerString == cam.MonikerString);
                try
                {
                    if (info != null)
                    {
                        device_interface.info = info;
                        device_interface.resolution = cam.Size;

                        NewDevice();
                    }
                }
                catch (Exception){
                    MessageBox.Show("Falha ao restaurar os dispositivos!", "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }
        public static IEnumerable<DeviceDto> BindDeviceConfiguration()
        {
            foreach(var device in devices_capture) { 
                yield return new DeviceDto() {
                    MonikerString = device.Value.device.info.MonikerString,
                    Size = device.Value.device.resolution
                };
            };
        }
        #endregion

        #region List Devices
        private static int ProportionalWidth(int tam, int porcent) => (tam - 5) * porcent / 100;
        private static void FillListView()
        {
            List<ListViewItem> lines = new List<ListViewItem>();
            list_view_devices.Clear();


            var tam = list_view_devices.Width;
            list_view_devices.Columns.Add("Dispositivo", ProportionalWidth(tam, 65));
            list_view_devices.Columns.Add("Resolução", ProportionalWidth(tam, 35));

            //insert data
            IterateDevices(x => {
                var item = list_view_devices.Items.Add(new ListViewItem(new[] {
                    x.Value.device.info.Name, //name device
                    $"{x.Value.device.resolution.Width} x {x.Value.device.resolution.Height}" //resolution device
                }));

                item.Tag = x.Key;

                //cannot working with key string values
                item.ImageIndex = (int)x.Value.GetDeviceState();
            });

            LoadDevice();
        }
        public static void ChangeAllIconState(EDeviceState st)
        {
            var stInt = (int)st;
            foreach (ListViewItem item in list_view_devices.Items)
                item.ImageIndex = stInt;
        }
        public static void ChangeIconState(int key, EDeviceState st)
        {
            foreach (ListViewItem item in list_view_devices.Items)
            {
                if ((int)item.Tag == key)
                {
                    item.ImageIndex = (int)st;
                    break;
                }
            }
        }
        #endregion

        #region Control Devices
        public static void IterateDevices(Action<KeyValuePair<int, DeviceCapture>> task)
        {
            lock (devices_capture)
                foreach (KeyValuePair<int, DeviceCapture> dev in devices_capture)
                    task(dev);
        }
        public static EDeviceState GetStateCurrentDevice()
        {
            var dev = GetDevice(selected_device);
            return dev?.GetDeviceState() ?? EDeviceState.Stoped;
        }
        public static DeviceCapture GetDevice(int key) {
            DeviceCapture ret;
            if (!devices_capture.TryGetValue(key, out ret))
                return null;
            return ret;
        }
        public static void ShowConfigurationDevice() {
            var dev = devices_capture[selected_device].device;
            if (dev.font == null)
                dev.font = new Capture(dev.info, null);
            dev.font.PropertyPages[0].Show( cmb_device );
        }
        public static void StartAllDevices() {
            IterateDevices(x => x.Value.StartDevice());
            ChangeAllIconState(EDeviceState.Runing);
        }
        public static void StopAllDevices(bool refreshIcons = true) {
            IterateDevices(x => x.Value.StopDevice());
            if(refreshIcons)
                ChangeAllIconState(EDeviceState.Stoped);
        }
        public static void LoadDevice()
        {
            //list devices
            FillCombobox(cmb_device, devices.ToArray(), x => {
                var y = (Filter)x;
                return new ComboboxItemDto { Name = y.Name, Value = y };
            }, null, "Nenhum dispositivo");
        }
        #endregion

        #region Resolution
        public static void LoadResolutions()
        {
            //update resolutions
            FillCombobox(cmb_resolution, Consts.RESOLUTION, x => {
                var y = (Size)x;
                return new ComboboxItemDto { Name = $"{y.Width} x {y.Height}", Value = y };                 
            }, "640 x 480", "[Vazio]");
        }
        private static void FillCombobox(ComboBox cmb, Array collection, Func<object, ComboboxItemDto> action, string defull = null, string empty = null)
        {
            cmb.Items.Clear();
            foreach (var item in collection)
                cmb.Items.Add(action(item));

            cmb.DisplayMember = "Name";
            cmb.ValueMember = "Value";

            if (cmb.Items.Count > 0){
                if (string.IsNullOrEmpty(defull))
                    cmb.SelectedIndex = cmb.Items.Count - 1;
                else
                    cmb.Text = defull;
            }else
                cmb.Text = empty;
        }
        #endregion

        #region Control Video
        public static void DividerVideo()
        {
            IterateDevices(x => {
                x.Value.StopVideo();
                x.Value.StartVideo();
            });
            ChangeAllIconState(EDeviceState.Recording);
        }
        public static void StartAllVideo() {
            IterateDevices(x => x.Value.StartVideo());
            ChangeAllIconState(EDeviceState.Recording);
        }
        public static void StopAllVideo() {
            IterateDevices(x => x.Value.StopVideo());
            ChangeAllIconState(EDeviceState.Runing);
        }
        #endregion

        #region Integrate Services
        public static Dictionary<int, string> GetNames()
        {
            var ret = new Dictionary<int, string>();
            IterateDevices(x => {
                ret.Add(
                    x.Key, x.Value.device.info.Name
                );
            });
            return ret;
        }

        public static Dictionary<int, string> GetImages()
        {
            var ret = new Dictionary<int, string>();
            IterateDevices(x => {
                byte[] data;
                lock(x.Value.process_image)
                    data = Helpers.ToByteArray(x.Value.process_image);
                ret.Add(
                    x.Key,
                    Convert.ToBase64String(data)
                );
            });

            return ret;
        }
        internal static void NewDevice()
        {
            var new_key = devices_capture.Count > 0 ? devices_capture.Keys.Max() + 1 : 1;
            lock(devices_capture)
                devices_capture.Add(new_key, new DeviceCapture() { key = new_key, device = device_interface.Clone() });
            Configuration.State = EDbState.Update;
            GetDevice(new_key).ReserveDevice();
            selected_device = new_key;
            FillListView();
        }

        internal static void SaveDevice(){
            if (list_view_devices.SelectedItems.Count > 0)
            {
                var key = (int)list_view_devices.SelectedItems[0].Tag;
                GetDevice(key).device = device_interface.Clone();
                Configuration.State = EDbState.Update;
                FillListView();
            }   
        }

        internal static void RemoveDevice()
        {
            if (list_view_devices.SelectedItems.Count > 0)
            {
                var key = (int)list_view_devices.SelectedItems[0].Tag;
                GetDevice(key).StopDevice();
                GetDevice(key).RestoreDevice();
                lock(devices_capture)
                    devices_capture.Remove(key);
                Configuration.State = EDbState.Update;
                FillListView();
            }
        }
        #endregion
    }
}
