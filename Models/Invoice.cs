namespace Ledgers.Models;
public class Invoice
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int notifyDaysBefore { get; set; }

    public bool sendEmail { get; set; }

    public bool sendSMS { get; set; }

    public string? uploadedInvoiceUrl { get; set; }

    public DateOnly PaidAt { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }
}