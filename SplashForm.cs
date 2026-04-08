using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MSSQLServerTest
{
    /// <summary>
    /// Splash screen shown at application startup.
    /// Image: F1441.png  |  Size: 270 × 152 px
    /// </summary>
    internal sealed class SplashForm : Form
    {
        private readonly PictureBox _pic;

        internal SplashForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition   = FormStartPosition.CenterScreen;
            ClientSize      = new Size(270, 152);
            TopMost         = true;
            ShowInTaskbar   = false;
            BackColor       = Color.FromArgb(30, 30, 30);

            _pic = new PictureBox
            {
                Dock     = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // Look in output directory first, then the source directory
            string[] candidates =
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "F1441.png"),
                @"D:\TICKET\MS SQL Server Test\F1441.png"
            };

            foreach (var path in candidates)
            {
                if (File.Exists(path))
                {
                    _pic.Image = Image.FromFile(path);
                    break;
                }
            }

            Controls.Add(_pic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pic.Image?.Dispose();
                _pic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
