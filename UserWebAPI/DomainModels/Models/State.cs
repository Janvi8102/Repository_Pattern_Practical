using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainModels.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required]
        public string StateName { get; set; }

        [Display(Name = "Country")]
        public virtual int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
       
    }
}
