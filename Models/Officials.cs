using System.ComponentModel.DataAnnotations;

namespace Gbms.Models
{
    public class officials
    {
        public string? off_id { get; set; }

        [Required(ErrorMessage = "Please upload a photo.")]
        public byte[]? photo { get; set; }
        public string? fname { get; set; }
        public string? mname { get; set; }
        public string? lname { get; set; }
        public string? ext { get; set; }
        public string? fullname { get; set; }
        public string? position { get; set; }
        public string? chairmanship { get; set; }
        public string? contact { get; set; }
        public string? purok { get; set; }
        public DateTime term_start { get; set; } = DateTime.Now;
        public DateTime term_end { get; set; } = DateTime.Now;    
    }
}
