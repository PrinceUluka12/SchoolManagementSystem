namespace School.Models
{
    public class TuitionPayment
    {
        public int TuitionPaymentId { get; set; }
        public int StudentId { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public Student Student { get; set; }
    }
}
