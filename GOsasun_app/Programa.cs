using System;
using System.Windows.Forms;

namespace GOsasun_WinForms
{
    internal static class Programa
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new SaioaHasiFormularioa());
        }
    }
}