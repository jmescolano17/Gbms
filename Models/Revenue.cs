namespace Gbms.Models
{
    public class revenue
    {
        public DateTime date { get; set; }
        public string? fullname { get; set; }
        public string? type { get; set; }
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

        public double _total { get; set; }
        public string? total
        {
            get { return _total.ToString("0.00"); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _total = 0;
                }
                else
                {
                    _total = double.Parse(value);
                }

            }
        }

    }
}

