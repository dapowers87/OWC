using System;

namespace Domain
{
    public class Temperature
    {
        public int Id { get; set; }
        public decimal Temp { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
