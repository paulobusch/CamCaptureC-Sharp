using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace WebServer.Models
{
    public static class Helpers
    {
        public static byte[] ToByteArray(Image obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                obj.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        public static bool SetStartup(bool OnOff)
        {
            try
            {
                string appName = "Câmeras de segurança";
                string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                RegistryKey startupKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (OnOff)//Iniciar
                {
                    if (startupKey.GetValue(appName) == null)
                    {
                        // Add startup reg key
                        startupKey.SetValue(appName, @"""" + Application.ExecutablePath.ToString() + @"""");
                        startupKey.Close();
                    }
                }
                else//Nao iniciar mais
                {
                    // remove startup
                    startupKey = Registry.LocalMachine.OpenSubKey(runKey, true);
                    startupKey.DeleteValue(appName, false);
                    startupKey.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }    
}