using Yadam.Models;

namespace Yadam.ViewModels;

public class AlbumViewModel
{
    public Guid AlbumId { get; set; }
    public string AlbumTitle { get; set; }
    public string AlbumSummary { get; set; }
    
    public string AlbumRootPath { get; set; }
    public List<AlbumItemModel> Items { get; set; }
}