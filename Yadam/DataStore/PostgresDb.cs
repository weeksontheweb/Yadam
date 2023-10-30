using Dapper;
using Npgsql;
using Yadam.Models;
using Yadam.ViewModels;

namespace Yadam.DataStore;

public class PostgresDb : IDataStore
{
    private readonly NpgsqlConnection connection;
    private readonly string connectionString;
    
    public PostgresDb()
    {
        connectionString = System.Environment.GetEnvironmentVariable("PhotoAlbumConnectionString");

        connection = new NpgsqlConnection(connectionString);
    }
    
    public List<AlbumModel> ReadAlbums()
    {
        connection.Open();

        var albumsList = connection.Query<AlbumModel>("Select id, name, description, path, date_time_created from albums").ToList();
    
        connection.Close(); 
       
        return albumsList;
    }

    public List<AlbumItemCombinedModel> ReadAlbumDetails(Guid albumId)
    {
        connection.Open();

        var parameters = new
        {
            falbum_id = albumId
        };
/*
       var albumItems = connection.Query<AlbumItemModel>(
            "SELECT * from read_album_details(@falbum_id)",
            parameters
        ).ToList();
*/
        var albumItems = connection.Query<AlbumItemCombinedModel>(
            "SELECT * from read_album_details(@falbum_id)",
            parameters
        ).ToList();

        connection.Close();

        return albumItems;
    }

    public Guid CreateAlbum(string title, string description)
    {
        return System.Guid.Empty;
    }
    
    public int CreateAlbumItem(Guid albumId, int displayOrder, string fileName, string title, string description)
    {
        return 1;
    }
}