namespace CertBE.DTOs;

public class CertPutDTO
{
    public required int Id { get; set; }
    public required int CategoryId { get; set; }
    public required string CertName { get; set; }
    public required float Price { get; set; }
}