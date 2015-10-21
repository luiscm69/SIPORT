using Domain.Siport.HojaRuta;
using System.Data.Entity.ModelConfiguration;
namespace Data.Siport.Mappings
{
    public class PlanhojaderutadetConfigurations : EntityTypeConfiguration<PlanHojadeRutaDet>
    {
        public PlanhojaderutadetConfigurations()
            : base()
        {
            //HasKey(p => p.ID);
            //Property(p => p.ID)
            //    .HasColumnName("IDPLANHOJADERUTADET")
            //    .IsRequired();

            //ToTable("OPERACIONES.PLANHOJADERUTADET");
            

        }
    }
}
