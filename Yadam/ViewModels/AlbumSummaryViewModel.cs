namespace Yadam.ViewModels;

public class AlbumSummaryViewModel
{
    public Guid Id { get; set; }
    public Guid ParentAlbumId { get; set; }
    public string? Title { get; set; }
    public string? Path { get; set; }
    public string? Summary { get; set; }
    public DateTime? DateTimeCreated { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? DateTimeModified { get; set; }
    public string ModifiedBy { get; set; }
}