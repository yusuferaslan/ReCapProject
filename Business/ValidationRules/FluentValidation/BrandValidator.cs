using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.Name).NotEmpty();
            RuleFor(b => b.Name).MinimumLength(3);
            //Must uymali demek. Olmayan bi kurali kendimiz yazabilmek icin must asagidaki gibi kullanilir.
            RuleFor(b => b.Name).Must(StartWhithA).WithMessage(" A harfi ile başlamalı");
        }
        private bool StartWhithA(string arg)
        {
            return arg.StartsWith("A");
        }
    
    }
}
