namespace CertBE.DTOs;

public class ExamDatePostDTO
{
    public int CertId { get; set; }
    public DateTime ExamStartingTime { get; set; }
    public DateTime ExamEndingTime { get; set; }
}