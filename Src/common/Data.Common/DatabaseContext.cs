namespace Data.Common
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Reflection;

    using Infraestructure.Common.Module;

    public class DatabaseContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Conventions.Remove<ColumnTypeCasingConvention>();
           
            RegisterEntitiesFromAssembly(modelBuilder, AppDomain.CurrentDomain.LoadFromModule("Data"));
            RegisterEntitiesFromAssembly(modelBuilder, AppDomain.CurrentDomain.Load("Data.Common"));
        }

        private static void RegisterEntitiesFromAssembly(DbModelBuilder modelBuilder, Assembly assembly)
        {
            var typesToRegister =
                assembly.GetTypes().Where(
                    type =>
                    type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }
    }
}
