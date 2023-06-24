using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace YT2013.UploadTool {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void OK_Click(object sender, EventArgs e) {
            this.Hide();

            using(var client = new HttpClient()) {
                var data = new Backend.Schemas.LoginSchema();

                data.Username = UsernameTextBox.Text;
                data.Password = Utils.GetSHA256(PasswordTextBox.Text);
                data.FromApp = true;

                var content = new StringContent(JsonConvert.SerializeObject(data));

                string uri = (Utils.DebugMode ? Utils.TestDomain : "https://yt2013.secretclub.cloud") + "/Accounts/LogIn.ashx";

                var response = client.PostAsync(uri, content).Result;

                var responseString = response.Content.ReadAsStringAsync().Result;

                var dec = JsonConvert.DeserializeObject<Backend.Schemas.LoginResponse>(responseString);

                if(dec.Status == "OK") {
                    new VideoDataPrompt(dec.Token).Show();
                }
                else {
                    MessageBox.Show(dec.Status, "Server Response");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e) {

        }
    }
}
