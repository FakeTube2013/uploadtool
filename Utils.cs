using System;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;   

namespace YT2013.UploadTool {
    internal class Utils {
        internal static string GetSHA256(string text) {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create()) {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(text));

                foreach (Byte b in result) {
                    Sb.Append(b.ToString("x2"));
                }
            }

            return Sb.ToString();
        }

        internal static bool DebugMode = Debugger.IsAttached;

        internal static string TestDomain = "https://localhost:44356";
        //internal static string TestDomain = "http://192.168.1.177:553/";
    }
}