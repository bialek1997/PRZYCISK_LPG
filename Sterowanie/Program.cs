using System;
using System.Windows.Forms;

namespace Sterowanie
{
    internal static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CAN.DoObiektu(0);
            Application.Run(new SterowanieForm());
        }
    }
}
