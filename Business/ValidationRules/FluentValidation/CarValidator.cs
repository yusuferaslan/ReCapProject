using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.BrandId).NotEmpty();
            RuleFor(c => c.Description).MinimumLength(2);
            RuleFor(c => c.DailyPrice).NotEmpty();
            //Color 1 kategorisinde ise (1  nolu kategorideyse) gunluk kira minimum 10 olmalidir.
            RuleFor(c => c.DailyPrice).GreaterThanOrEqualTo(10).When(c => c.ColorId == 1);
            
        }
       
    }
}
