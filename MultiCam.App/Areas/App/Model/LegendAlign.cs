using System;
using System.Drawing;
using System.Windows.Forms;

using MultiCam.Model.Enums;

namespace MultiCam.Model
{
    public class Legend
    {
        private Point _location;
        private Size _size;
        private Font _font;
        public Legend(ELegendAlign align, Font font, Size resolution){
            this._size = resolution;
            Update(align,font);
        }
        public void Update(ELegendAlign align, Font font)
        {
            if (_location == null || _size == null)
                return;
            
            this._font = font;

            var text = TextRenderer.MeasureText(DateTime.Now.ToString(Consts.FORMAT_DATE_TIME), font);

            var width = text.Width;
            var height = text.Height;

            switch (align)
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
            using (Graphics g = Graphics.FromImage(img))
                TextRenderer.DrawText(g, DateTime.Now.ToString(Consts.FORMAT_DATE_TIME), _font, _location, Color.White, Color.Black);
        }
    }
}
