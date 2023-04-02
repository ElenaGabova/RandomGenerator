using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class NumberRepetitionEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int RepetitionAmount { get; set; }
    }
}