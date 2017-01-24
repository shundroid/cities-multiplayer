using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Google_Drive_Test
{
  class Program
  {
    // If modifying these scopes, delete your previously saved credentials
    // at ~/.credentials/drive-dotnet-quickstart.json
    static string[] Scopes = { DriveService.Scope.Drive };
    static string ApplicationName = "Drive API .NET Quickstart";

    static void Main(string[] args)
    {
      UserCredential credential;

      using (var stream =
          new System.IO.FileStream("client_secret.json", System.IO.FileMode.Open, System.IO.FileAccess.Read))
      {
        string credPath = Environment.GetFolderPath(
            Environment.SpecialFolder.Personal);
        credPath = System.IO.Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            GoogleClientSecrets.Load(stream).Secrets,
            Scopes,
            "user",
            CancellationToken.None,
            new FileDataStore(credPath, true)).Result;
        Console.WriteLine("Credential file saved to: " + credPath);
      }

      // Create Drive API service.
      var service = new DriveService(new BaseClientService.Initializer()
      {
        HttpClientInitializer = credential,
        ApplicationName = ApplicationName,
      });

      var fileMetadata = new File();
      fileMetadata.Name = "Project plan";
      fileMetadata.MimeType = "application/vnd.google-apps.drive-sdk";
      var request = service.Files.Create(fileMetadata);
      request.Fields = "id";
        var file = request.Execute();
      Console.WriteLine("File ID: " + file.Id);

      Console.ReadLine();
    }
  }
}
