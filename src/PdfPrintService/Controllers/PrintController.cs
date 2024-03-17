using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PdfPrintService.Converters;
using PdfPrintService.Models;

namespace PdfPrintService.Controllers;

public class PrintController(DocumentConvertor documentConvertor) : Controller
{
    [HttpPost("pdf")]
    public async Task<IActionResult> Print([FromBody] PdfRequest request)
    {
        var pdfContent = await documentConvertor.ConvertToHtml(request.HtmlContent);
        return File(pdfContent, "application/pdf", $"{request.FileName}.pdf");
    }
}