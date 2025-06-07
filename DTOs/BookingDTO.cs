namespace CertBE.DTOs;

public class BookingDTO
{
    public string Category { get; set; } = null!;
    public string CertName { get; set; } = null!;
    public float Price { get; set; }
    public DateTime ExamStartingTime { get; set; }
    public DateTime ExamEndingTime { get; set; }
    public string CustomerFirstName { get; set; } = null!;
    public string CustomerLastName { get; set; } = null!;
}