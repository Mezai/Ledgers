using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ledgers.Areas.Identity.Data;
using Ledgers.Data;
using Ledgers.Models;

[Route("api/[controller]")]
[ApiController]
[IgnoreAntiforgeryToken]
public class FormController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IHostEnvironment _env;
    private readonly ILogger<FormController> _logger;

    public FormController(
        AppDbContext context,
        IHostEnvironment env,
        ILogger<FormController> logger)
    {
        _context = context;
        _env = env;
        _logger = logger;
    }

    [HttpPost("invoice")]
    public async Task<ActionResult<Invoice>> Create(
        [FromForm] InvoiceDto newInvoice)
    {
        if (newInvoice.uploadedInvoice is null || newInvoice.uploadedInvoice.Length == 0)
        {
            return BadRequest("Please select an invoice file.");
        }

        var extension = Path.GetExtension(newInvoice.uploadedInvoice.FileName).ToLowerInvariant();
        var allowedExtensions = new[] { ".pdf", ".png", ".jpg", ".jpeg" };
        if (!allowedExtensions.Contains(extension))
        {
            return BadRequest("Only PDF, PNG, JPG, and JPEG files are allowed.");
        }

        if (newInvoice.uploadedInvoice.Length > 15 * 1024 * 1024)
        {
            return BadRequest("The file must be 15 MB or smaller.");
        }

        var uploadDirectory = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads");
        Directory.CreateDirectory(uploadDirectory);

        var storedFileName = $"{Path.GetRandomFileName()}{extension}";
        var storedFilePath = Path.Combine(uploadDirectory, storedFileName);

        await using (var fileStream = System.IO.File.Create(storedFilePath))
        {
            await newInvoice.uploadedInvoice.CopyToAsync(fileStream);
        }

        var invoice = new Invoice
        {
            Name = newInvoice.Name,
            notifyDaysBefore = newInvoice.notifyDaysBefore,
            sendEmail = newInvoice.sendEmail,
            sendSMS = newInvoice.sendSMS,
            PaidAt = newInvoice.PaidAt,
            CreatedAt = newInvoice.CreatedAt,
            UpdatedAt = newInvoice.UpdatedAt,
            uploadedInvoiceUrl = $"/uploads/{storedFileName}"
        };

        _context.Invoice.Add(invoice);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Create), new { id = invoice.Id }, invoice);
    }
}