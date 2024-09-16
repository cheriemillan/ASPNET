using System;

namespace Testing.Models
{
    //properties for ErrorViewMode;
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
