using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YT2013.UploadTool {
    internal static class Program {
        public static string currentVersion = "0.9.0";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if(Process.GetProcessesByName(Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1) {
                MessageBox.Show("Another instance is already running.", "FakeTube", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            Application.Run(new Form1());
        }
    }
}
