namespace CertBE.DTOs;

public class CertPostDTO
{
    public required int CategoryId { get; set; }
    public required string CertName { get; set; }
    public required float Price { get; set; }
}