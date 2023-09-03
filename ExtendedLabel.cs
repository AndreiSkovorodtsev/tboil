using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tboil_v20._10._21
{
    [System.ComponentModel.DesignerCategory("")] // чтобы дизайнер формы работал
    public class ExtendedLabel : Label
    {
        private const int WS_EX_TRANSPARENT = 0x20;

        public ExtendedLabel()
        {
            SetStyle(ControlStyles.Opaque, true);
        }

        private int _opacity; // прозрачность 

        public int Opacity
        {
            get { return this._opacity; }
            set
            {
                if (value < 0 || value > 100) // процентов от 0 до 100
                    throw new ArgumentException("Значение должно быть от 0 до 100");
                this._opacity = value;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Color.Black - вот он наш черный цвет панели полупрозрачный
            using (var brush = new SolidBrush(Color.FromArgb(this._opacity * 255 / 100, Color.Black)))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
            base.OnPaint(e);
        }
    }
}
