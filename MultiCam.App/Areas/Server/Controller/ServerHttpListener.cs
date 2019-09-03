using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Net.Sockets;
using MultiCam.Model;
using MultiCam.Controller;

namespace MultiCam.Server.Controller
{
    public class ServerHttpListener
    {
        private static IAppController _controller;
        
        private static ServerHttpListener _server_web;

        private static HttpListener _listener = new HttpListener();
        private static Func<HttpListenerRequest, byte[]> _responderMethod;
        private static Thread _socket;

        public static void StratThread()
        {
            if (_socket == null)
            {
                _socket = new Thread(Strat);
                _socket.IsBackground = true;
                _socket.Start();
            }
        }
        private static void Strat()
        {
            if (_server_web != null)
                return;

            _server_web = new ServerHttpListener(SendResponse, $"http://{LocalIPAddress()}:80/");
            _server_web.Run();
        }

        public static byte[] SendResponse(HttpListenerRequest request)
        {
            string json;
            string baseUrl = request.RawUrl.Substring(1);
            switch (baseUrl)
            {
                //GET images
                case "GetImages":
                    json = JsonConvert.SerializeObject(_controller.GetImages());
                    return Encoding.ASCII.GetBytes(json);
                case "GetNames":
                    json = JsonConvert.SerializeObject(_controller.GetNames());
                    return Encoding.ASCII.GetBytes(json);
                //GET files
                default:
                    var path = !string.IsNullOrEmpty(baseUrl) ? baseUrl : "index.html";
                    return File.ReadAllBytes($@"{Consts.CURRENT_PATH}\{Consts.WEB_PATH}\{path}");
            }
        }
        public ServerHttpListener(string[] prefixes, Func<HttpListenerRequest, byte[]> method)
        {
            foreach (string s in prefixes)
                _listener.Prefixes.Add(s);

            _controller = ServiceLocator.Instance.Get<IAppController>();
            _responderMethod = method;
            _listener.Start();
        }

        public ServerHttpListener(Func<HttpListenerRequest, byte[]> method, params string[] prefixes) : this(prefixes, method) { }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            var ctx = c as HttpListenerContext;
                            try
                            {
                                byte[] buf = _responderMethod(ctx.Request);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    ctx.Response.OutputStream.Close();
                                }
                                catch { }
                            }
                        }, _listener.GetContext());
                    }
                }catch { }
            });
        }
        public static void StopThread()
        {
            if (_socket != null)
            {
                Stop();
                _socket.Abort();
            }
        }
        private static void Stop()
        {
            if (_listener.IsListening){
                _listener.Stop();
                _listener.Close();
                _server_web = null;
            }
        }
        public static string LocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
        }
    }
}
