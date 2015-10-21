namespace CommandContracts.Common
{
    public class CommandError
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }

        public CommandError() { }

        public CommandError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }
}
