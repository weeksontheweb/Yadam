using Yadam.ViewModels;
using Yadam.Models;

namespace Yadam.DataStore;

public interface IDataStore
{
    public List<AlbumModel> ReadAlbums();

    public List<AlbumItemCombinedModel> ReadAlbumDetails(Guid albumId);

    public Guid CreateAlbum(AlbumSummaryViewModel newAlbumSummary);

    public int CreateAlbumItem(Guid albumId, int displayOrder, string fileName, string title, string description);
}