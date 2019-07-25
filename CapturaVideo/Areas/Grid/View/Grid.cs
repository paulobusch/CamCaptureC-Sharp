using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using MultiCam.Grid.Controller;
using MultiCam.Model;

namespace MultiCam.Grid.View
{
    public interface IGridView : IView, FrameObservable
    {
        void PrepareImages(IEnumerable<VideoCapture> videosCapture);
    }
    public partial class GridForm : Form, IGridView
    {
        private readonly IGridController _controller;
        private Dictionary<int,PictureBox> _images;

        #region Constructor
        public GridForm(IGridController controller)
        {
            _controller = controller;
            _images = new Dictionary<int, PictureBox>();

            InitializeComponent();
        }
        #endregion

        #region Grid
        private int CalcHeigt(int width) => width / 4 * 3;
        public void PrepareImages(IEnumerable<VideoCapture> videosCapture)
        {
            var width_image = ResetColumns(1);
            var heigt_image = CalcHeigt(width_image);
            var heigt_grid = tbl_grid.Height;

            if(heigt_image > heigt_grid)
                heigt_image = heigt_grid;

            for (int i = 0; i < videosCapture.Count(); i++)
            {
                var videoCapture = videosCapture.ElementAt(i);
                var image = new PictureBox()
                {
                    Image = (Image)videoCapture.CurrentFrame.Clone(),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Fill,
                    Height = heigt_image
                };

                tbl_grid.RowCount++;
                tbl_grid.RowStyles.Add(new RowStyle(SizeType.Absolute, heigt_image));
                tbl_grid.Controls.Add(image, 0, i);

                _images.Add(videoCapture.Id, image);
                videoCapture.Subscribe(this);
            }
        }
        private int ResetColumns(int columns)
        {
            while(tbl_grid.RowCount > 0) {
                tbl_grid.RowCount--;
                tbl_grid.RowStyles.RemoveAt(tbl_grid.RowCount);
            }

            tbl_grid.Controls.Clear();
            tbl_grid.ColumnStyles.Clear();
            tbl_grid.RowStyles.Clear();

            //tbl_grid.RowStyles;

            tbl_grid.ColumnCount = columns;

            int width_image = (tbl_grid.Width - 20) / columns;

            for (int i = 0; i < columns; i++)
                tbl_grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, width_image));

            return width_image;
        }
        public void UpdateFrame(int id, Image img)
        {
            lock (_images[id].Image)
            {
                _images[id].Image.Dispose();
                _images[id].Image = img;
            }
        }
        private void GridForm_Load(object sender, System.EventArgs e)
        {
            _controller.SetView(this);
        }
        #endregion

        #region Buttons 
        private void Btn_1x1_Click(object sender, System.EventArgs e)
        {
            var width_image = ResetColumns(1);
            var heigt_image = CalcHeigt(width_image);
            var heigt_grid = tbl_grid.Height;

            if (heigt_image > heigt_grid)
                heigt_image = heigt_grid;

            for (int i = 0; i < _images.Count(); i++)
            {
                var image = _images.ElementAt(i);
                tbl_grid.RowCount = i+1;
                image.Value.Height = heigt_image;

                tbl_grid.RowStyles.Add(new RowStyle(SizeType.Absolute, heigt_image));
                tbl_grid.Controls.Add(image.Value, 0, i);
            }
        }
        private void Btn_4x4_Click(object sender, System.EventArgs e)
        {
            var columns = 2;
            var width_image = ResetColumns(columns);
            var heigt_image = CalcHeigt(width_image);
            
            for (int i = 0; i < _images.Count(); i++)
            {
                var image = _images.ElementAt(i);
                image.Value.Height = heigt_image;
                image.Value.Width = width_image;
            
                if (i % columns == 0)
                {
                    tbl_grid.RowCount++;
                    tbl_grid.RowStyles.Add(new RowStyle(SizeType.Absolute, heigt_image));
                }
                tbl_grid.Controls.Add(image.Value, i % columns, tbl_grid.RowCount - 1);
            }

            //TODO: Resolve Bug
            ResetColumns(columns);
            for (int i = 0; i < _images.Count(); i++)
            {
                var image = _images.ElementAt(i);
                image.Value.Height = heigt_image;
                image.Value.Width = width_image;

                if (i % columns == 0)
                {
                    tbl_grid.RowCount++;
                    tbl_grid.RowStyles.Add(new RowStyle(SizeType.Absolute, heigt_image));
                }
                tbl_grid.Controls.Add(image.Value, i % columns, tbl_grid.RowCount - 1);
            }
        }
        #endregion

        #region Close
        private void GridForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _controller.Exit();
        }
        #endregion
    }
}
