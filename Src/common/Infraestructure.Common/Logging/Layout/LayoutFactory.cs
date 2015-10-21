
namespace Infraestructure.Common.Logging.Layout
{
    using log4net.Layout;

    public class LayoutFactory
    {
        private const string DEFAULT_PATTERN = "%-5level - %date - %message%newline";
        private const string USER_PATTERN = "%-5level - %date - %property{user} - %message%newline";

        public static ILayout Create()
        {
            var layout = new PatternLayout { ConversionPattern = Pattern() };
            layout.ActivateOptions();
            return layout;
        }

        private static string Pattern()
        {
            var pattern = DEFAULT_PATTERN;
            if (log4net.GlobalContext.Properties["user"] != null)
            {
                pattern = USER_PATTERN;
            }
            return pattern;
        }

    }
}
