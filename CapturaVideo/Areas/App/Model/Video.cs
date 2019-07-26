using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

using Accord.Video.FFMPEG;
using System.IO;

namespace MultiCam.Model
{
    public class Video
    {
        public bool recording;

        private Size _resolution;
        private string _codenome;
        private DateTime _start_record;
        private VideoFileWriter _writer;
        
        private static Thread _compress;

        #region Video
        public Video(string codenome, Size resolution)
        {
            this._resolution = resolution;
            this._codenome = codenome;
            recording = false;
        }
        public void StartRecording(string pathSaveVideo, int frameRate, int bitRate)
        {
            if (!Directory.Exists(pathSaveVideo))
                Directory.CreateDirectory(pathSaveVideo);

            _writer = new VideoFileWriter();
            _writer.Open($"{pathSaveVideo}{_codenome}_{DateTime.Now.ToString("yyyy-MM-dd HHmm")}_.avi",
                _resolution.Width, _resolution.Height, frameRate, VideoCodec.MPEG4, bitRate);

            _start_record = DateTime.Now;
            recording = true;
        }
        public void WriteFrame(Bitmap img)
        {
            // TODO: Resolve bug library
            try { 
                lock(_writer)
                    _writer.WriteVideoFrame(img, DateTime.Now - _start_record);
            }catch (Exception) { }
        }
        public void StopRecording()
        {
            recording = false;
            lock (_writer)
            {
                _writer.Close();
                _writer = null;
            }

            GC.Collect();
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
