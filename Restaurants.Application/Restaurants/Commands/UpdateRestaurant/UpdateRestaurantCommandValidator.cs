using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    private readonly IRestaurantsRepository _restaurantsRepository;

    public UpdateRestaurantCommandValidator(IRestaurantsRepository restaurantsRepository)
    {
        _restaurantsRepository = restaurantsRepository;

        RuleFor(p => p.Name)
            .Length(3, 100).WithMessage("Name should be atleast 3 characters and with maximu    m characters of 100")
            .Must((dto, b) => { return IsNameUnique(dto.Name, dto.Id); })
            .When(p => p.Id > 0 && p.Name is not null)
            .WithMessage(x => $"The name {x.Name} is already exists. Name should be unique.");

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

    private bool IsNameUnique(string name, int id)
    {
        var restaurants = _restaurantsRepository.GetQuery();
        var isExists = restaurants.Any(p => p.Name == name && p.Id != id);
        return !isExists;
    }
}
