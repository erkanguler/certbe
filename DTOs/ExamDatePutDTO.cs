namespace CertBE.DTOs;

public class ExamDatePutDTO
{
    public int Id { get; set; }
    public DateTime ExamStartingTime { get; set; }
    public DateTime ExamEndingTime { get; set; }
}