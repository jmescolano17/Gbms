using System.ComponentModel.DataAnnotations;

namespace Gbms.Models
{
    public class residents
    {
        public string? res_id { get; set; }

        [Required(ErrorMessage = "Please upload a photo.")]
        public byte[]? photo { get; set; }
        public string? house_id { get; set; }
        public string? fname { get; set; }
        public string? mname { get; set; }
        public string? lname { get; set; }
        public string? ext { get; set; }
        public string? fullname { get; set; }
        public string? household { get; set; }
        public string? purok { get; set; }
        public string? birthplace { get; set; }
        public DateTime birthdate { get; set; } = DateTime.Now;
        public int age { get; set; }
        public string? gender { get; set; }
        public string? civil { get; set; }
        public string? citizenship { get; set; }
        public string? occupation { get; set; }
        public string? status { get; set; } = "Active";

        //Blotter
        public string? complainant { get; set; }
        public string? respondent { get; set; }

        public string? token { get; set; }
    }
}
