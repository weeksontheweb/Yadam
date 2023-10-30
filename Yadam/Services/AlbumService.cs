using Yadam.ViewModels;
using Yadam.Models;
using Yadam.DataStore;

namespace Yadam.Services
{
    public class AlbumService
    {
        public List<AlbumModel> ReadAlbums(IDataStore dataStore)
        {
            return dataStore.ReadAlbums();
        }

        public List<AlbumItemCombinedModel> ReadAlbumDetails(Guid albumId, IDataStore dataStore)
        {
            //return albums;
            return dataStore.ReadAlbumDetails(albumId);
        }

        public Guid CreateAlbum(string title, string description, IDataStore dataStore)
        {
            string rootPath = @"c:\\photo_album_app_images\\";

           var newAlbumId = dataStore.CreateAlbum(title, description);

           if (newAlbumId != System.Guid.Empty) {

                var albumRootPath = System.Environment.GetEnvironmentVariable("AlbumRootPath");

                var albumPath = albumRootPath + newAlbumId;

                Directory.CreateDirectory(albumPath);
          }

          return newAlbumId;
        }

        public int CreateAlbumItem(Guid albumId, int displayOrder, string fileName, string title, string description,
            IDataStore dataStore)
        {
            var id = dataStore.CreateAlbumItem(albumId, displayOrder, fileName, title, description);

            return id;
        }
    }
}