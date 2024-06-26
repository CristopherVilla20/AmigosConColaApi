using AmigosConCola.Core.Extensions;
using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.Core.Validations;
using ErrorOr;

namespace AmigosConCola.Core.UseCases;

public sealed class CreateAnimalUseCase
{
    private readonly IAnimalRepository _animals;

    public CreateAnimalUseCase(
        IAnimalRepository animals)
    {
        _animals = animals;
    }

    public async Task<ErrorOr<Animal>> Invoke(CreateAnimalParams parameters)
    {
        var validator = new CreateAnimalParamsValidator();
        var validationResult = validator.Validate(parameters);

        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        return await _animals.Create(parameters);
    }
}