using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

using MultiCam.Logger.Controller;
using MultiCam.Model;
using System.Drawing;
using System.Reflection;

namespace MultiCam.Controller
{
    /// <summary>
    /// Main application program
    /// </summary>
    public static class MultiCam
    {
        /// <summary>
        /// Start point application
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

            ServiceLocator.RegisterServices();

            Application.Run(ServiceLocator.Instance.Get<IAppController>().Run());
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
