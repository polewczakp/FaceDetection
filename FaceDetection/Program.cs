using System;
using System.Windows.Forms;

namespace FaceDetection
{
    static class Program
    {
        public static MainWindow form1;
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form1 = new MainWindow();
            Application.Run(form1);
        }
    }
}
