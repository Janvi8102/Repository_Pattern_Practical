using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainModels.InPutModel
{
    public class StateInput
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }

       

    }
}
