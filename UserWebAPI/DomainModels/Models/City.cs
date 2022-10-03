using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainModels.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required]
        public string CityName { get; set; }

        [Display(Name = "State")]
        public virtual int StateId { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }
}
