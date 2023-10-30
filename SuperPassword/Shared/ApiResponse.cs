﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperPassword.Shared.Contact
{
    public class ApiResponse
    { 
        public string Message { get; set; }

        public bool Status { get; set; }

        public object Result { get; set; }
    }

    public class ApiResponse<T>
    {
        public string Message { get; set; }

        public string Status { get; set; }

        public T Result { get; set; }
    }
}
