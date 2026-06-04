using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.DTO

{
    public class ApiResponse
    {
        public object? Data { get; set; } = null;
        public string? Message { get; set; } = string.Empty;
        public string? Error { get; set; } = string.Empty; 
        public ApiResponse() { }
    }
}
