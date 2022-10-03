using DomainModels.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModels.ValidateEntity
{
   public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator(IEnumerable<Customer> customers)
        {
            RuleFor(x => x.PinCode).Must(x => x.Length == 6).WithMessage("Invalid Pincode");
            RuleFor(x => x.Mobile).Must(x => x.ToString().Length == 10).WithMessage("Invalid Mobile Number");
            RuleFor(x => x.CustomerName).NotEmpty().NotNull().WithMessage("The Customer Name cannot be blank.");
            RuleFor(customer => customer.Email).EmailAddress();
        }
    }
}
