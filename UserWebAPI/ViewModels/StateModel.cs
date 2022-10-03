using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class StateModel
    {
        public int StateId { get; set; }     
        public string StateName { get; set; }       
        public CountryModel Country { get; set; }
    }
}
