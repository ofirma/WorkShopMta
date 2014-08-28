using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PicPoint.Models;
using System.Data.SqlClient;
using System;
using System.IO;

public class UploadController : ApiController
{
    public Task<HttpResponseMessage> PostFormData()
    {
        // Check if the request contains multipart/form-data.
        if (!Request.Content.IsMimeMultipartContent())
        {
            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        }
        

        string root = HttpContext.Current.Server.MapPath("~/App_Data");
        var provider = new MultipartFormDataStreamProvider(root);

        // Read the form data and return an async task.
        var task = Request.Content.ReadAsMultipartAsync(provider).
            ContinueWith<HttpResponseMessage>(t =>
            {
                if (t.IsFaulted || t.IsCanceled)
                {
                    Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                }

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Photos photo = Photos.CreatePhotos(Guid.NewGuid().ToString());
                    FileStream stream = File.OpenRead(file.LocalFileName);
                    byte[] fileBytes = new byte[stream.Length];
                    string fileName = file.Headers.ContentDisposition.FileName.Remove(0, 1);
                    fileName = fileName.Remove(fileName.Length - 1, 1);
                    photo.name = fileName;

                    //stream.Read(fileBytes, 0, fileBytes.Length);
                    FileStream writer = File.Create(Path.Combine(root, fileName));
                    writer.Write(fileBytes, 0, fileBytes.Length);
                    writer.Close();
                    //stream.Close();
                    //photo.photo = fileBytes;

                    using (var reader = new ExifReader(file.LocalFileName))
                    {
                        double[] val;
                        reader.GetTagValue(ExifTags.GPSLongitude, out val);
                        photo.longitude1 = val[0];
                        photo.longitude2 = val[1];
                        photo.longitude3 = val[2];

                        reader.GetTagValue(ExifTags.GPSLatitude, out val);
                        photo.latitude1 = val[0];
                        photo.latitude2 = val[1];
                        photo.latitude3 = val[2];

                        string date = null;
                        reader.GetTagValue(ExifTags.DateTime, out date);
                        photo.date = GetDateFromString(date);
                    }
                    
                    DBEntities.Proxy.Photos.AddObject(photo);
                    DBEntities.Proxy.SaveChanges();
                    
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                var rowCount = new { RowCount = 10 };

                return Request.CreateResponse(HttpStatusCode.OK, rowCount, Configuration.Formatters.JsonFormatter);
            });

        return task;
    }

    private DateTime? GetDateFromString(string dateTime)
    {
        if (string.IsNullOrEmpty(dateTime))
        {
            return null;
        }
        string[] strs = dateTime.Split(' ');
        string[] date = strs[0].Split(':');
        string[] time = strs[1].Split(':');
        return new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]),
            int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
    }
}