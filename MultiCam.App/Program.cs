using System;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Threading;
using CapturaVideo.Model;
using CapturaVideo.Model;
using System.Collections.Generic;
using System.Linq;

namespace CapturaVideo
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            NotifyIcon ni = new NotifyIcon()
            {
                Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                Text = "Câmera"
            };

            var ex = new LogException();
            Application.ThreadException += new ThreadExceptionEventHandler(ex.GenerateLog);

            Application.Run(new MultipleCaptureForm());
        }
    }
    internal class LogException
    {
        public void GenerateLog(object sender, ThreadExceptionEventArgs e)
        {

            var logStr = new List<string> {"",
                $"######################### LogException ##########################",
                $"DateTime: {DateTime.Now.ToString(Consts.FORMAT_DATE_TIME)}",
                $"Message: {e.Exception?.Message ?? "[null]"}",
                $"File: {e.Exception?.TargetSite?.ReflectedType?.Name ?? "[null]"}",
                $"Method: {e.Exception?.TargetSite?.Name ?? "[null]"}"
            };

            var trace = e.Exception?.StackTrace;
            if (!string.IsNullOrEmpty(trace))
            {
                logStr.Add("============================== Info =============================");
                var lines = trace.Split(new string[] { "\n" }, StringSplitOptions.None).ToList();
                foreach (var str in lines)
                    logStr.Add($" =>{str}");
            }

            Log.SaveLog(logStr);            
        }
    }
}
