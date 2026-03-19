using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StarTrekDatabase.Entities
{
    [Table("Ship")]

    public class ShipEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Class { get; set; }

        [Required]
        public string RaceFaction { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public int Crew { get; set; }

        [Required]
        public double MaxWarp { get; set; }

        [Required]
        public string Armament { get; set; }

        [Required]
        public string ShieldType { get; set; }

        [Required]
        public string HullMaterial {get; set;  }

        [Required]
        public string Role { get; set; }
    }
}
