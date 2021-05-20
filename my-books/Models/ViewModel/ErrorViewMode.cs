﻿using Newtonsoft.Json;

namespace my_books.Models.ViewModel
{
    public class ErrorViewMode
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}