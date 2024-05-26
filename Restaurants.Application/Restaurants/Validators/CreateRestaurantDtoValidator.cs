using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    private readonly IRestaurantsRepository _restaurantsRepository;

    public CreateRestaurantDtoValidator(IRestaurantsRepository restaurantsRepository)
    {
        _restaurantsRepository = restaurantsRepository;

        RuleFor(p => p.Name)
            .Must(IsNameUnique).WithMessage(x => $"The name {x.Name} is already exists. Name should be unique.")
            .Length(3, 100);
       
        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Description is required");

        RuleFor(p => p.ContactEmail)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");

        RuleFor(p => p.Category)
            .NotEmpty()
            .WithMessage("Insert a valid category");

        RuleFor(p => p.ContactNumber)
            .MaximumLength(11)
            .Must(IsValidContact)
            .When(x => !string.IsNullOrEmpty(x.ContactNumber)).WithMessage("Should be 11 digits and contain only digits");

        RuleFor(p => p.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide a valid postal code (XX-XXX).");

    }

    private bool IsValidContact(string? value)
    {
        if (string.IsNullOrEmpty(value)) return true;

        foreach (char c in value)
        {
            if (c < '0' || c > '9')
                return false;
        }
        return true;
    }

    private bool IsNameUnique(string name)
    {
        var restaurants = _restaurantsRepository.GetQuery();
        var isExists = restaurants.Any(p => p.Name == name);
        return !isExists;
    }
}
