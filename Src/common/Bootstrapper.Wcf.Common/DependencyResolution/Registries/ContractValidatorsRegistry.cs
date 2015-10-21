namespace Bootstrapper.Wcf.Common.DependencyResolution.Registries
{
    using System;
    using System.Reflection;

    using FluentValidation;

    using Infraestructure.Common.Module;

    using StructureMap.Configuration.DSL;

    public class ContractValidatorsRegistry : Registry
    {
        public ContractValidatorsRegistry()
        {
            Assembly assembly = AppDomain.CurrentDomain.LoadFromModule("CommandContracts");
            AssemblyScanner.FindValidatorsInAssembly(assembly)
                           .ForEach(result => this.For(result.InterfaceType)
                                                  .Singleton()
                                                  .Use(result.ValidatorType));

            assembly = AppDomain.CurrentDomain.LoadFromModule("QueryContracts");
            AssemblyScanner.FindValidatorsInAssembly(assembly)
                           .ForEach(result => this.For(result.InterfaceType)
                                                  .Singleton()
                                                  .Use(result.ValidatorType));
        }
    }
}