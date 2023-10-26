namespace Yadam.Models;

public class AlbumItemModel
{
    public int Id { get; set; }
    public Guid AlbumId { get; set; }
    public int? OrderId { get; set; }
    public string? Filename { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
}