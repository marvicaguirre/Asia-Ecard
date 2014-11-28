using System;
using System.IO;
using System.Text;

namespace Wizardsgroup.Utilities.Helpers
{
    public static class Logger
    {
        public static void Log(string message)
        {
            var finalMessage = string.Format("{0} : {1}{2}", DateTime.Now.ToLongTimeString(), message, Environment.NewLine);

            var baseDir = AppDomain.CurrentDomain.BaseDirectory.Trim();
            baseDir = baseDir.LastIndexOf(@"\", StringComparison.Ordinal) == baseDir.Length - 1
                          ? baseDir.Remove(baseDir.Length - 1)
                          : baseDir;
            var baseLogPath = baseDir + @"\Logs";
            var logFileFullPath = string.Format(@"{0}\{1}", baseLogPath, "Log-Current.txt");

            _BackupFile(logFileFullPath, baseLogPath);

            try
            {
                File.AppendAllText(logFileFullPath, finalMessage, Encoding.ASCII);
            }
            catch
            {
            }
        }

        private static void _BackupFile(string logFileFullPath, string baseLogPath)
        {
            const int MAX_FILE_SIZE = 512000;
            var backupFileFullPath = string.Format(@"{0}\Log-{1}-{2}.txt", baseLogPath
                                                   , DateTime.Now.ToShortDateString().Replace("/", "")
                                                   , DateTime.Now.ToLongTimeString().Replace(":", ""));
            var fi = new FileInfo(logFileFullPath);
            var di = new DirectoryInfo(baseLogPath);

            if (_Guard(fi, di, MAX_FILE_SIZE, baseLogPath, logFileFullPath, backupFileFullPath))
            {
                File.Copy(logFileFullPath, backupFileFullPath);
                File.WriteAllText(logFileFullPath, "==reset==" + DateTime.Now.ToLongTimeString().Replace(":", ""));
            }
        }


        private static bool _Guard(FileInfo fi, DirectoryInfo di, int MAX_FILE_SIZE, string baseLogPath, string logFileFullPath, string backupFileFullPath)
        {
            bool result = false;
            if (!di.Exists) Directory.CreateDirectory(baseLogPath);
            if (!fi.Exists) { var myFile = File.Create(logFileFullPath); myFile.Close(); }
            if (fi.Exists && fi.Length > MAX_FILE_SIZE) result = true;
            return result;
        }
    }
}