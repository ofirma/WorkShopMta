﻿using System.Diagnostics;
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

                    stream.Read(fileBytes, 0, fileBytes.Length);
                    stream.Close();
                    photo.photo = fileBytes;
                    using (var ctx = new Database1Entities1())
                    {
                        ctx.Photos.AddObject(photo);
                        ctx.SaveChanges();
                    }
                    
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                var rowCount = new { RowCount = 10 };

                return Request.CreateResponse(HttpStatusCode.OK, rowCount, Configuration.Formatters.JsonFormatter);
            });

        return task;
    }
}