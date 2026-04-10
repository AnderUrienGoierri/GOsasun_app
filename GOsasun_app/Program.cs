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

            Application.ThreadException += (s, args) => MessageBox.Show($"Zerbait gaizki joan da:\n{args.Exception.Message}\n{args.Exception.StackTrace}", "Errorea UI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            AppDomain.CurrentDomain.UnhandledException += (s, args) => MessageBox.Show($"Zerbait gaizki joan da (Core):\n{((Exception)args.ExceptionObject).Message}\n{((Exception)args.ExceptionObject).StackTrace}", "Errorea N", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Application.Run(new GOsasun_app.Interfazea.SaioaHasi());
        }
    }
}