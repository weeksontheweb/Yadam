using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Yadam.Models;
using Microsoft.AspNetCore.Mvc;
using Yadam.ViewModels;
using Yadam.Models;
using System.Diagnostics;
using Yadam.Services;
using Yadam.DataStore;

namespace Yadam.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        //PostgresDb dataStore = new PostgresDb();
        SqlServerDb dataStore = new SqlServerDb();
        AlbumService albumService = new AlbumService();

        return View(albumService.ReadAlbums(dataStore));
    }

    [HttpPost]
    public IActionResult CreateAlbum(string titleStore, string descriptionStore)
    {
        //ID and DateTimeCreated are done in the database.
        AlbumService albumService = new AlbumService();
        
        //PostgresDb dataStore = new PostgresDb();
        SqlServerDb dataStore = new SqlServerDb();
        
        var newAlbumGuid = albumService.CreateAlbum(titleStore, descriptionStore,dataStore);

        //If the GUID is null then return an error description.
        return (RedirectToAction("Index"));

    }

    public IActionResult DisplayFullAlbum(Guid albumId)
    {
        //Get the Album details from the database.
        AlbumService albumService = new AlbumService();
        //PostgresDb dataStore = new PostgresDb();
        SqlServerDb dataStore = new SqlServerDb();

        var albumItemCombinedModel = albumService.ReadAlbumDetails(albumId, dataStore);
        var albumRootPath = System.Environment.GetEnvironmentVariable("AlbumRootPath");
        
        //Look through directory and get images.
        //Also need to get any sub-photo albums.
        string[] fileNames = Directory.GetFiles(albumRootPath + albumId);

        List<AlbumModel> albumItems = new List<AlbumModel>();

        AlbumViewModel albumViewModel = new AlbumViewModel();
        albumViewModel.Items = new List<AlbumItemModel>();

        foreach (var albumItemCombinedModelRow in albumItemCombinedModel)
        {
            //Iterate through the rows and build up the ViewModel.
            if (albumViewModel.AlbumId == System.Guid.Empty)
            {
                //Guid is blank, so fill in root table info.
                albumViewModel.AlbumId = albumItemCombinedModelRow.AlbumId;
                albumViewModel.AlbumTitle = albumItemCombinedModelRow.AlbumTitle;
                albumViewModel.AlbumSummary = albumItemCombinedModelRow.AlbumSummary;
                albumViewModel.AlbumRootPath = albumRootPath;
            }

            if (albumItemCombinedModelRow.AlbumItemId != 0)
            {
                AlbumItemModel newAlbumItem = new AlbumItemModel();

                newAlbumItem.AlbumId = albumItemCombinedModelRow.AlbumId;
                newAlbumItem.Id = albumItemCombinedModelRow.AlbumItemId;
                newAlbumItem.Summary = albumItemCombinedModelRow.AlbumItemSummary;
                newAlbumItem.Filename = albumItemCombinedModelRow.AlbumItemFilename;
                newAlbumItem.Title = albumItemCombinedModelRow.AlbumItemTitle;
                newAlbumItem.OrderId = albumItemCombinedModelRow.AlbumItemOrderId;
                albumViewModel.Items.Add(newAlbumItem);
            }
        }

        return View(albumViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddItem(Guid albumId, int displayOrder, string titleStore, string descriptionStore, string filePathStore)
    {
        //Allow input of item via browse, title and description.
        //Need to call stored procedure.
        //Need to copy the file to the new destination.
        
        var albumRootPath = System.Environment.GetEnvironmentVariable("AlbumRootPath");

        AlbumService albumService = new AlbumService();
        //PostgresDb dataStore = new PostgresDb();
        SqlServerDb dataStore = new SqlServerDb();

        //if (File.Exists(filePath))
       // {
            var fileStream = new FileStream(filePathStore, FileMode.Open);
            var fileName = Path.GetFileName(filePathStore);
            var formFile = new FormFile(fileStream, 0, fileStream.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream" // Set the appropriate content type
            };

       // }
       
        // Combine the Samba share path with the filename
        var sambaPath = Path.Combine(albumRootPath + albumId, fileName);

        if (Directory.Exists(albumRootPath + albumId))
        {
            
        }
        else
        {
            try
            {
                Directory.CreateDirectory(albumRootPath + albumId);
                Console.WriteLine("Directory created successfully.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
        }

        var stream1 = new FileStream(sambaPath, FileMode.Create);

        await fileStream.CopyToAsync(stream1);

        var albumItemCombinedModel = albumService.CreateAlbumItem(albumId, displayOrder, fileName, titleStore,descriptionStore,dataStore);

        return RedirectToAction("Index");
    }

    public IActionResult GetSambaImage(string imagePath)
    {
        // Replace with the actual path to your SAMBA share image
        string sambaImagePath = imagePath;

        // Read the image file from the SAMBA share
        byte[] imageBytes = System.IO.File.ReadAllBytes(sambaImagePath);

        // Determine the content type (e.g., image/jpeg)
        string contentType = "image/jpeg";

        // Return the image as a FileResult
        return File(imageBytes, contentType);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}