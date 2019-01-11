using CapturaVideo.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CapturaVideo.Model
{
    public enum LegendAlign
    {
        TopRight = 0,
        TopLeft = 1,
        BottonRight = 2,
        BottonLeft = 3,
    }
    public class Legend
    {
        private Point _location;
        private Size _size;
        public Legend(Size resolution){
            _size = resolution;
            CalcPosition();
        }
        public void CalcPosition()
        {
            if (_location == null || _size == null)
                return;

            var text = TextRenderer.MeasureText(DateTime.Now.ToString(Consts.FORMAT_DATE_TIME), Configuration.font);

            var width = text.Width;
            var height = text.Height;

            switch (Configuration.legend_align)
            {
                case LegendAlign.TopLeft:
                    _location.X = Consts.LOCATION.X;
                    _location.Y = Consts.LOCATION.Y;
                    break;
                case LegendAlign.TopRight:
                    _location.X = _size.Width - (Consts.LOCATION.X + width);
                    _location.Y = Consts.LOCATION.Y;
                    break;
                case LegendAlign.BottonLeft:
                    _location.X = Consts.LOCATION.X;
                    _location.Y = _size.Height - (Consts.LOCATION.Y + height);
                    break;
                case LegendAlign.BottonRight:
                    _location.X = _size.Width - (Consts.LOCATION.X + width);
                    _location.Y = _size.Height - (Consts.LOCATION.Y + height);
                    break;
            }
        }
        public void WriteLegend(Bitmap img)
        {
            if (Configuration.show_date_time)
                using (Graphics g = Graphics.FromImage(img))
                    TextRenderer.DrawText(g, DateTime.Now.ToString(Consts.FORMAT_DATE_TIME), Configuration.font, _location, Color.White, Color.Black);
        }
    }
}
