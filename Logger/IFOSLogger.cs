using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FOnlineScalex.Logger
{
    /// <summary>
    /// Represents simple logger used by ADF Packages.
    /// </summary>
    public interface IFOSLogger
    {
        /// <summary>
        /// Log Level of the logging message
        /// </summary>
        public enum LogLevel { INFO, ERR, DEBUG }

        /// <summary>
        /// Returns underlaying log stream
        /// </summary>
        /// <returns>underlaying log stream </returns>
        public Stream GetLogger();

        /// <summary>
        /// Writes a log message (to logging stream)
        /// </summary>
        /// <param name="message">user message</param>
        /// <param name="ex">exception</param>
        /// <param name="severity">log level {INFORMATION, ERROR, DEBUG}</param>
        public void Log(string message, Exception ex, LogLevel severity);

        /// <summary>
        /// Writes a log message (to logging stream). Severity is set to ERROR.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Log(string message, Exception ex);

        /// <summary>
        /// Writes a log message (to logging stream)
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message);        

    }
}
