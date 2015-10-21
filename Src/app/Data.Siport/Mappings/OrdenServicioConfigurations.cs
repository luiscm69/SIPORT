

using System.Data.Entity.ModelConfiguration;
namespace Data.Siport.Mappings
{
    public class OrdenServicioConfigurations :  EntityTypeConfiguration<Domain.Siport.OrdenServicio.OrdenServicio>
    {
        public OrdenServicioConfigurations()
            : base()
        {
            HasKey(p => p.ID);
            Property(p => p.ID)
                .HasColumnName("IDORDENSERVICIO")
                .IsRequired();

            HasMany(x => x.ListaDestinos)
                .WithRequired(a => a.EntidadOrdenServicio)
                .Map(b => b.MapKey("IDORDENSERVICIO"));

            ToTable("OPERACIONES.ORDENSERVICIO");
            

        }
    }
}
