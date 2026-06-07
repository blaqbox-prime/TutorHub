using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.DTO

{
    public class ApiResponse<T>
    {
        public T? Data { get; set; } = default(T);
        public string? Message { get; set; } = string.Empty;
        public string? Error { get; set; } = string.Empty; 
        public ApiResponse() { }
    }
}
