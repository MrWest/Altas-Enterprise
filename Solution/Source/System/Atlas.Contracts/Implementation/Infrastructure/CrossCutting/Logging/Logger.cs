using System;
using System.IO;
using Microsoft.Practices.Prism.Logging;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Logging
{
    /// <summary>
    /// This is the custom logger of the system.
    /// </summary>
    // TODO: Test
    public class Logger : ILoggerFacade, IDisposable
    {
        //changed to a reasonable directory
        private readonly string LogsPath = Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WildWest Company"), "Atlas Enterprise"), "Logs");
        private readonly TraceLogger _trace = new TraceLogger();
        private readonly TextLogger _text;
        private readonly FileStream _file;
        private static Logger _instance = new Logger();
        private readonly object Lock = new object();


        /// <summary>
        /// Initializes a new instance of the suite's custom logger.
        /// </summary>
        private Logger()
        {
           
            if (!Directory.Exists(LogsPath))
                Directory.CreateDirectory(LogsPath);

            var logsFile = Path.Combine(LogsPath, "logs.log");
            _file = new FileStream(logsFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            var streamWriter = new StreamWriter(_file);
            _text = new TextLogger(streamWriter);
        }

        /// <summary>
        /// Finalizes the logger.
        /// </summary>
        ~Logger()
        {
            Dispose();
        }


        /// <summary>
        /// Gets the singleton instance of the <see cref="Logger"/> class.
        /// </summary>
        public static Logger Instance
        {
            get { return _instance; }
        }


        /// <summary>
        /// Write a new log entry with the specified category and priority.
        /// </summary>
        /// <param name="message">Message body to log.</param>
        /// <param name="category">Category of the entry.</param>
        /// <param name="priority">The priority of the entry.</param>
        public void Log(string message, Category category, Priority priority)
        {
            lock (Lock)
            {
                _trace.Log(message, category, priority);
                _text.Log(message, category, priority);
                Flush();
            }
        }

        /// <summary>
        /// Performs specific operations to clean the resources used by the current logger.
        /// </summary>
        public void Dispose()
        {
            Flush();
            _text.Dispose();

            GC.SuppressFinalize(this);
        }

        private void Flush()
        {
            lock (Lock)
                _file.Flush();
        }
    }
}
