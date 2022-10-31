using Microsoft.AspNetCore.Http;
using Services.Helper.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public static class Utilites
    {
        public static UploadFileOutput UploadFile(IFormFile files, string fileTitle)
        {
            try
            {
                string uploadFolderName = files.ContentType.StartsWith("image") ? "Images" : "UploadedFiles";
                var folderName = Path.Combine("wwwroot",uploadFolderName );
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (files.Length > 0)
                {
                    var fileName = fileTitle;
                    //ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');

                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    if (!Directory.Exists(pathToSave))
                    {
                        Directory.CreateDirectory(pathToSave);
                    }
                    
                    RemoveUploadFile(fileTitle, uploadFolderName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        files.CopyTo(stream);
                    }

                    return new UploadFileOutput
                    {
                        IsUploaded = true,
                        FileUrl = dbPath
                    };
                }
                else
                {
                    return new UploadFileOutput
                    {
                        IsUploaded = false,
                        FileUrl = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                return new UploadFileOutput
                {
                    IsUploaded = false,
                    FileUrl = string.Empty
                };
            }
        }

        public static bool RemoveUploadFile(string fileTitle,string uploadFolderName)
        {
            var folderName = Path.Combine("wwwroot", uploadFolderName);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(pathToSave, fileTitle);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }
    }
}
