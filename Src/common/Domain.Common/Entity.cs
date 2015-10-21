using System.ComponentModel.DataAnnotations;
namespace Domain.Common
{
    public abstract class Entity
    {
        [Key]
        public long ID { get; set; }
    }
}
