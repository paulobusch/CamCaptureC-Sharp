namespace MultiCam.Config.View
{
    partial class ConfigurationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_dir = new System.Windows.Forms.TextBox();
            this.lbl_interval = new System.Windows.Forms.Label();
            this.bar_timer = new System.Windows.Forms.TrackBar();
            this.chk_enable_timer = new System.Windows.Forms.CheckBox();
            this.btn_ok_config = new System.Windows.Forms.Button();
            this.btn_cancel_config = new System.Windows.Forms.Button();
            this.chk_web_difusion = new System.Windows.Forms.CheckBox();
            this.lbl_frame_rate = new System.Windows.Forms.Label();
            this.bar_frame_rate = new System.Windows.Forms.TrackBar();
            this.bar_bit_rate = new System.Windows.Forms.TrackBar();
            this.lbl_bit_rate = new System.Windows.Forms.Label();
            this.chk_date_time = new System.Windows.Forms.CheckBox();
            this.text_align = new System.Windows.Forms.GroupBox();
            this.cmb_font = new System.Windows.Forms.ComboBox();
            this.lbl_font = new System.Windows.Forms.Label();
            this.lbl_align = new System.Windows.Forms.Label();
            this.chk_botton_right = new System.Windows.Forms.CheckBox();
            this.chk_botton_left = new System.Windows.Forms.CheckBox();
            this.chk_top_right = new System.Windows.Forms.CheckBox();
            this.chk_top_left = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_day = new System.Windows.Forms.Label();
            this.lbl_month = new System.Windows.Forms.Label();
            this.lbl_year = new System.Windows.Forms.Label();
            this.bar_folder_name_precision = new System.Windows.Forms.TrackBar();
            this.lbl_forlder_name = new System.Windows.Forms.Label();
            this.chk_separate_registers = new System.Windows.Forms.CheckBox();
            this.btn_search_dir = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_enable_compress = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.bar_timer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar_frame_rate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar_bit_rate)).BeginInit();
            this.text_align.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar_folder_name_precision)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local de Registros:";
            // 
            // txt_dir
            // 
            this.txt_dir.Location = new System.Drawing.Point(31, 75);
            this.txt_dir.Name = "txt_dir";
            this.txt_dir.Size = new System.Drawing.Size(230, 22);
            this.txt_dir.TabIndex = 2;
            // 
            // lbl_interval
            // 
            this.lbl_interval.AutoSize = true;
            this.lbl_interval.Enabled = false;
            this.lbl_interval.Location = new System.Drawing.Point(18, 116);
            this.lbl_interval.Name = "lbl_interval";
            this.lbl_interval.Size = new System.Drawing.Size(185, 17);
            this.lbl_interval.TabIndex = 3;
            this.lbl_interval.Text = "Tempo de vídeo [5 min - 1h]";
            // 
            // bar_timer
            // 
            this.bar_timer.Enabled = false;
            this.bar_timer.Location = new System.Drawing.Point(21, 146);
            this.bar_timer.Maximum = 60;
            this.bar_timer.Minimum = 5;
            this.bar_timer.Name = "bar_timer";
            this.bar_timer.Size = new System.Drawing.Size(417, 56);
            this.bar_timer.SmallChange = 5;
            this.bar_timer.TabIndex = 4;
            this.bar_timer.TickFrequency = 5;
            this.bar_timer.Value = 5;
            this.bar_timer.ValueChanged += new System.EventHandler(this.bar_timer_ValueChanged);
            // 
            // chk_enable_timer
            // 
            this.chk_enable_timer.AutoSize = true;
            this.chk_enable_timer.Location = new System.Drawing.Point(21, 40);
            this.chk_enable_timer.Name = "chk_enable_timer";
            this.chk_enable_timer.Size = new System.Drawing.Size(120, 21);
            this.chk_enable_timer.TabIndex = 5;
            this.chk_enable_timer.Text = "Vídeo em loop";
            this.chk_enable_timer.UseVisualStyleBackColor = true;
            this.chk_enable_timer.CheckedChanged += new System.EventHandler(this.chk_enable_timer_DockChanged);
            // 
            // btn_ok_config
            // 
            this.btn_ok_config.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ok_config.Location = new System.Drawing.Point(826, 484);
            this.btn_ok_config.Margin = new System.Windows.Forms.Padding(5);
            this.btn_ok_config.Name = "btn_ok_config";
            this.btn_ok_config.Padding = new System.Windows.Forms.Padding(5);
            this.btn_ok_config.Size = new System.Drawing.Size(115, 35);
            this.btn_ok_config.TabIndex = 6;
            this.btn_ok_config.Text = "OK";
            this.btn_ok_config.UseVisualStyleBackColor = true;
            this.btn_ok_config.Click += new System.EventHandler(this.btn_ok_config_Click);
            // 
            // btn_cancel_config
            // 
            this.btn_cancel_config.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_cancel_config.Location = new System.Drawing.Point(682, 484);
            this.btn_cancel_config.Margin = new System.Windows.Forms.Padding(5);
            this.btn_cancel_config.Name = "btn_cancel_config";
            this.btn_cancel_config.Padding = new System.Windows.Forms.Padding(5);
            this.btn_cancel_config.Size = new System.Drawing.Size(115, 35);
            this.btn_cancel_config.TabIndex = 7;
            this.btn_cancel_config.Text = "Cancelar";
            this.btn_cancel_config.UseVisualStyleBackColor = true;
            this.btn_cancel_config.Click += new System.EventHandler(this.btn_cancel_config_Click);
            // 
            // chk_web_difusion
            // 
            this.chk_web_difusion.AutoSize = true;
            this.chk_web_difusion.Location = new System.Drawing.Point(178, 40);
            this.chk_web_difusion.Name = "chk_web_difusion";
            this.chk_web_difusion.Size = new System.Drawing.Size(107, 21);
            this.chk_web_difusion.TabIndex = 8;
            this.chk_web_difusion.Text = "Difusão web";
            this.chk_web_difusion.UseVisualStyleBackColor = true;
            // 
            // lbl_frame_rate
            // 
            this.lbl_frame_rate.AutoSize = true;
            this.lbl_frame_rate.Location = new System.Drawing.Point(18, 206);
            this.lbl_frame_rate.Name = "lbl_frame_rate";
            this.lbl_frame_rate.Size = new System.Drawing.Size(156, 17);
            this.lbl_frame_rate.TabIndex = 9;
            this.lbl_frame_rate.Text = "Frame rate [1 - 60 FPS]";
            // 
            // bar_frame_rate
            // 
            this.bar_frame_rate.LargeChange = 1;
            this.bar_frame_rate.Location = new System.Drawing.Point(21, 235);
            this.bar_frame_rate.Maximum = 60;
            this.bar_frame_rate.Minimum = 1;
            this.bar_frame_rate.Name = "bar_frame_rate";
            this.bar_frame_rate.Size = new System.Drawing.Size(417, 56);
            this.bar_frame_rate.SmallChange = 5;
            this.bar_frame_rate.TabIndex = 10;
            this.bar_frame_rate.Value = 1;
            this.bar_frame_rate.ValueChanged += new System.EventHandler(this.bar_frame_rate_ValueChanged);
            // 
            // bar_bit_rate
            // 
            this.bar_bit_rate.LargeChange = 100;
            this.bar_bit_rate.Location = new System.Drawing.Point(21, 331);
            this.bar_bit_rate.Maximum = 8000;
            this.bar_bit_rate.Minimum = 100;
            this.bar_bit_rate.Name = "bar_bit_rate";
            this.bar_bit_rate.Size = new System.Drawing.Size(417, 56);
            this.bar_bit_rate.SmallChange = 5;
            this.bar_bit_rate.TabIndex = 12;
            this.bar_bit_rate.TickFrequency = 100;
            this.bar_bit_rate.Value = 100;
            this.bar_bit_rate.ValueChanged += new System.EventHandler(this.bar_bit_rate_ValueChanged);
            // 
            // lbl_bit_rate
            // 
            this.lbl_bit_rate.AutoSize = true;
            this.lbl_bit_rate.Location = new System.Drawing.Point(18, 302);
            this.lbl_bit_rate.Name = "lbl_bit_rate";
            this.lbl_bit_rate.Size = new System.Drawing.Size(202, 17);
            this.lbl_bit_rate.TabIndex = 11;
            this.lbl_bit_rate.Text = "Taxa de bits [100 - 8000 Kbps]";
            // 
            // chk_date_time
            // 
            this.chk_date_time.AutoSize = true;
            this.chk_date_time.Location = new System.Drawing.Point(28, 41);
            this.chk_date_time.Name = "chk_date_time";
            this.chk_date_time.Size = new System.Drawing.Size(141, 21);
            this.chk_date_time.TabIndex = 13;
            this.chk_date_time.Text = "Exibir data e hora";
            this.chk_date_time.UseVisualStyleBackColor = true;
            this.chk_date_time.CheckedChanged += new System.EventHandler(this.chk_date_time_CheckedChanged);
            // 
            // text_align
            // 
            this.text_align.Controls.Add(this.cmb_font);
            this.text_align.Controls.Add(this.lbl_font);
            this.text_align.Controls.Add(this.lbl_align);
            this.text_align.Controls.Add(this.chk_botton_right);
            this.text_align.Controls.Add(this.chk_botton_left);
            this.text_align.Controls.Add(this.chk_top_right);
            this.text_align.Controls.Add(this.chk_top_left);
            this.text_align.Controls.Add(this.pictureBox1);
            this.text_align.Controls.Add(this.chk_date_time);
            this.text_align.Location = new System.Drawing.Point(12, 298);
            this.text_align.Name = "text_align";
            this.text_align.Size = new System.Drawing.Size(446, 221);
            this.text_align.TabIndex = 14;
            this.text_align.TabStop = false;
            this.text_align.Text = "Legenda";
            // 
            // cmb_font
            // 
            this.cmb_font.Enabled = false;
            this.cmb_font.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_font.FormattingEnabled = true;
            this.cmb_font.Location = new System.Drawing.Point(236, 115);
            this.cmb_font.Name = "cmb_font";
            this.cmb_font.Size = new System.Drawing.Size(102, 28);
            this.cmb_font.TabIndex = 23;
            // 
            // lbl_font
            // 
            this.lbl_font.AutoSize = true;
            this.lbl_font.Enabled = false;
            this.lbl_font.Location = new System.Drawing.Point(233, 84);
            this.lbl_font.Name = "lbl_font";
            this.lbl_font.Size = new System.Drawing.Size(104, 17);
            this.lbl_font.TabIndex = 22;
            this.lbl_font.Text = "Tamanho fonte";
            // 
            // lbl_align
            // 
            this.lbl_align.AutoSize = true;
            this.lbl_align.Enabled = false;
            this.lbl_align.Location = new System.Drawing.Point(28, 84);
            this.lbl_align.Name = "lbl_align";
            this.lbl_align.Size = new System.Drawing.Size(86, 17);
            this.lbl_align.TabIndex = 19;
            this.lbl_align.Text = "Alinhamento";
            // 
            // chk_botton_right
            // 
            this.chk_botton_right.AutoSize = true;
            this.chk_botton_right.Enabled = false;
            this.chk_botton_right.Location = new System.Drawing.Point(135, 180);
            this.chk_botton_right.Name = "chk_botton_right";
            this.chk_botton_right.Size = new System.Drawing.Size(18, 17);
            this.chk_botton_right.TabIndex = 18;
            this.chk_botton_right.UseVisualStyleBackColor = true;
            this.chk_botton_right.Click += new System.EventHandler(this.chk_botton_right_Click);
            // 
            // chk_botton_left
            // 
            this.chk_botton_left.AutoSize = true;
            this.chk_botton_left.Enabled = false;
            this.chk_botton_left.Location = new System.Drawing.Point(35, 181);
            this.chk_botton_left.Name = "chk_botton_left";
            this.chk_botton_left.Size = new System.Drawing.Size(18, 17);
            this.chk_botton_left.TabIndex = 17;
            this.chk_botton_left.UseVisualStyleBackColor = true;
            this.chk_botton_left.Click += new System.EventHandler(this.chk_botton_left_Click);
            // 
            // chk_top_right
            // 
            this.chk_top_right.AutoSize = true;
            this.chk_top_right.Enabled = false;
            this.chk_top_right.Location = new System.Drawing.Point(135, 115);
            this.chk_top_right.Name = "chk_top_right";
            this.chk_top_right.Size = new System.Drawing.Size(18, 17);
            this.chk_top_right.TabIndex = 16;
            this.chk_top_right.UseVisualStyleBackColor = true;
            this.chk_top_right.Click += new System.EventHandler(this.chk_top_right_Click);
            // 
            // chk_top_left
            // 
            this.chk_top_left.AutoSize = true;
            this.chk_top_left.Enabled = false;
            this.chk_top_left.Location = new System.Drawing.Point(35, 115);
            this.chk_top_left.Name = "chk_top_left";
            this.chk_top_left.Size = new System.Drawing.Size(18, 17);
            this.chk_top_left.TabIndex = 15;
            this.chk_top_left.UseVisualStyleBackColor = true;
            this.chk_top_left.Click += new System.EventHandler(this.chk_top_left_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = global::MultiCam.Properties.Resources.image1;
            this.pictureBox1.Location = new System.Drawing.Point(28, 110);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(130, 94);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_day);
            this.groupBox1.Controls.Add(this.lbl_month);
            this.groupBox1.Controls.Add(this.lbl_year);
            this.groupBox1.Controls.Add(this.bar_folder_name_precision);
            this.groupBox1.Controls.Add(this.lbl_forlder_name);
            this.groupBox1.Controls.Add(this.chk_separate_registers);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_search_dir);
            this.groupBox1.Controls.Add(this.txt_dir);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 286);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Diretórios";
            // 
            // lbl_day
            // 
            this.lbl_day.AutoSize = true;
            this.lbl_day.Enabled = false;
            this.lbl_day.Location = new System.Drawing.Point(208, 244);
            this.lbl_day.Name = "lbl_day";
            this.lbl_day.Size = new System.Drawing.Size(29, 17);
            this.lbl_day.TabIndex = 27;
            this.lbl_day.Text = "Dia";
            // 
            // lbl_month
            // 
            this.lbl_month.AutoSize = true;
            this.lbl_month.Enabled = false;
            this.lbl_month.Location = new System.Drawing.Point(119, 244);
            this.lbl_month.Name = "lbl_month";
            this.lbl_month.Size = new System.Drawing.Size(34, 17);
            this.lbl_month.TabIndex = 26;
            this.lbl_month.Text = "Mês";
            // 
            // lbl_year
            // 
            this.lbl_year.AutoSize = true;
            this.lbl_year.Enabled = false;
            this.lbl_year.Location = new System.Drawing.Point(28, 244);
            this.lbl_year.Name = "lbl_year";
            this.lbl_year.Size = new System.Drawing.Size(33, 17);
            this.lbl_year.TabIndex = 25;
            this.lbl_year.Text = "Ano";
            // 
            // bar_folder_name_precision
            // 
            this.bar_folder_name_precision.Enabled = false;
            this.bar_folder_name_precision.LargeChange = 1;
            this.bar_folder_name_precision.Location = new System.Drawing.Point(28, 185);
            this.bar_folder_name_precision.Margin = new System.Windows.Forms.Padding(0);
            this.bar_folder_name_precision.Maximum = 2;
            this.bar_folder_name_precision.Name = "bar_folder_name_precision";
            this.bar_folder_name_precision.Size = new System.Drawing.Size(209, 56);
            this.bar_folder_name_precision.TabIndex = 14;
            this.bar_folder_name_precision.Value = 2;
            this.bar_folder_name_precision.ValueChanged += new System.EventHandler(this.bar_folder_name_precision_ValueChanged);
            // 
            // lbl_forlder_name
            // 
            this.lbl_forlder_name.AutoSize = true;
            this.lbl_forlder_name.Enabled = false;
            this.lbl_forlder_name.Location = new System.Drawing.Point(270, 185);
            this.lbl_forlder_name.Name = "lbl_forlder_name";
            this.lbl_forlder_name.Size = new System.Drawing.Size(68, 17);
            this.lbl_forlder_name.TabIndex = 24;
            this.lbl_forlder_name.Text = "Nome Ex:";
            // 
            // chk_separate_registers
            // 
            this.chk_separate_registers.AutoSize = true;
            this.chk_separate_registers.Location = new System.Drawing.Point(28, 131);
            this.chk_separate_registers.Name = "chk_separate_registers";
            this.chk_separate_registers.Size = new System.Drawing.Size(209, 21);
            this.chk_separate_registers.TabIndex = 24;
            this.chk_separate_registers.Text = "Separar registros em pastas";
            this.chk_separate_registers.UseVisualStyleBackColor = true;
            this.chk_separate_registers.CheckedChanged += new System.EventHandler(this.Chk_separate_registers_CheckedChanged);
            // 
            // btn_search_dir
            // 
            this.btn_search_dir.Image = global::MultiCam.Properties.Resources.search_file1;
            this.btn_search_dir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_search_dir.Location = new System.Drawing.Point(298, 65);
            this.btn_search_dir.Margin = new System.Windows.Forms.Padding(5);
            this.btn_search_dir.Name = "btn_search_dir";
            this.btn_search_dir.Padding = new System.Windows.Forms.Padding(5);
            this.btn_search_dir.Size = new System.Drawing.Size(130, 43);
            this.btn_search_dir.TabIndex = 0;
            this.btn_search_dir.Text = "Procurar";
            this.btn_search_dir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_search_dir.UseVisualStyleBackColor = true;
            this.btn_search_dir.Click += new System.EventHandler(this.btn_search_dir_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_enable_compress);
            this.groupBox2.Controls.Add(this.chk_enable_timer);
            this.groupBox2.Controls.Add(this.bar_timer);
            this.groupBox2.Controls.Add(this.bar_bit_rate);
            this.groupBox2.Controls.Add(this.lbl_interval);
            this.groupBox2.Controls.Add(this.lbl_bit_rate);
            this.groupBox2.Controls.Add(this.chk_web_difusion);
            this.groupBox2.Controls.Add(this.lbl_frame_rate);
            this.groupBox2.Controls.Add(this.bar_frame_rate);
            this.groupBox2.Location = new System.Drawing.Point(478, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(463, 442);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opções de vídeo";
            // 
            // chk_enable_compress
            // 
            this.chk_enable_compress.AutoSize = true;
            this.chk_enable_compress.Location = new System.Drawing.Point(322, 40);
            this.chk_enable_compress.Name = "chk_enable_compress";
            this.chk_enable_compress.Size = new System.Drawing.Size(116, 21);
            this.chk_enable_compress.TabIndex = 13;
            this.chk_enable_compress.Text = "Compactação";
            this.chk_enable_compress.UseVisualStyleBackColor = true;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 533);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.text_align);
            this.Controls.Add(this.btn_cancel_config);
            this.Controls.Add(this.btn_ok_config);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações";
            ((System.ComponentModel.ISupportInitialize)(this.bar_timer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar_frame_rate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar_bit_rate)).EndInit();
            this.text_align.ResumeLayout(false);
            this.text_align.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar_folder_name_precision)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_search_dir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_dir;
        private System.Windows.Forms.Label lbl_interval;
        private System.Windows.Forms.TrackBar bar_timer;
        private System.Windows.Forms.CheckBox chk_enable_timer;
        private System.Windows.Forms.Button btn_ok_config;
        private System.Windows.Forms.Button btn_cancel_config;
        private System.Windows.Forms.CheckBox chk_web_difusion;
        private System.Windows.Forms.Label lbl_frame_rate;
        private System.Windows.Forms.TrackBar bar_frame_rate;
        private System.Windows.Forms.TrackBar bar_bit_rate;
        private System.Windows.Forms.Label lbl_bit_rate;
        private System.Windows.Forms.CheckBox chk_date_time;
        private System.Windows.Forms.GroupBox text_align;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chk_top_left;
        private System.Windows.Forms.CheckBox chk_botton_right;
        private System.Windows.Forms.CheckBox chk_botton_left;
        private System.Windows.Forms.CheckBox chk_top_right;
        private System.Windows.Forms.Label lbl_align;
        private System.Windows.Forms.CheckBox chk_enable_compress;
        private System.Windows.Forms.Label lbl_font;
        private System.Windows.Forms.ComboBox cmb_font;
        private System.Windows.Forms.Label lbl_forlder_name;
        private System.Windows.Forms.CheckBox chk_separate_registers;
        private System.Windows.Forms.Label lbl_day;
        private System.Windows.Forms.Label lbl_month;
        private System.Windows.Forms.Label lbl_year;
        private System.Windows.Forms.TrackBar bar_folder_name_precision;
    }
}