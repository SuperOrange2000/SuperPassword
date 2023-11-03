﻿namespace SuperPassword.Shared.Contact
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

        public T Content { get; set; }
    }
}
