using CapturaVideo.Model;

namespace CapturaVideo
{
    partial class MultipleCaptureForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultipleCaptureForm));
            this.cmb_device = new System.Windows.Forms.ComboBox();
            this.menu_view_image = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tool_strip_configure = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_strip_start_font = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_strip_stop_font = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_strip_start_video = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_strip_stop_video = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_strip_capture = new System.Windows.Forms.ToolStripMenuItem();
            this.cmb_resolution = new System.Windows.Forms.ComboBox();
            this.menu_primary = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracoesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inicializaçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_start_window = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_start_window_minimized = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.video_interval = new System.Windows.Forms.Timer(this.components);
            this.icon_notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_link = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.list_view_devices = new System.Windows.Forms.ListView();
            this.btn_tools = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_remove = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.box_image = new System.Windows.Forms.GroupBox();
            this.btn_font_start = new System.Windows.Forms.Button();
            this.btn_font_stop = new System.Windows.Forms.Button();
            this.btn_video_start = new System.Windows.Forms.Button();
            this.btn_video_stop = new System.Windows.Forms.Button();
            this.image_state = new System.Windows.Forms.PictureBox();
            this.image_grid = new System.Windows.Forms.PictureBox();
            this.menu_view_image.SuspendLayout();
            this.menu_primary.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.box_image.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image_state)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.image_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb_device
            // 
            this.cmb_device.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_device.FormattingEnabled = true;
            this.cmb_device.Location = new System.Drawing.Point(21, 63);
            this.cmb_device.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_device.Name = "cmb_device";
            this.cmb_device.Size = new System.Drawing.Size(229, 24);
            this.cmb_device.TabIndex = 5;
            this.cmb_device.SelectedIndexChanged += new System.EventHandler(this.cmb_device_SelectedIndexChanged);
            // 
            // menu_view_image
            // 
            this.menu_view_image.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu_view_image.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_strip_configure,
            this.toolStripSeparator1,
            this.tool_strip_start_font,
            this.tool_strip_stop_font,
            this.toolStripSeparator2,
            this.tool_strip_start_video,
            this.tool_strip_stop_video,
            this.toolStripSeparator3,
            this.tool_strip_capture});
            this.menu_view_image.Name = "view_image";
            this.menu_view_image.Size = new System.Drawing.Size(192, 178);
            this.menu_view_image.Opening += new System.ComponentModel.CancelEventHandler(this.menu_view_image_Opening);
            // 
            // tool_strip_configure
            // 
            this.tool_strip_configure.Image = global::CapturaVideo.Properties.Resources.tools;
            this.tool_strip_configure.Name = "tool_strip_configure";
            this.tool_strip_configure.Size = new System.Drawing.Size(191, 26);
            this.tool_strip_configure.Text = "Configurar fonte";
            this.tool_strip_configure.Click += new System.EventHandler(this.tool_strip_configure_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
            // 
            // tool_strip_start_font
            // 
            this.tool_strip_start_font.Image = global::CapturaVideo.Properties.Resources.cam_start;
            this.tool_strip_start_font.Name = "tool_strip_start_font";
            this.tool_strip_start_font.Size = new System.Drawing.Size(191, 26);
            this.tool_strip_start_font.Text = "Iniciar fonte";
            this.tool_strip_start_font.Click += new System.EventHandler(this.tool_strip_start_font_Click);
            // 
            // tool_strip_stop_font
            // 
            this.tool_strip_stop_font.Image = global::CapturaVideo.Properties.Resources.cam_stop;
            this.tool_strip_stop_font.Name = "tool_strip_stop_font";
            this.tool_strip_stop_font.Size = new System.Drawing.Size(191, 26);
            this.tool_strip_stop_font.Text = "Parar fonte";
            this.tool_strip_stop_font.Click += new System.EventHandler(this.tool_strip_stop_font_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(188, 6);
            // 
            // tool_strip_start_video
            // 
            this.tool_strip_start_video.Image = global::CapturaVideo.Properties.Resources.video_start;
            this.tool_strip_start_video.Name = "tool_strip_start_video";
            this.tool_strip_start_video.Size = new System.Drawing.Size(191, 26);
            this.tool_strip_start_video.Text = "Iniciar vídeo";
            this.tool_strip_start_video.Click += new System.EventHandler(this.tool_strip_start_video_Click);
            // 
            // tool_strip_stop_video
            // 
            this.tool_strip_stop_video.Image = global::CapturaVideo.Properties.Resources.video_stop;
            this.tool_strip_stop_video.Name = "tool_strip_stop_video";
            this.tool_strip_stop_video.Size = new System.Drawing.Size(191, 26);
            this.tool_strip_stop_video.Text = "Parar vídeo";
            this.tool_strip_stop_video.Click += new System.EventHandler(this.tool_strip_stop_video_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(188, 6);
            // 
            // tool_strip_capture
            // 
            this.tool_strip_capture.Image = global::CapturaVideo.Properties.Resources.capture;
            this.tool_strip_capture.Name = "tool_strip_capture";
            this.tool_strip_capture.Size = new System.Drawing.Size(191, 26);
            this.tool_strip_capture.Text = "Capturar foto";
            this.tool_strip_capture.Click += new System.EventHandler(this.tool_strip_capture_Click);
            // 
            // cmb_resolution
            // 
            this.cmb_resolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_resolution.FormattingEnabled = true;
            this.cmb_resolution.Location = new System.Drawing.Point(267, 63);
            this.cmb_resolution.Name = "cmb_resolution";
            this.cmb_resolution.Size = new System.Drawing.Size(126, 24);
            this.cmb_resolution.TabIndex = 6;
            this.cmb_resolution.SelectedIndexChanged += new System.EventHandler(this.cmb_resolution_SelectedIndexChanged);
            // 
            // menu_primary
            // 
            this.menu_primary.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu_primary.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.ajudaToolStripMenuItem});
            this.menu_primary.Location = new System.Drawing.Point(0, 0);
            this.menu_primary.Name = "menu_primary";
            this.menu_primary.Size = new System.Drawing.Size(1125, 28);
            this.menu_primary.TabIndex = 20;
            this.menu_primary.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuracoesToolStripMenuItem,
            this.inicializaçãoToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(73, 24);
            this.toolStripMenuItem1.Text = "Arquivo";
            // 
            // configuracoesToolStripMenuItem
            // 
            this.configuracoesToolStripMenuItem.Name = "configuracoesToolStripMenuItem";
            this.configuracoesToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.configuracoesToolStripMenuItem.Text = "Configurações";
            this.configuracoesToolStripMenuItem.Click += new System.EventHandler(this.configuracoesToolStripMenuItem_Click);
            // 
            // inicializaçãoToolStripMenuItem
            // 
            this.inicializaçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_start_window,
            this.mnu_start_window_minimized});
            this.inicializaçãoToolStripMenuItem.Name = "inicializaçãoToolStripMenuItem";
            this.inicializaçãoToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.inicializaçãoToolStripMenuItem.Text = "Inicialização";
            // 
            // mnu_start_window
            // 
            this.mnu_start_window.Name = "mnu_start_window";
            this.mnu_start_window.Size = new System.Drawing.Size(232, 26);
            this.mnu_start_window.Text = "Iniciar com o windows";
            this.mnu_start_window.Click += new System.EventHandler(this.mnu_start_window_Click);
            // 
            // mnu_start_window_minimized
            // 
            this.mnu_start_window_minimized.Name = "mnu_start_window_minimized";
            this.mnu_start_window_minimized.Size = new System.Drawing.Size(232, 26);
            this.mnu_start_window_minimized.Text = "Iniciar minimizado";
            this.mnu_start_window_minimized.Click += new System.EventHandler(this.mnu_start_window_minimized_Click);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // ajudaToolStripMenuItem
            // 
            this.ajudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sobreToolStripMenuItem});
            this.ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            this.ajudaToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.ajudaToolStripMenuItem.Text = "Ajuda";
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(123, 26);
            this.sobreToolStripMenuItem.Text = "Sobre";
            this.sobreToolStripMenuItem.Click += new System.EventHandler(this.sobreToolStripMenuItem_Click);
            // 
            // video_interval
            // 
            this.video_interval.Interval = 300000;
            this.video_interval.Tick += new System.EventHandler(this.video_interval_Tick);
            // 
            // icon_notify
            // 
            this.icon_notify.Icon = ((System.Drawing.Icon)(resources.GetObject("icon_notify.Icon")));
            this.icon_notify.Text = "Câmeras";
            this.icon_notify.Visible = true;
            this.icon_notify.DoubleClick += new System.EventHandler(this.icon_notify_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_link);
            this.groupBox1.Controls.Add(this.btn_add);
            this.groupBox1.Controls.Add(this.list_view_devices);
            this.groupBox1.Controls.Add(this.btn_tools);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_remove);
            this.groupBox1.Controls.Add(this.cmb_device);
            this.groupBox1.Controls.Add(this.cmb_resolution);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(489, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 370);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controles";
            // 
            // lbl_link
            // 
            this.lbl_link.AutoSize = true;
            this.lbl_link.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_link.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_link.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbl_link.Location = new System.Drawing.Point(17, 339);
            this.lbl_link.Name = "lbl_link";
            this.lbl_link.Size = new System.Drawing.Size(34, 17);
            this.lbl_link.TabIndex = 18;
            this.lbl_link.Text = "Link";
            this.lbl_link.Visible = false;
            this.lbl_link.VisibleChanged += new System.EventHandler(this.lbl_link_VisibleChanged);
            this.lbl_link.Click += new System.EventHandler(this.lbl_link_Click);
            // 
            // btn_add
            // 
            this.btn_add.Image = global::CapturaVideo.Properties.Resources.include;
            this.btn_add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_add.Location = new System.Drawing.Point(414, 20);
            this.btn_add.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_add.Name = "btn_add";
            this.btn_add.Padding = new System.Windows.Forms.Padding(5);
            this.btn_add.Size = new System.Drawing.Size(157, 60);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "Adicionar";
            this.btn_add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // list_view_devices
            // 
            this.list_view_devices.ContextMenuStrip = this.menu_view_image;
            this.list_view_devices.FullRowSelect = true;
            this.list_view_devices.GridLines = true;
            this.list_view_devices.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.list_view_devices.Location = new System.Drawing.Point(21, 106);
            this.list_view_devices.MultiSelect = false;
            this.list_view_devices.Name = "list_view_devices";
            this.list_view_devices.ShowItemToolTips = true;
            this.list_view_devices.Size = new System.Drawing.Size(372, 220);
            this.list_view_devices.TabIndex = 7;
            this.list_view_devices.UseCompatibleStateImageBehavior = false;
            this.list_view_devices.View = System.Windows.Forms.View.Details;
            this.list_view_devices.SelectedIndexChanged += new System.EventHandler(this.list_view_devices_SelectedIndexChanged);
            // 
            // btn_tools
            // 
            this.btn_tools.Enabled = false;
            this.btn_tools.Image = global::CapturaVideo.Properties.Resources.tools;
            this.btn_tools.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_tools.Location = new System.Drawing.Point(415, 186);
            this.btn_tools.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_tools.Name = "btn_tools";
            this.btn_tools.Padding = new System.Windows.Forms.Padding(5);
            this.btn_tools.Size = new System.Drawing.Size(156, 60);
            this.btn_tools.TabIndex = 3;
            this.btn_tools.Text = "Configurar";
            this.btn_tools.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_tools.UseVisualStyleBackColor = true;
            this.btn_tools.Click += new System.EventHandler(this.btn_tools_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(263, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 24;
            this.label2.Text = "Resolução";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 17);
            this.label1.TabIndex = 23;
            this.label1.Text = "Câmeras disponíveis";
            // 
            // btn_remove
            // 
            this.btn_remove.Enabled = false;
            this.btn_remove.Image = global::CapturaVideo.Properties.Resources.remove;
            this.btn_remove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_remove.Location = new System.Drawing.Point(415, 266);
            this.btn_remove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Padding = new System.Windows.Forms.Padding(5);
            this.btn_remove.Size = new System.Drawing.Size(156, 60);
            this.btn_remove.TabIndex = 4;
            this.btn_remove.Text = "Remover";
            this.btn_remove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btn_save
            // 
            this.btn_save.Enabled = false;
            this.btn_save.Image = global::CapturaVideo.Properties.Resources.save;
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_save.Location = new System.Drawing.Point(415, 106);
            this.btn_save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_save.Name = "btn_save";
            this.btn_save.Padding = new System.Windows.Forms.Padding(5);
            this.btn_save.Size = new System.Drawing.Size(157, 60);
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "Salvar";
            this.btn_save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // box_image
            // 
            this.box_image.Controls.Add(this.btn_font_start);
            this.box_image.Controls.Add(this.btn_font_stop);
            this.box_image.Controls.Add(this.btn_video_start);
            this.box_image.Controls.Add(this.btn_video_stop);
            this.box_image.Controls.Add(this.image_state);
            this.box_image.Controls.Add(this.image_grid);
            this.box_image.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.box_image.Location = new System.Drawing.Point(12, 31);
            this.box_image.Name = "box_image";
            this.box_image.Size = new System.Drawing.Size(458, 370);
            this.box_image.TabIndex = 24;
            this.box_image.TabStop = false;
            this.box_image.Text = "Câmera";
            // 
            // btn_font_start
            // 
            this.btn_font_start.Image = global::CapturaVideo.Properties.Resources.cam_start;
            this.btn_font_start.Location = new System.Drawing.Point(27, 319);
            this.btn_font_start.Name = "btn_font_start";
            this.btn_font_start.Size = new System.Drawing.Size(90, 37);
            this.btn_font_start.TabIndex = 21;
            this.btn_font_start.UseVisualStyleBackColor = true;
            this.btn_font_start.Click += new System.EventHandler(this.btn_font_start_Click);
            // 
            // btn_font_stop
            // 
            this.btn_font_stop.Image = global::CapturaVideo.Properties.Resources.cam_stop;
            this.btn_font_stop.Location = new System.Drawing.Point(136, 319);
            this.btn_font_stop.Name = "btn_font_stop";
            this.btn_font_stop.Size = new System.Drawing.Size(90, 37);
            this.btn_font_stop.TabIndex = 20;
            this.btn_font_stop.UseVisualStyleBackColor = true;
            this.btn_font_stop.Click += new System.EventHandler(this.btn_font_stop_Click);
            // 
            // btn_video_start
            // 
            this.btn_video_start.Image = global::CapturaVideo.Properties.Resources.video_start;
            this.btn_video_start.Location = new System.Drawing.Point(242, 319);
            this.btn_video_start.Name = "btn_video_start";
            this.btn_video_start.Size = new System.Drawing.Size(90, 37);
            this.btn_video_start.TabIndex = 19;
            this.btn_video_start.UseVisualStyleBackColor = true;
            this.btn_video_start.Click += new System.EventHandler(this.btn_video_start_Click);
            // 
            // btn_video_stop
            // 
            this.btn_video_stop.Image = global::CapturaVideo.Properties.Resources.video_stop1;
            this.btn_video_stop.Location = new System.Drawing.Point(347, 319);
            this.btn_video_stop.Name = "btn_video_stop";
            this.btn_video_stop.Size = new System.Drawing.Size(90, 37);
            this.btn_video_stop.TabIndex = 18;
            this.btn_video_stop.UseVisualStyleBackColor = true;
            this.btn_video_stop.Click += new System.EventHandler(this.btn_video_stop_Click);
            // 
            // image_state
            // 
            this.image_state.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.image_state.Location = new System.Drawing.Point(408, 298);
            this.image_state.Name = "image_state";
            this.image_state.Size = new System.Drawing.Size(29, 10);
            this.image_state.TabIndex = 17;
            this.image_state.TabStop = false;
            // 
            // image_grid
            // 
            this.image_grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.image_grid.ContextMenuStrip = this.menu_view_image;
            this.image_grid.Location = new System.Drawing.Point(27, 30);
            this.image_grid.Name = "image_grid";
            this.image_grid.Size = new System.Drawing.Size(410, 278);
            this.image_grid.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.image_grid.TabIndex = 16;
            this.image_grid.TabStop = false;
            // 
            // MultipleCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1125, 413);
            this.Controls.Add(this.box_image);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menu_primary);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu_primary;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "MultipleCaptureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Câmeras de segurança";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MultipleCaptureForm_Load);
            this.menu_view_image.ResumeLayout(false);
            this.menu_primary.ResumeLayout(false);
            this.menu_primary.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.box_image.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.image_state)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.image_grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.ComboBox cmb_device;
        private System.Windows.Forms.PictureBox image_grid;
        private System.Windows.Forms.ComboBox cmb_resolution;
        private System.Windows.Forms.MenuStrip menu_primary;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem configuracoesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        private System.Windows.Forms.Timer video_interval;
        private System.Windows.Forms.NotifyIcon icon_notify;
        private System.Windows.Forms.ContextMenuStrip menu_view_image;
        private System.Windows.Forms.ToolStripMenuItem tool_strip_configure;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tool_strip_start_font;
        private System.Windows.Forms.ToolStripMenuItem tool_strip_stop_font;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tool_strip_start_video;
        private System.Windows.Forms.ToolStripMenuItem tool_strip_stop_video;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_tools;
        private System.Windows.Forms.GroupBox box_image;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tool_strip_capture;
        private System.Windows.Forms.ListView list_view_devices;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.PictureBox image_state;
        private System.Windows.Forms.ToolStripMenuItem inicializaçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnu_start_window;
        private System.Windows.Forms.ToolStripMenuItem mnu_start_window_minimized;
        private System.Windows.Forms.Label lbl_link;
        private System.Windows.Forms.Button btn_video_stop;
        private System.Windows.Forms.Button btn_font_start;
        private System.Windows.Forms.Button btn_font_stop;
        private System.Windows.Forms.Button btn_video_start;
    }
}

