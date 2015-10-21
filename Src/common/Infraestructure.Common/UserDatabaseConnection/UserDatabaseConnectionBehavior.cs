using System;

namespace Infraestructure.Common.UserDatabaseConnection
{
    using System.ServiceModel.Channels;
    using System.ServiceModel.Configuration;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    public class UserDatabaseConnectionBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new UserDatabaseConnectionMessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new UserDatabaseConnectionMessageInspector());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }


        protected override object CreateBehavior()
        {
            return this;
        }

        public override Type BehaviorType
        {
            get
            {
                return this.GetType();
            }
        }

        


        

        
    }
}
