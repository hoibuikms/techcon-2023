using System.ComponentModel.DataAnnotations;

namespace Common.Entites
{
    public interface IBaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}