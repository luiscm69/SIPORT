using Domain.Siport.OrdenServicio;
using System.Data.Entity.ModelConfiguration;
namespace Data.Siport.Mappings
{
    public class OrdenServicioDestinoConfigurations : EntityTypeConfiguration<OrdenServicioDestino>
    {
        public OrdenServicioDestinoConfigurations()
            : base()
        {
            HasKey(p => p.ID);
            Property(p => p.ID)
                .HasColumnName("IDORDENSERVICIODESTINO")
                .IsRequired();

            ToTable("OPERACIONES.ORDENSERVICIODESTINO");
            

        }
    }
}
