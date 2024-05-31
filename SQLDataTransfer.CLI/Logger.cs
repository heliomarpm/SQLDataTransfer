using System;
using System.IO;

namespace SQLDataTransfer.CLI
{
    internal class Logger
    {
        private readonly string _path;
        private string _fileName;

        public Logger()
        {
            this._path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log");

            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            this.FileName = $"Log-{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = Path.Combine(_path, value);
            }
        }

        public void WriteLog(string message, ConsoleColor fColor = ConsoleColor.White, bool includeTime = true)
        {
            Console.ForegroundColor = fColor;
            if (includeTime)
                message = string.Format("{0}: {1}", DateTime.Now.ToString("HH:mm:ss"), message);

            Console.WriteLine(message);
            WriteFileLog(message + Environment.NewLine);
        }

        public void WriteError(string message)
        {
            this.WriteLog("ERRO! " + message, ConsoleColor.DarkRed);
        }

        public void WriteLine(char divisor = '-')
        {
            string text = new String(divisor, 100) + Environment.NewLine;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);

            WriteFileLog(text);
        }

        private void WriteFileLog(string text)
        {
            lock (this)
            {
                File.AppendAllText(_fileName, text);
            }
        }
    }
}
