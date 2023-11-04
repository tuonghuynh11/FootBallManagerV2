namespace FootBallManagerAPI.Models
{
    public class DoiBongSupplierModel
    {
        public string IdDoiBong { get; set; } = null!;

        public int IdSupplier { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Duration { get; set; }

        public int? Status { get; set; }
    }
}
