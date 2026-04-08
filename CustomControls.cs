using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MSSQLServerTest
{
    /// <summary>
    /// Panel that paints a smooth horizontal linear gradient.
    /// Set GradientStart / GradientEnd and call Invalidate() to refresh.
    /// </summary>
    public sealed class GradientPanel : Panel
    {
        public Color GradientStart { get; set; } = Color.FromArgb(28, 100, 175);
        public Color GradientEnd   { get; set; } = Color.FromArgb(16, 60, 100);

        public GradientPanel()
        {
            // Double-buffer the panel to eliminate flicker during resize / theme switch
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint  |
                     ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width <= 0 || Height <= 0) return;

            using var brush = new LinearGradientBrush(
                ClientRectangle, GradientStart, GradientEnd,
                LinearGradientMode.Horizontal);

            e.Graphics.FillRectangle(brush, ClientRectangle);

            // Let child controls paint over the gradient
            base.OnPaint(e);
        }
    }
}
