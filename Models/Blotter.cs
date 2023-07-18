namespace Gbms.Models
{
    public class blotter
    {
        public string? type { get; set; } = "Blotter";
        public string? blotter_id { get; set; }
        public string? case_no { get; set; }
        public string? res_id_c { get; set; }
        public string? complainant { get; set; }
        public string? res_id_r { get; set; }
        public string? respondent { get; set; }
        public string? title { get; set; }
        public string? details { get; set; }

        //Received Date
        public DateTime rdate { get; set; } = DateTime.Now;
        //Schedule Date
        public DateTime sched { get; set; } = DateTime.Now;
        public double _fee { get; set; }
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
        public string? status { get; set; }
    }
}
