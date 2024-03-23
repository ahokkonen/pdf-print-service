using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PdfPrintService.Converters;
using PdfPrintService.Models;

namespace PdfPrintService.Controllers;

public class PrintController : Controller
{
    [HttpPost("html/pdf")]
    public async Task<IActionResult> Print([FromBody] HtmlToPdfRequest request)
    {
        return GetPdfFile(
            request.FileName,
            await DocumentConvertor.ToPdf(request));
    }

    [HttpPost("url/pdf")]
    public async Task<IActionResult> Print([FromBody] UrlToPdfRequest request)
    {
        return GetPdfFile(
            request.FileName,
            await DocumentConvertor.ToPdf(request));
    }

    private FileStreamResult GetPdfFile(string fileName, Stream stream)
    {
        return File(stream, "application/pdf", $"{fileName}.pdf");
    }
}