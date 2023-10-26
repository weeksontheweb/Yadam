using Yadam.ViewModels;
using Yadam.Models;
using Yadam.DataStore;

namespace Yadam.Services
{
    public class AlbumService
    {
        public List<AlbumModel> ReadAlbums(IDataStore dataStore)
        {
            //return albums;
            return dataStore.ReadAlbums();
        }

        public List<AlbumItemCombinedModel> ReadAlbumDetails(Guid albumId, IDataStore dataStore)
        {
            //return albums;
            return dataStore.ReadAlbumDetails(albumId);


        }

        public Guid CreateAlbum(AlbumSummaryViewModel newAlbumSummary, IDataStore dataStore)
        {
            string rootPath = @"c:\\photo_album_app_images\\";

            //Guid newAlbumId = "";

            var newAlbumId = dataStore.CreateAlbum(newAlbumSummary);

            if (newAlbumId != System.Guid.Empty)
            {
                //newAlbum.Path = rootPath + newAlbumGuid;
                newAlbumSummary.Path = rootPath + newAlbumId;

                //Don't need to check that the directory exists as 
                //Directory.CreateDirectory() will not do anything if the 
                //directory already exists.
                Directory.CreateDirectory(newAlbumSummary.Path);
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