using System;
using System.Collections.Generic;


namespace MediReserva.Models

{
    public partial class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public ApiResponse(T? data, bool success=true, string? message=null)
        {
            Data = data;
            Message = message;
            Success = success;

        }
    }


}
