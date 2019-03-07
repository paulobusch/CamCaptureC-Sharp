using CapturaVideo.Model;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CapturaVideo.Model
{
    public static class Log
    {
        private static string path_name_log = $@"{Consts.CURRENT_PATH}\{Consts.NAME_FILE_LOG}";

        public static void SaveLog(List<string> data_line)
        {
            using (StreamWriter file_log = new StreamWriter(path_name_log, true, Encoding.Unicode)){
                foreach (var item in data_line)
                    file_log.WriteLine(item);
                file_log.Close();
            }
        }
    }
}
