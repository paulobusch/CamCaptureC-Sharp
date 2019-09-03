namespace MultiCam.Grid.View
{
    partial class GridForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridForm));
            this.tbl_grid = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_1x1 = new System.Windows.Forms.Button();
            this.btn_4x4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbl_grid
            // 
            this.tbl_grid.AutoScroll = true;
            this.tbl_grid.AutoSize = true;
            this.tbl_grid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tbl_grid.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tbl_grid.ColumnCount = 1;
            this.tbl_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_grid.Location = new System.Drawing.Point(0, 0);
            this.tbl_grid.Name = "tbl_grid";
            this.tbl_grid.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.tbl_grid.RowCount = 1;
            this.tbl_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_grid.Size = new System.Drawing.Size(1324, 765);
            this.tbl_grid.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_1x1);
            this.splitContainer1.Panel1.Controls.Add(this.btn_4x4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbl_grid);
            this.splitContainer1.Size = new System.Drawing.Size(1324, 845);
            this.splitContainer1.SplitterDistance = 76;
            this.splitContainer1.TabIndex = 5;
            // 
            // btn_1x1
            // 
            this.btn_1x1.Image = global::MultiCam.Properties.Resources._1x1;
            this.btn_1x1.Location = new System.Drawing.Point(12, 12);
            this.btn_1x1.Name = "btn_1x1";
            this.btn_1x1.Size = new System.Drawing.Size(68, 58);
            this.btn_1x1.TabIndex = 0;
            this.btn_1x1.UseVisualStyleBackColor = true;
            this.btn_1x1.Click += new System.EventHandler(this.Btn_1x1_Click);
            // 
            // btn_4x4
            // 
            this.btn_4x4.Image = global::MultiCam.Properties.Resources.grid;
            this.btn_4x4.Location = new System.Drawing.Point(86, 12);
            this.btn_4x4.Name = "btn_4x4";
            this.btn_4x4.Size = new System.Drawing.Size(68, 58);
            this.btn_4x4.TabIndex = 1;
            this.btn_4x4.UseVisualStyleBackColor = true;
            this.btn_4x4.Click += new System.EventHandler(this.Btn_4x4_Click);
            // 
            // GridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 845);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GridForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Todas as Câmeras";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GridForm_FormClosed);
            this.Load += new System.EventHandler(this.GridForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tbl_grid;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_4x4;
        private System.Windows.Forms.Button btn_1x1;
    }
}