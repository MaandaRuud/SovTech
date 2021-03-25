using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SovTech.Models
{
    public class RequestModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public dynamic Id { get; set; }
    }
}
