namespace Gbms.Models
{
    public class household
    {
        public string? house_id { get; set; }
        public string? fname { get; set; }
        public string? mname { get; set; }
        public string? lname { get; set; }
        public string? ext { get; set; }
        public string? fullname { get; set; }
        public string? gender { get; set; }
        public string? householdno { get; set; }
        public string? purok { get; set; }
        public int male { get; set; }
        public int female { get; set; }
        public int total { get; set; }
        public string? status { get; set; } = "Active";
    }
}
