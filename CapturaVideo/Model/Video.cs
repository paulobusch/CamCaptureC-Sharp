using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using Accord.Video.FFMPEG;

namespace CapturaVideo.Model
{
    public class Video
    {
        public bool recording;

        private Size _resolution;
        private int _id_name;
        private VideoFileWriter _writer;
        private DateTime _start_record;

        private static Thread _compress;

        #region Video
        public Video(int id_name, Size resolution)
        {
            this._resolution = resolution;
            this._id_name = id_name;
            recording = false;
        }
        public void StartRecording()
        {
            _writer = new VideoFileWriter();
            _writer.Open($"{DeviceController.Configuration.PathSaveVideo}{DateTime.Now.ToString("yyyy-MM-dd HHmm")}-{_id_name}_.avi",
                _resolution.Width, _resolution.Height, DeviceController.Configuration.FrameRate, VideoCodec.MPEG4, DeviceController.Configuration.BitRate);
            _start_record = DateTime.Now;
            recording = true;
        }
        public void WriteFrame(Bitmap img)
        {
            lock (_writer) {
                _writer.WriteVideoFrame(img, TimeSpan.FromMilliseconds(
                     DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds - 
                    _start_record.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds));
            }
        }
        public void StopRecording()
        {
            recording = false;
            lock (_writer)
            {
                _writer.Close();
                _writer = null;
            }
        }
        #endregion

        #region Compress
        public static void CompressAllVideoThread()
        {
            if (!DeviceController.Configuration.EnableCompressVideo)
                return;
            if (_compress == null || (_compress.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Unstarted)) != 0)
            {
                _compress = new Thread(CompressAllVideo);
                _compress.IsBackground = true;
                _compress.Start();
            }
        }
        private static void CompressAllVideo()
        {
            //execute external bat process
            var process = new Process {
                StartInfo =
                {
                    FileName = $@"{Consts.CURRENT_PATH}\{Consts.FFMPEG_PATH}\compressFiles.bat",
                    Arguments = $"\"{DeviceController.Configuration.PathSaveVideo}*_.avi\" \"{Consts.CURRENT_PATH}\"",//filter media - source
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
