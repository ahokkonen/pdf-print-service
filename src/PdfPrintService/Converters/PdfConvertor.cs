using System;
using System.IO;
using System.Threading.Tasks;
using PdfPrintService.Models;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PdfPrintService.Converters;

/// <summary>
/// Converts given content from HTML or URL to PDF using PuppeteerSharp.
/// </summary>
public class PdfConvertor
{
    static PdfConvertor()
    {
        new BrowserFetcher().DownloadAsync().Wait();
    }

    public static async Task<Stream> Convert(PdfRequest request)
    {
        await using var browser = await GetBrowser();
        await using var page = await browser.NewPageAsync();

        await page.EmulateMediaTypeAsync(MediaType.Screen);
        
        switch (request)
        {
            case HtmlToPdfRequest htmlRequest:
                await page.SetContentAsync(htmlRequest.Content);
                break;
            case UrlToPdfRequest urlRequest:
                await page.GoToAsync(urlRequest.Url);
                break;
            default:
                throw new ArgumentException("Invalid request type");
        }

        var options = GetPdfOptions();
        var content = await page.PdfStreamAsync(options);

        await browser.CloseAsync();

        return content;
    }

    private static async Task<IBrowser> GetBrowser()
    {
        return await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            Args = ["--no-sandbox"]
        });
    }

    private static PdfOptions GetPdfOptions()
    {
        return new PdfOptions
        {
            Format = PaperFormat.A4,
            PrintBackground = true,
        };
    }
}