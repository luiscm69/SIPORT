namespace CommandContracts.Common
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(Name = "CommandFault", Namespace = "http://schemas.datacontract.org/2004/07/CommandContracts.Common")]
    public class CommandFault
    {
        [DataMember]
        public List<CommandError> CommandErrors { get; set; }

        public CommandFault() { }

        public CommandFault(List<CommandError> commandErrors)
        {
            this.CommandErrors = commandErrors;
        }

        public CommandFault(string errorMessage)
        {
            this.CommandErrors = new List<CommandError> { new CommandError("", errorMessage) };
        }
    }
}
