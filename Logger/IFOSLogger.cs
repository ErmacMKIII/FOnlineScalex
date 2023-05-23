/* Copyright (C) 2023 Aleksandar Stojanovic <coas91@rocketmail.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/> */
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
