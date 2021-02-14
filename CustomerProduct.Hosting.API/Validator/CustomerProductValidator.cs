using CustomerProduct.Hosting.API.Models.RequestModels;
using FluentValidation;

namespace CustomerProduct.Hosting.API.Validator
{
    public class CustomerProductValidator : AbstractValidator<CustomerCreateOrDeleteRequest>
    {
        public CustomerProductValidator()
        {
            RuleFor(c => c.IdentificationNumber)
             .NotEmpty()
             .WithMessage("IdentificationNumber Fields Is Required");


            RuleFor(c => c.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId Field Is Required");


        }
    }
}
