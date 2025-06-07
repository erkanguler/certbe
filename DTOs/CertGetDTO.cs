namespace CertBE.DTOs;

public class CertGetDTO
{
    public int Id { get; set; }
    public required string Category { get; set; }
    public required string CertName { get; set; }
    public required float Price { get; set; }
}