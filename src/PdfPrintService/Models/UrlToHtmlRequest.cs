namespace PdfPrintService.Models;

public record UrlToPdfRequest(string FileName, string Url) : PdfRequest(FileName);