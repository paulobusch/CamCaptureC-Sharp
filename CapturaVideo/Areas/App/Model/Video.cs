using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

using Accord.Video.FFMPEG;
using System.IO;
using System.Collections.Generic;
using MultiCam.Areas.App.Model.Dtos;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace MultiCam.Model
{
    public class Video
    {
        public bool recording;

        private Size _resolution;
        private string _codenome;
        private DateTime _start_record;
        private List<FrameDto> _frames = new List<FrameDto>();
        
        private static Queue<Thread> _write = new Queue<Thread>();
        private static Thread _compress;

        #region Video
        public Video(string codenome, Size resolution)
        {
            this._resolution = resolution;
            this._codenome = codenome;
            recording = false;
        }
        public void StartRecording()
        {
            recording = true;
            _start_record = DateTime.Now;
        }
        public void WriteFrame(Bitmap img)
        {
            _frames.Add(new FrameDto{ 
                bytes = Helpers.ToByteArray(img), 
                time = DateTime.Now - _start_record 
            });
        }
        public void StopRecording(string pathSaveVideo, int frameRate, int bitRate)
        {
            recording = false;            

            if (!Directory.Exists(pathSaveVideo))
                Directory.CreateDirectory(pathSaveVideo);

            var thread = new Thread(() =>
            {
                var writer = new VideoFileWriter();
                writer.Open($"{pathSaveVideo}{_codenome}_{DateTime.Now.ToString("yyyy-MM-dd HHmm")}_.avi",
                    _resolution.Width, _resolution.Height, frameRate, VideoCodec.MPEG4, bitRate);

                _frames.ForEach(frame =>
                {
                    try
                    {
                        using (var image = Image.FromStream(new MemoryStream(frame.bytes)))
                            writer.WriteVideoFrame((Bitmap)image, frame.time);
                    }
                    catch (Exception) { }
                });

                _frames.Clear();

                writer.Close();
                writer.Dispose();
                GC.Collect();
            });

            _write.Enqueue(thread);

            if (_write.Peek().ThreadState != System.Threading.ThreadState.Running)
            {
                Task.Run(() =>
                {
                    while (_write.Count > 0)
                        _write.Dequeue().Start();
                });
            }
        }
        #endregion

        #region Compress
        public static void CompressAllVideoThread(string pathSaveVideo)
        {
            if (_compress == null || (_compress.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Unstarted)) != 0)
            {
                _compress = new Thread(() => CompressAllVideo(pathSaveVideo));
                _compress.IsBackground = true;
                _compress.Start();
            }
        }
        private static void CompressAllVideo(string pathSaveVideo)
        {
            //execute external bat process
            var process = new Process {
                StartInfo =
                {
                    FileName = $@"{Consts.CURRENT_PATH}\{Consts.FFMPEG_PATH}\compressFiles.bat",
                    Arguments = $"\"{pathSaveVideo}*_.avi\" \"{Consts.CURRENT_PATH}\"",//filter media - source
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true
                }
            };

            process.Start();
            process.WaitForExit();
        }
        #endregion

    }
}
