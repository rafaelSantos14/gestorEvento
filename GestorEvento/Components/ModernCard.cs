using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestorEvento.Components
{
    /// <summary>
    /// Card moderno com bordas arredondadas e sombra
    /// </summary>
    public class ModernCard : Panel
    {
        private int _borderRadius = 10;
        private Color _shadowColor = Color.FromArgb(50, 0, 0, 0);
        private int _shadowSize = 3;

        public ModernCard()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
        }

        public int BorderRadius
        {
            get { return _borderRadius; }
            set { _borderRadius = value; this.Invalidate(); }
        }

        public Color ShadowColor
        {
            get { return _shadowColor; }
            set { _shadowColor = value; this.Invalidate(); }
        }

        public int ShadowSize
        {
            get { return _shadowSize; }
            set { _shadowSize = value; this.Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(this.Parent.BackColor);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Desenhar sombra
            using (var shadowBrush = new SolidBrush(_shadowColor))
            {
                var shadowPath = GetRoundedRectangle(
                    new Rectangle(_shadowSize, _shadowSize, this.Width - _shadowSize - 1, this.Height - _shadowSize - 1),
                    _borderRadius);
                e.Graphics.FillPath(shadowBrush, shadowPath);
            }

            // Desenhar fundo com bordas arredondadas
            using (var bgBrush = new SolidBrush(this.BackColor))
            {
                var bgPath = GetRoundedRectangle(
                    new Rectangle(0, 0, this.Width - _shadowSize - 1, this.Height - _shadowSize - 1),
                    _borderRadius);
                e.Graphics.FillPath(bgBrush, bgPath);
            }

            // Desenhar borda sutil
            using (var borderPen = new Pen(Color.FromArgb(200, 200, 200), 0.5f))
            {
                var borderPath = GetRoundedRectangle(
                    new Rectangle(0, 0, this.Width - _shadowSize - 1, this.Height - _shadowSize - 1),
                    _borderRadius);
                e.Graphics.DrawPath(borderPen, borderPath);
            }

            base.OnPaint(e);
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedRectangle(Rectangle bounds, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int diameter = radius * 2;

            if (diameter > bounds.Width)
                diameter = bounds.Width;
            if (diameter > bounds.Height)
                diameter = bounds.Height;

            var arc = new Rectangle(bounds.Location, new Size(diameter, diameter));

            // Top-left
            path.AddArc(arc, 180, 90);

            // Top-right
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom-right
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom-left
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}
