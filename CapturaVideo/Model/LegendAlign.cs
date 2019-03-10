using CapturaVideo.Model.Enums;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CapturaVideo.Model
{
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

            var text = TextRenderer.MeasureText(DateTime.Now.ToString(Consts.FORMAT_DATE_TIME), Configuration.Data.Font);

            var width = text.Width;
            var height = text.Height;

            switch (Configuration.Data.LegendAlign)
            {
                case ELegendAlign.TopLeft:
                    _location.X = Consts.LOCATION.X;
                    _location.Y = Consts.LOCATION.Y;
                    break;
                case ELegendAlign.TopRight:
                    _location.X = _size.Width - (Consts.LOCATION.X + width);
                    _location.Y = Consts.LOCATION.Y;
                    break;
                case ELegendAlign.BottonLeft:
                    _location.X = Consts.LOCATION.X;
                    _location.Y = _size.Height - (Consts.LOCATION.Y + height);
                    break;
                case ELegendAlign.BottonRight:
                    _location.X = _size.Width - (Consts.LOCATION.X + width);
                    _location.Y = _size.Height - (Consts.LOCATION.Y + height);
                    break;
            }
        }
        public void WriteLegend(Bitmap img)
        {
            if (Configuration.Data.ViewDateTime)
                using (Graphics g = Graphics.FromImage(img))
                    TextRenderer.DrawText(g, DateTime.Now.ToString(Consts.FORMAT_DATE_TIME), Configuration.Data.Font, _location, Color.White, Color.Black);
        }
    }
}
