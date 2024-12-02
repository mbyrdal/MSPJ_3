using System.ComponentModel.DataAnnotations;

namespace ServiceAPI.Models
{
    public class ErrorViewModel
    {
        /// <summary>
        /// The unique identifier for the request, useful for debugging and tracking errors.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Indicates whether a RequestId is available.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>
        /// A short, user-friendly description of the error.
        /// </summary>
        [Required(ErrorMessage = "Error message is required.")]
        [StringLength(200, ErrorMessage = "Error message cannot exceed 200 characters.")]
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp of when the error occurred.
        /// </summary>
        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The HTTP status code associated with the error.
        /// </summary>
        [Range(100, 599, ErrorMessage = "HTTP status code must be between 100 and 599.")]
        public int? StatusCode { get; set; }

        /// <summary>
        /// Optional additional details for the error, useful for debugging.
        /// </summary>
        public string? Details { get; set; }

        public ErrorViewModel() { }

        public ErrorViewModel(string? requestId, string errorMessage, int? statusCode = null, string? details = null)
        {
            RequestId = requestId;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
            Details = details;
            Timestamp = DateTime.UtcNow;
        }
    }
}
