using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FOnlineScalex.Logger
{
    public class FOSLogger : IFOSLogger
    {
        private readonly Stream logger;

        private StreamWriter writer;

        private static readonly string Format = "MM/dd/yyyy_HH:mm:ss.ffff";

        //private static string HomePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
        //           Environment.OSVersion.Platform == PlatformID.MacOSX)
        //            ? Environment.GetEnvironmentVariable("$HOME")
        //            : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

        public string DirPath { get; protected set; }

        public string FileName { get; protected set; }

        public string FullPath { get => GetFullPath(); }

        public FOSLogger(string filename, string logDirPath)
        {            ;            
            FileName = filename;
            DirPath = logDirPath;
            if (!Directory.Exists(DirPath))
            {
                Directory.CreateDirectory(DirPath);
            }
            this.logger = File.Create(FullPath);            
            Init();
        }

        /// <summary>
        /// Initializes this logger. 
        /// Capable of writting to the output file & console in the same time using Trace.
        /// </summary>
        private void Init()
        {
            // create file writer
            writer = new StreamWriter(logger, Encoding.UTF8);
            // add listner to the output file writer
            Trace.Listeners.Add(new TextWriterTraceListener(writer));
            // add listner to to the console output
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            // Set Auto Flush (so content is immediately available after each write)
            Trace.AutoFlush = true;
        }

        public Stream GetLogger()
        {
            return logger;
        }

        public void Log(string message, Exception ex, IFOSLogger.LogLevel severity)
        {
            Trace.WriteLine(string.Format("{0}:{1}>{2} {3}", DateTime.UtcNow.ToString(FOSLogger.Format), severity, message, ex.Message));
        }

        public void Log(string message, Exception ex)
        {
            Trace.WriteLine(string.Format("{0}:{1}>{2} {3}", DateTime.UtcNow.ToString(FOSLogger.Format), IFOSLogger.LogLevel.ERR, message, ex.Message));
        }

        public void Log(string message, IFOSLogger.LogLevel severity)
        {
            Trace.WriteLine(string.Format("{0}:{1}>{2} {3}", DateTime.UtcNow.ToString(FOSLogger.Format), severity, message, null));
        }

        public void Log(string message)
        {
            Trace.WriteLine(string.Format("{0}:{1}>{2} {3}", DateTime.UtcNow.ToString(FOSLogger.Format), IFOSLogger.LogLevel.INFO, message, null));
        }

        /// <summary>
        /// Get Log Path (without filename). It uses F: Drive.
        /// </summary>
        /// <returns>Log Path</returns>
        public string GetDirPath()
        {
            return "F:\\" + "RTDD" + Path.DirectorySeparatorChar + "ADF" + Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// Gets log filename.
        /// </summary>
        /// <returns>log filename</returns>
        public string GetFileName()
        {
            return FileName;
        }

        /// <summary>
        /// Get Full Path (with filename). It uses F: Drive.
        /// </summary>
        /// <returns>Log Path</returns>
        public string GetFullPath()
        {
            return DirPath + Path.DirectorySeparatorChar + FileName;
        }

        /// <summary>
        /// Flushes the Trace
        /// </summary>
        public static void Flush()
        {
            Trace.Flush();
        }

        /// <summary>
        /// Flushes & closes all the listeners
        /// </summary>
        public static void Close()
        {
            Trace.Close();
        }

    }
}
