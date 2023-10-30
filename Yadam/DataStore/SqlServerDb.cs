using Dapper;
using Yadam.Models;
using Yadam.ViewModels;
using Microsoft.Data.SqlClient;

namespace Yadam.DataStore;

public class SqlServerDb : IDataStore
{
    private readonly SqlConnection connection;
    private readonly string connectionString;
    
    public SqlServerDb()
    {
        connectionString = System.Environment.GetEnvironmentVariable("SQLAlbumConnectionString");
        connection = new SqlConnection(connectionString);
    }

    public List<AlbumModel> ReadAlbums()
    {
        var sqlString = "Exec ReadAlbums";
        
        connection.Open();
        var albumsList = connection.Query<AlbumModel>(sqlString).ToList();
        connection.Close();
        
        return albumsList;
    }

    public List<AlbumItemCombinedModel> ReadAlbumDetails(Guid albumId)
    {
        var sqlString = "Exec ReadAlbumDetails '" + albumId + "'";
        
        connection.Open();
        var albumDetails = connection.Query<AlbumItemCombinedModel>(sqlString).ToList();
        connection.Close();
        
        return albumDetails;
    }

    public Guid CreateAlbum(string title, string summary)
    {
        Guid parentAlbumId = Guid.Empty;
        
        var sqlString = "Exec CreateAlbum '";
        sqlString = sqlString + title + "','";
        sqlString = sqlString + summary +  "','";
        sqlString = sqlString + parentAlbumId +  "'";
        
        connection.Open();
        var albumId = connection.QueryFirstOrDefault<Guid>(sqlString);
        connection.Close();
        
        return albumId;
    }

    public int CreateAlbumItem(Guid albumId, int displayOrder, string fileName, string title, string description)
    {
        var sqlString = "Exec CreateAlbumItem '";
        sqlString = sqlString + albumId + "',";
        sqlString = sqlString + displayOrder + ",'";
        sqlString = sqlString + fileName +  "','";
        sqlString = sqlString + title +  "','";
        sqlString = sqlString + description +  "'";
        
        connection.Open();
        var itemId = connection.QueryFirstOrDefault<int>(sqlString);
        connection.Close();
        
        return itemId;
    }
}