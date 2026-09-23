using System.ComponentModel.DataAnnotations;

public class InvoiceDto
{
     public int Id { get; set; }
    
    [Required(ErrorMessage = "The product name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    public bool sendEmail { get; set; }

    public bool sendSMS { get; set; }

    public string? uploadedInvoiceUrl { get; set; }

    public DateOnly PaidAt { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }
    [Range(1, 15, ErrorMessage = "Days must be between 1 and 15")]
    public int notifyDaysBefore { get; set; }


    public IFormFile? uploadedInvoice { get; set; }
}