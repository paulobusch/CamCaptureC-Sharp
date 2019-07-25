using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace MultiCam
{
    public static class Helpers
    {
        /// <summary>
        /// Convert image into byte array
        /// </summary>
        /// <param name="obj">Image by convert</param>
        /// <returns></returns>
        public static byte[] ToByteArray(Image obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                obj.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        
        /// <summary>
        /// Calculate dimension by display grid
        /// </summary>
        /// <param name="tam">original size</param>
        /// <param name="porcent">porcetage</param>
        /// <returns>Size value</returns>
        public static int ProportionalWidth(int tam, int porcent) => (tam - 5) * porcent / 100;

        /// <summary>
        /// Set Auto initialization
        /// </summary>
        /// <param name="OnOff">New value</param>
        /// <returns></returns>
        public static bool SetStartup(bool OnOff)
        {
            try
            {
                string appName = "Câmeras de segurança";
                string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                RegistryKey startupKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

                if (OnOff)
                {
                    if (startupKey.GetValue(appName) == null)
                    {
                        startupKey.SetValue(appName, @"""" + Application.ExecutablePath.ToString() + @"""");
                        startupKey.Close();
                    }
                } else { 
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