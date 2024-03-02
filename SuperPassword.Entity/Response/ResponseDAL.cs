using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.Entity
{
    public class ResponseDAL
    {
        public HttpStatusCode Status { get; set; }

        public string? Content { get; set; }
    }
}
