namespace Gbms.Models
{
    public class business
    {
        public string? type { get; set; } = "Brgy. Business Clearance";
        public string? bsns_id  { get; set; }
        public string? res_id { get; set; }
        public string? fname { get; set; }
        public string? mname { get; set; }
        public string? lname { get; set; }
        public string? ext { get; set; }
        public string? fullname { get; set; }
        public string? bsns_name { get; set; }
        public string? purok { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public double _fee { get; set; }
        public string? stat { get; set; } = "Pending";
        public string? fee
        {
            get { return _fee.ToString("0.00"); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _fee = 0;
                }
                else
                {
                    _fee = double.Parse(value);
                }

            }
        }

        public string? receipt { get; set; }
        public string? clerk { get; set; }
    }
}
