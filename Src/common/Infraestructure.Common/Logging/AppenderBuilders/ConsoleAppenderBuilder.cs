namespace Infraestructure.Common.Logging.AppenderBuilders
{
    using Infraestructure.Common.Logging.Layout;

    using log4net.Appender;

    public class ConsoleAppenderBuilder : IAppenderBuilder
    {
        public IAppender Build()
        {
            var consoleAppender = new ConsoleAppender { Layout = LayoutFactory.Create() };
            consoleAppender.ActivateOptions();
            return consoleAppender;
        }
    }
}
