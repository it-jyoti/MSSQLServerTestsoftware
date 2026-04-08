using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MSSQLServerTest
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // ── Splash screen ─────────────────────────────────────────────────
            using var splash = new SplashForm();
            splash.Show();
            splash.Refresh();

            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < 2500)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
            splash.Close();

            // ── Main form ─────────────────────────────────────────────────────
            Application.Run(new Form1());
        }
    }
}
