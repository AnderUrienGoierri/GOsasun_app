using System;
using System.Windows.Forms;

namespace GOsasun_app
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            Application.Run(new GOsasun_app.Formularioak.SaioaHasiFormularioa());
        }
    }
}