namespace Yadam.Models;

public class AlbumItemCombinedModel
{
    public Guid AlbumId { get; set; }
    public Guid ParentAlbumId { get; set; }

    public string AlbumTitle { get; set; }

    public string AlbumSummary { get; set; }

    public DateTime AlbumDateTimeCreated { get; set; }
    public string AlbumCreatedBy { get; set; }
    
    public DateTime AlbumDateTimeModified { get; set; }

    public string AlbumModifiedBy { get; set; }
    
    //public List<AlbumItemModel> Items { get; set; }

    public int AlbumItemId { get; set; }

    public int? AlbumItemOrderId { get; set; }
    public string? AlbumItemFilename { get; set; }
    public string? AlbumItemTitle { get; set; }
    public string? AlbumItemSummary { get; set; }

    public DateTime AlbumItemDateTimeCreated { get; set; }

    public string AlbumItemCreatedBy { get; set; }

    public DateTime AlbumItemDateTimeModified { get; set; }

    public string AlbumItemModifiedBy { get; set; }
}