﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34209
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceAgents.Siport.SiportReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SiportReference.IBackendService")]
    public interface IBackendService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackendService/ExecuteCommand", ReplyAction="http://tempuri.org/IBackendService/ExecuteCommandResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(CommandContracts.Common.CommandFault), Action="http://tempuri.org/IBackendService/ExecuteCommandCommandFaultFault", Name="CommandFault", Namespace="http://schemas.datacontract.org/2004/07/CommandContracts.Common")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CommandContracts.Siport.HojaRuta.InsertarPlanificacionCommand))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CommandContracts.Siport.OrdenServicio.DetalleInsertarEditarOrdSrvCommand))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CommandContracts.Siport.OrdenServicio.Outputs.DetalleInsertarEditarOrdSrvOutput))]
        CommandContracts.Common.CommandResult ExecuteCommand(CommandContracts.Common.Command command);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackendService/ExecuteQuery", ReplyAction="http://tempuri.org/IBackendService/ExecuteQueryResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.Cliente.Parameter.ObtenerClienteParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.Seguridad.Parameters.ListarMenuParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.Seguridad.Parameters.ObtenerDatosUsuarioParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.Seguridad.Parameters.ValidarCredencialesUsuarioParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.HojaRuta.Parameter.ListarOrdenServicioDisponibleParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.HojaRuta.Parameter.ListarVehiculosDisponiblesParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.Ubigeo.Parameter.ListarUbigeoParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.OrderServicio.Parameter.ListarHorarioEntregaParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.OrderServicio.Parameter.ListarOrdenServicioParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.OrderServicio.Parameter.ObtenerOrdenServicioDestinoParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.OrderServicio.Parameter.ObtenerOrdenServicioParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.ObtenerDatosConexionParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.Seguridad.Results.ListarMenuResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.Seguridad.Results.ObtenerDatosUsuarioResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.Seguridad.Results.ValidarCredencialesUsuarioResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.Ubigeo.Result.ListarUbigeoResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.HojaRuta.Result.ListarOrdenServicioDisponibleResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.HojaRuta.Result.ListarVehiculosDisponiblesResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.Cliente.Result.ObtenerClienteResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.OrderServicio.Result.ListarHorarioEntregaResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.OrderServicio.Result.ListarOrdenServicioResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.OrderServicio.Result.ObtenerOrdenServicioDestinoResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Siport.OrderServicio.Result.ObtenerOrdenServicioResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.ObtenerDatosConexionResult))]
        QueryContracts.Common.QueryResult ExecuteQuery(QueryContracts.Common.QueryParameter parameter);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBackendServiceChannel : ServiceAgents.Siport.SiportReference.IBackendService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BackendServiceClient : System.ServiceModel.ClientBase<ServiceAgents.Siport.SiportReference.IBackendService>, ServiceAgents.Siport.SiportReference.IBackendService {
        
        public BackendServiceClient() {
        }
        
        public BackendServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BackendServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BackendServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BackendServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public CommandContracts.Common.CommandResult ExecuteCommand(CommandContracts.Common.Command command) {
            return base.Channel.ExecuteCommand(command);
        }
        
        public QueryContracts.Common.QueryResult ExecuteQuery(QueryContracts.Common.QueryParameter parameter) {
            return base.Channel.ExecuteQuery(parameter);
        }
    }
}
