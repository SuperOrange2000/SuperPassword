using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.Entity
{
    public class ResponseBLL<T>
    {
        public string Message { get; set; }

        public HttpStatusCode Status { get; set; }

        public T? Content { get; set; }
    }
}
