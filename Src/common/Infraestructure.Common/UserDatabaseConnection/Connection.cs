namespace Infraestructure.Common.UserDatabaseConnection
{
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    [DataContract(Name = HeaderConstants.Name, Namespace = HeaderConstants.NamespaceURI)]
    public class Connection
    {
        [DataMember]
        public string User { get; set; }

        [DataMember]
        public string Password { get; set; }

        public static Connection GetHeaderFromMessage()
        {
            
            MessageHeaders headers = OperationContext.Current.IncomingMessageHeaders;
            foreach (var header in headers.UnderstoodHeaders)
            {
                if (header.Name == HeaderConstants.Name && header.Namespace == HeaderConstants.NamespaceURI)
                {
                    return headers.GetHeader<Connection>(header.Name, header.Namespace);
                }
            }

            return null;
        }
    }
}
