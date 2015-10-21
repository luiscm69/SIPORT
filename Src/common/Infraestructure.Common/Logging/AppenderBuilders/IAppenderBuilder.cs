namespace Infraestructure.Common.Logging.AppenderBuilders
{
    using log4net.Appender;

    public interface IAppenderBuilder
    {
        IAppender Build();
    }
}
