namespace Cafe.Domain.Entities;

public class Dish : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public List<string> PhotosUrls { get; set; }
    public List<Pair<Guid, double>> Ingridients { get; set; }
}
