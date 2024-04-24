using AuthServer.Interfaces;
using AuthServer.Models.Requests;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Models.Content.Requests
{
    public class FWRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedTime { get; set; }
        public string RequestStatus { get; set; } = string.Empty;

    }
}
