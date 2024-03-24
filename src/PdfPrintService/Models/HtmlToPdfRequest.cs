namespace PdfPrintService.Models;

public record HtmlToPdfRequest(string FileName, string Content) : PdfRequest(FileName);