namespace CertBE.DTOs;

public class ExamDateDTO
{
    public int Id { get; set; }
    public string Category { get; set; } = null!;
    public string CertName { get; set; } = null!;
    public float Price { get; set; }
    public DateTime ExamStartingTime { get; set; }
    public DateTime ExamEndingTime { get; set; }
}