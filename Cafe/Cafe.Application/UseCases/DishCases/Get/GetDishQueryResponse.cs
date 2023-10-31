using Cafe.Domain.Entities;
using Newtonsoft.Json;

namespace Cafe.Application.UseCases.DishCases.Get;

public class GetDishQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public List<string> PhotosUrls { get; set; }
    public List<Pair<Guid, double>> Ingridients { get; set; }
}
