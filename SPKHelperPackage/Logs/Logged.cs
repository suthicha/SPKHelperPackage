using System;
using System.Globalization;
using System.IO;

namespace SPKHelperPackage.Logs
{
    public interface ILogged
    {
        void Event(params string[] message);

        void Error(params string[] message);
    }

    internal enum EnumLogged
    {
        ERROR,
        EVENT
    }

    public class Logged : ILogged
    {
        private readonly CultureInfo _cultureInfo;
        private string _locationPath = string.Empty;

        public Logged(string locationPath = "")
        {
            _cultureInfo = new CultureInfo("en-US");

            if (locationPath == "")
            {
                locationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        public void Error(params string[] message)
        {
            write(string.Join(" | ", message), EnumLogged.ERROR);
        }

        public void Event(params string[] message)
        {
            write(string.Join(" | ", message), EnumLogged.EVENT);
        }

        private void write(string content, EnumLogged logType)
        {
            string fileName = logType == EnumLogged.ERROR ? "error.log" : "event.log";
            string destFolder = Path.Combine(_locationPath, "Logged");
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            using (StreamWriter sw = new StreamWriter(
                new FileStream(Path.Combine(destFolder, fileName), FileMode.Append)))
            {
                try
                {
                    sw.WriteLine("{0} {1}",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", _cultureInfo),
                        content);
                }
                catch { }
            }
        }
    }
}