namespace Infraestructure.Common.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    using Infraestructure.Common.Configuration;
    using Infraestructure.Common.Logging.AppenderBuilders;

    using log4net;
    using log4net.Appender;
    using log4net.Repository.Hierarchy;

    public class LoggingConfigurator
    {
        private static List<IAppender> appenders = new List<IAppender>();

        public static void AddAppenderBuilder<TAppenderBuilder>() where TAppenderBuilder : IAppenderBuilder
        {
            var appenderBuilder = Activator.CreateInstance<TAppenderBuilder>();
            appenders.Add(appenderBuilder.Build());
        }

        public static void AddAppenderBuilder(IAppenderBuilder appenderBuilder)
        {
            appenders.Add(appenderBuilder.Build());
        }

        public static void Configure()
        {
            log4net.Config.BasicConfigurator.Configure();
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            AddAppenders(hierarchy);
            SetLevel(hierarchy);
        }

        private static void SetLevel(Hierarchy hierarchy)
        {
            if (AppConfigurationSettings.IsProductionEnvironment)
            {
                Logger rootLogger = hierarchy.Root;
                rootLogger.Level = hierarchy.LevelMap["INFO"];
            }
        }

        private static void AddAppenders(Hierarchy hierarchy)
        {
            hierarchy.Root.RemoveAllAppenders();
            foreach (var appender in appenders)
            {
                hierarchy.Root.AddAppender(appender);
            }
        }
    }
}