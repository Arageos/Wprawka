using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlatform.Models
{
    public class Achievement
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(256)]
        public string? Description { get; set; }

        public int GameId { get; set; }

        public virtual Game? Game { get; set; }
    }
}