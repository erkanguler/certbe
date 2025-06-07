using CertBE.Models;

namespace CertBE.DTOs;

public class BookingPostDTO
{
    public int ExamId { get; set; }
    public DateTime ExamStartingTime { get; set; }
    public DateTime ExamEndingTime { get; set; }
    public required string CustomerFirstName { get; set; }
    public required string CustomerLastName{ get; set; }
    public required string CustomerEmail{ get; set; }
    public required string CustomerPassword{ get; set; } 
}