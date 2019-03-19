using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Api.ViewModels.Validations
{
    public class StudentViewModelValidator : AbstractValidator<StudentViewModel>
    {
        public StudentViewModelValidator()
        {
            RuleFor(vm => vm.FirstName)
                .NotEmpty()
                .WithMessage("First name cannot be null or empty");
            RuleFor(vm => vm.LastName)
                .NotEmpty()
                .WithMessage("Last name cannot be null or empty");
            RuleFor(vm => vm.DOB)
                .NotNull()
                .NotEmpty()
                .WithMessage("DOB cannot be null or empty")
                .Must(BeValidDate)
                .WithMessage("DOB should be in proper date format.");
            //RuleFor(vm => vm.DOB).Must(BeValidDate).WithMessage("DOB cannot be null or empty");
            RuleFor(vm => vm.GPA)
                .NotNull()
                .NotEmpty()
                .WithMessage("GPA name cannot be null or empty")
                .InclusiveBetween(1,4)
                .WithMessage("GPA should be between 1 and 4")
                .ScalePrecision(2,3)
                .WithMessage("GPA should have maximum of 2 decimal precision");
        }

        private bool BeValidDate(string strDate)
        {
            DateTime date;
            var isDate = DateTime.TryParse(strDate,out date);
            if(date != null)
                return !date.Equals(default(DateTime));
            return false;
        }
    }
}
