﻿using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using CapturaVideo.Model;
using AForge.Video.FFMPEG;

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
            _writer.Open($"{Configuration.path_save_video}{_id_name}_{DateTime.Now.ToString("hhmmss")}_.avi",
                _resolution.Width, _resolution.Height, Configuration.frame_rate, VideoCodec.MPEG4, Configuration.bit_rate);
            _start_record = DateTime.Now;
            recording = true;
        }
        public void WriteFrame(Bitmap img)
        {
            lock (_writer)
                _writer.WriteVideoFrame(img, DateTime.Now - _start_record);
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
            if (!Configuration.compress_video)
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
                    Arguments = $"\"{Configuration.path_save_video}*_.avi\" \"{Consts.CURRENT_PATH}\"",//filter media - source
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
