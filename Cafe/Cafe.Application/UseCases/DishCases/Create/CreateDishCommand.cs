using MediatR;
using Cafe.Domain.Entities;
using Cafe.Application.OperationResult;

namespace Cafe.Application.UseCases.DishCases.Create;

public class CreateDishCommand : IRequest<IOperationResult>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public List<string> PhotosUrls { get; set; }
    public List<Pair<Guid, double>> Ingridients { get; set; }
}
