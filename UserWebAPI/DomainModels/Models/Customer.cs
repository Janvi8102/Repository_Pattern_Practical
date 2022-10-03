using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainModels.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }


        [Display(Name = "City")]
        public virtual int? CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        [Display(Name = "State")]
        public virtual int? StateId { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        [Display(Name = "Country")]
        public virtual int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public string PinCode { get; set; }
    }
}
