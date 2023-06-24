using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YT2013.UploadTool {
    public partial class VideoDataPrompt : Form {
        string Token = "";

        public VideoDataPrompt(string token) {
            InitializeComponent();

            Token = token;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FakeTube")) {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FakeTube");
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FakeTube\\videodataprompt.lic");
            }

            var dialog = new OpenFileDialog();

            dialog.Title = "Select file";
            dialog.Filter = "MPEG-4|*.mp4";
            dialog.DefaultExt = "mp4";

            if (dialog.ShowDialog() == DialogResult.OK) {
                MessageBox.Show("The client may seem to halt in the next stage, this is normal.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                using (var wc = new WebClient()) {
                    wc.Headers.Add("Authorization", Token);
                    wc.Headers.Add("VideoTitle", titleBox.Text);
                    wc.Headers.Add("VideoDescription", descBox.Text);

                    var rawResponse = wc.UploadFile((Utils.DebugMode ? Utils.TestDomain : "https://yt2013.secretclub.cloud") + "/Videos/UploadVideo.ashx", dialog.FileName);

                    string response = Encoding.ASCII.GetString(rawResponse);

                    var dec = JsonConvert.DeserializeObject<Backend.Schemas.UploadResponseSchema>(response);

                    Backend.Schemas.AccountData.Root account = null;

                    using (var wc2 = new WebClient()) {
                        wc2.Headers.Add("Authorization", Token);

                        account = JsonConvert.DeserializeObject<Backend.Schemas.AccountData.Root>(wc2.DownloadString((Utils.DebugMode ? Utils.TestDomain : "https://yt2013.secretclub.cloud") + "/Accounts/GetAccountDataPrivate.ashx"));
                    }

                    if(Utils.DebugMode) {
                        MessageBox.Show(response);
                    }

                    if(dec.Status == "OK") {
                        var videoUrl = "https://yt.secretclub.cloud/watch?v=" + dec.VideoID;

                        Clipboard.SetText(videoUrl);

                        MessageBox.Show($"Video upload successful.\nThe video URI is {videoUrl}\n\nThis link has been copied to your clipboard.", "Upload Complete");

                        Application.Exit();
                    }
                    else {
                        MessageBox.Show(dec.Status);
                        Application.Exit();
                    }
                }
            }
        }

        private void Form_Closing(object sender, FormClosingEventArgs e) {
            Application.Exit();
        }
    }
}
