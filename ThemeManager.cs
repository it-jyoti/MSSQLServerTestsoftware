using System.Drawing;
using System.Windows.Forms;

namespace MSSQLServerTest
{
    public enum AppTheme { Day, Night }

    /// <summary>Central palette for Day / Night themes.</summary>
    public static class ThemeManager
    {
        public static AppTheme Current { get; private set; } = AppTheme.Day;

        public static void Toggle() =>
            Current = Current == AppTheme.Day ? AppTheme.Night : AppTheme.Day;

        private static bool IsNight => Current == AppTheme.Night;

        // ── Backgrounds ──────────────────────────────────────────────────────
        public static Color FormBack        => IsNight ? Color.FromArgb(28, 28, 28)    : Color.FromArgb(240, 240, 240);
        public static Color TabBack         => IsNight ? Color.FromArgb(32, 32, 32)    : Color.FromArgb(245, 245, 245);
        public static Color InputBack       => IsNight ? Color.FromArgb(55, 55, 55)    : Color.White;
        public static Color HeaderBack      => IsNight ? Color.FromArgb(21, 47, 78)    : Color.SteelBlue;
        public static Color AccentColor     => IsNight ? Color.FromArgb(14, 99, 156)   : Color.SteelBlue;
        public static Color MenuBack        => IsNight ? Color.FromArgb(45, 45, 45)    : SystemColors.Control;
        public static Color ButtonSecBack   => IsNight ? Color.FromArgb(55, 55, 55)    : SystemColors.Control;
        public static Color DgvHeaderBack   => IsNight ? Color.FromArgb(20, 60, 100)   : Color.FromArgb(70, 130, 180);
        public static Color DgvAltRowBack   => IsNight ? Color.FromArgb(36, 36, 36)    : Color.FromArgb(245, 249, 255);
        public static Color DgvRowBack      => IsNight ? Color.FromArgb(28, 28, 28)    : Color.White;
        public static Color DgvSelectBack   => IsNight ? Color.FromArgb(30, 80, 130)   : Color.FromArgb(173, 216, 230);
        public static Color DgvSelectFore   => IsNight ? Color.White                   : Color.Black;
        public static Color StatusBarBack   => IsNight ? Color.FromArgb(40, 40, 40)    : SystemColors.Control;

        // ── Foregrounds ──────────────────────────────────────────────────────
        public static Color ForeColor       => IsNight ? Color.FromArgb(212, 212, 212) : Color.FromArgb(30, 30, 30);
        public static Color DisabledFore    => IsNight ? Color.FromArgb(100, 100, 100) : Color.Gray;
        public static Color MenuFore        => IsNight ? Color.FromArgb(212, 212, 212) : SystemColors.ControlText;
        public static Color ButtonSecFore   => IsNight ? Color.FromArgb(212, 212, 212) : SystemColors.ControlText;
        public static Color DgvHeaderFore   => Color.White;

        // ── Borders ──────────────────────────────────────────────────────────
        public static Color BorderColor     => IsNight ? Color.FromArgb(70, 70, 70)    : Color.LightGray;

        // ── Gradient header ──────────────────────────────────────────────────
        public static Color HeaderGradientStart => IsNight ? Color.FromArgb(12, 36, 62) : Color.FromArgb(28, 100, 175);
        public static Color HeaderGradientEnd   => IsNight ? Color.FromArgb(6,  20, 40) : Color.FromArgb(16, 60, 100);
    }
}
