using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Attributes
{
    public class AllowFileSizeAttribute : ValidationAttribute
    {
        /// <summary>  
        /// Gets or sets file size property. Default is 1GB (the value is in Bytes).  
        /// </summary>  
        public long FileSize { get; set; } = 1 * 1024 * 1024 * 1024;
        public override bool IsValid(object? value)
        {
            IFormFile formFile = value as IFormFile;

            long allowedFileSize = this.FileSize;

            bool isValid = true;

            if (formFile != null)
            {
                // Initialization.  
                var fileSize = formFile.Length;

                // Settings.  
                isValid = fileSize <= allowedFileSize;
            }

            return isValid;
        }
    }
}
