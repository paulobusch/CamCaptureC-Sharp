using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using MultiCam.Config.Controller;
using MultiCam.Config.Model.Dtos;
using MultiCam.Controller;
using MultiCam.Model;
using MultiCam.Model.Dtos;
using MultiCam.Model.Enums;

namespace MultiCam.Config.View
{
    /// <summary>
    /// Interface view by inject dependecies
    /// </summary>
    public interface IConfigView : IView { 
        
    }
    public partial class ConfigurationForm : Form, IConfigView
    {
        private readonly IConfigController _controller;
        private Configuration _config;

        public ConfigurationForm(IConfigController controller)
        {
            InitializeComponent();

            //load options inputs
            InitFonts();

            //load config interface
            _config = controller.Load();
            txt_dir.Text = _config.PathSaveVideo;
            bar_timer.Value = _config.TimeInterval;
            bar_timer.Enabled = _config.EnableInterval;
            bar_frame_rate.Value = _config.FrameRate;
            bar_bit_rate.Value = _config.BitRate / 1000;
            chk_enable_timer.Checked = _config.EnableInterval;
            chk_web_difusion.Checked = _config.EnableServer;
            chk_enable_compress.Checked = _config.EnableCompressVideo;
            chk_date_time.Checked = _config.ViewLegend;
            chk_separate_registers.Checked = _config.FolderFormat != null;
            cmb_font.Text = _config.Font.Size.ToString();
            SetFolder(_config.FolderFormat);
            SetAlign(_config.LegendAlign);

            _controller = controller;
        }

        #region Submit
        private void btn_ok_config_Click(object sender, EventArgs e)
        {
            //aply values
            _config.PathSaveVideo = GetPath(txt_dir.Text);
            _config.TimeInterval = bar_timer.Value;
            _config.FrameRate = bar_frame_rate.Value;
            _config.BitRate = bar_bit_rate.Value * 1000;
            _config.EnableInterval = chk_enable_timer.Checked;
            _config.EnableServer = chk_web_difusion.Checked;
            _config.EnableCompressVideo = chk_enable_compress.Checked;
            _config.ViewLegend = chk_date_time.Checked;
            _config.Font = new Font(_config.Font.FontFamily, int.Parse(cmb_font.Text));
            _config.FolderFormat = GetFormat();
            _config.LegendAlign = GetAlign();
            _config.State = EDbState.Update;

            _controller.Save(_config);

            Close();
        }

        private string GetFormat()
        {
            if(chk_separate_registers.Checked)
                return Consts.REGISTERS_FOLDER_NAME[bar_folder_name_precision.Value];
            return null;
        }

        private void btn_cancel_config_Click(object sender, EventArgs e) => Close();
        #endregion

        #region Search
        private string GetPath(string txt)
        {
            return txt.Last() != '\\' ? txt + @"\" : txt;
        }
        private void btn_search_dir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog search = new FolderBrowserDialog();
            search.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

            if (search.ShowDialog() == DialogResult.OK)
                txt_dir.Text = search.SelectedPath;
        }
        #endregion

        #region Timer
        private void chk_enable_timer_DockChanged(object sender, EventArgs e)
        {
            //enable controls
            bar_timer.Enabled = chk_enable_timer.Checked;
            lbl_interval.Enabled = chk_enable_timer.Checked;

            //aplly values
            if(!chk_enable_timer.Checked)
                bar_timer.Value = _config.TimeInterval;
        }
        #endregion

        #region Legend
        private void chk_date_time_CheckedChanged(object sender, EventArgs e)
        {
            //checkboxes
            chk_top_left.Enabled = chk_date_time.Checked;
            chk_top_right.Enabled = chk_date_time.Checked;
            chk_botton_left.Enabled = chk_date_time.Checked;
            chk_botton_right.Enabled = chk_date_time.Checked;

            //labels
            lbl_align.Enabled = chk_date_time.Checked;
            lbl_font.Enabled = chk_date_time.Checked;

            //combobox
            cmb_font.Enabled = chk_date_time.Checked;

            //aplly values
            if (!chk_date_time.Checked){
                cmb_font.Text = _config.Font.Size.ToString();
                SetAlign(_config.LegendAlign);
            }
        }
        #endregion

        #region Legend Align
        public void SetAlign(ELegendAlign align)
        {
            chk_top_left.Checked = align == ELegendAlign.TopLeft;
            chk_top_right.Checked = align == ELegendAlign.TopRight;
            chk_botton_left.Checked = align == ELegendAlign.BottonLeft;
            chk_botton_right.Checked = align == ELegendAlign.BottonRight;
        }
        public ELegendAlign GetAlign()
        {
            if (chk_top_left.Checked)
                return ELegendAlign.TopLeft;
            if (chk_top_right.Checked)
                return ELegendAlign.TopRight;
            if (chk_botton_left.Checked)
                return ELegendAlign.BottonLeft;

            return ELegendAlign.BottonRight;
        }
        private void chk_top_left_Click(object sender, EventArgs e)
        {
            SetAlign(ELegendAlign.TopLeft);
        }
        private void chk_top_right_Click(object sender, EventArgs e)
        {
            SetAlign(ELegendAlign.TopRight);
        }
        private void chk_botton_right_Click(object sender, EventArgs e)
        {
            SetAlign(ELegendAlign.BottonRight);
        }
        private void chk_botton_left_Click(object sender, EventArgs e)
        {
            SetAlign(ELegendAlign.BottonLeft);
        }
        #endregion

        #region Legend Font
        private void InitFonts()
        {
            foreach (var size in Consts.FONT_SIZE)
                cmb_font.Items.Add(size.ToString());
        }
        #endregion

        #region Bar Control
        private void bar_timer_ValueChanged(object sender, EventArgs e)
        {
            TrackBarInterval(bar_timer);
            lbl_interval.Text = $"Tempo de vídeo [{bar_timer.Value} min]:";
        }
        private void bar_frame_rate_ValueChanged(object sender, EventArgs e)
        {
            TrackBarInterval(bar_frame_rate);
            lbl_frame_rate.Text = $"Frame rate [{bar_frame_rate.Value} FPS]";
        }
        private void bar_bit_rate_ValueChanged(object sender, EventArgs e)
        {
            TrackBarInterval(bar_bit_rate);
            lbl_bit_rate.Text = $"Taxa de bits [{bar_bit_rate.Value} Kbps]";
        }
        private void bar_folder_name_precision_ValueChanged(object sender, EventArgs e)
        {
            TrackBarInterval(bar_folder_name_precision);
            var format = Consts.REGISTERS_FOLDER_NAME[bar_folder_name_precision.Value];
            lbl_forlder_name.Text = $"Nome Ex: [{DateTime.Now.ToString(format)}]";
        }
        private void TrackBarInterval(TrackBar bar)
        {
            if ((bool)(bar.Tag ?? false)) return;
            var value = bar.Value;
            var samll = bar.LargeChange;

            if (bar.Value % samll != 0)
            {
                value = (value / samll) * samll;
                bar.Tag = true;
                bar.Value = value;
                bar.Tag = false;
            }
        }

        #endregion

        #region Folder
        private void Chk_separate_registers_CheckedChanged(object sender, EventArgs e)
        {
            //labels
            lbl_year.Enabled = chk_separate_registers.Checked;
            lbl_month.Enabled = chk_separate_registers.Checked;
            lbl_day.Enabled = chk_separate_registers.Checked;
            lbl_forlder_name.Enabled = chk_separate_registers.Checked;

            //bar
            bar_folder_name_precision.Enabled = chk_separate_registers.Checked;
        }
        private void SetFolder(string folderFormat)
        {
            int precision;
            if(folderFormat == null) { 
                precision = 1;//Ano-Mês
            }else{ 
                precision = Consts.REGISTERS_FOLDER_NAME
                .First(x => x.Value == folderFormat)
                .Key;
            }

            bar_folder_name_precision.Value = precision;
            var format = Consts.REGISTERS_FOLDER_NAME[precision];
            lbl_forlder_name.Text = $"Nome Ex: [{DateTime.Now.ToString(format)}]";
        }
        #endregion
    }
}
