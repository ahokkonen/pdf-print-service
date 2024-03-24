using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdfPrintService.Converters;
using PdfPrintService.Models;

namespace PdfPrintService.Controllers;

[Route("api")]
public class PdfPrintController(ILogger<PdfPrintController> logger) : Controller
{
    [HttpPost("html-to-pdf")]
    public async Task<IActionResult> Print([FromBody] HtmlToPdfRequest request)
    {
        logger.LogInformation("Converting HTML to PDF");
        logger.LogDebug("Received request: {@Request}", request);
        
        return GetPdfFile(
            request.FileName,
            await PdfConvertor.Convert(request));
    }

    [HttpPost("url-to-pdf")]
    public async Task<IActionResult> Print([FromBody] UrlToPdfRequest request)
    {
        logger.LogInformation("Converting URL to PDF");
        logger.LogDebug("Received request: {@Request}", request);
        
        return GetPdfFile(
            request.FileName,
            await PdfConvertor.Convert(request));
    }

    private FileStreamResult GetPdfFile(string fileName, Stream stream)
    {
        return File(stream, "application/pdf", $"{fileName}.pdf");
    }
}