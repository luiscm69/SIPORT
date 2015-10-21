
using Domain.Siport.HojaRuta;
using System.Data.Entity.ModelConfiguration;
namespace Data.Siport.Mappings
{
    public class PlanhojaderutacabConfigurations : EntityTypeConfiguration<PlanHojadeRutaCab>
    {
        public PlanhojaderutacabConfigurations()
            : base()
        {
            //HasKey(p => p.ID);
            //Property(p => p.ID)
            //    .HasColumnName("IDPLANHOJADERUTACAB")
            //    .IsRequired();

            //HasMany(x => x.ListaPlanHojaRuta)
            //    .WithRequired(a => a.EntidadHojaRuta)
            //    .Map(b => b.MapKey("IDPLANHOJADERUTACAB"));

            //ToTable("OPERACIONES.PLANHOJADERUTADET");
            

        }
    }
}
