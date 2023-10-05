using Cafe.Domain.Entities;

namespace Cafe.Application.DTO.DishTO;

public class DishCM
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public List<string> PhotosUrls { get; set; }
    public Pair<Guid, double> Ingridients { get; set; }
}
