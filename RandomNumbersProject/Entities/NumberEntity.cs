using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class NumberEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }
    }
}