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

            using (var wc = new WebClient()) {
                string latestVer = wc.DownloadString("https://yt2013.secretclub.cloud/Application/GetLatestRevision.ashx");

                if(!latestVer.Contains("2.")) {
                    if (latestVer == currentVersion) {
                        Application.Run(new Form1());
                    }
                    else {
                        if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FakeTube")) {
                            new Thread(() => {
                                Thread.CurrentThread.IsBackground = true;

                                MessageBox.Show("A new version is available and has been placed on the test branch and will launch shortly.\nPlease don't attempt to launch another instance", "FakeTube", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }).Start();

                            YT2013.UpdaterExtensions.Class1.Update();
                        }

                        Application.Run(new Form1());
                    }
                }
                else {
                    MessageBox.Show("This application's Windows Installer configuration needs to be updated and the updater doesn't have the necessary permissions to do so. Please redownload and reinstall the installer package from #upload-videos in order to use this tool.");
                }
            }
        }
    }
}
