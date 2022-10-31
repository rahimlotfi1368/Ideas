using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper.Dtos
{
    public class ServiceResponceDto
    {
        public bool Statuse { get; set; }
        public string? Message { get; set; }
        public object? Result { get; set; }
    }
}
