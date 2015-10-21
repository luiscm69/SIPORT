namespace Infraestructure.Common.Logging.AppenderBuilders
{
    using System.Configuration;

    using Infraestructure.Common.Logging.Layout;

    using log4net.Appender;

    public class FileAppenderBuilder : IAppenderBuilder
    {
        private const string DefaultFileName = "log";
        public IAppender Build()
        {
            var fileAppender = new RollingFileAppender();
            fileAppender.RollingStyle = RollingFileAppender.RollingMode.Date;
            fileAppender.DatePattern = "'.'yyyyMMdd'.txt'";
            fileAppender.File = File();
            fileAppender.StaticLogFileName = false;
            fileAppender.Layout = LayoutFactory.Create();
            fileAppender.ActivateOptions();

            return fileAppender;
        }

        private static string File()
        {
            var file = DefaultFileName;
            var fileInConfiguration = ConfigurationManager.AppSettings["LogFile"];
            if (!string.IsNullOrEmpty(fileInConfiguration))
                file = fileInConfiguration;
            return file;
        }
    }
}
