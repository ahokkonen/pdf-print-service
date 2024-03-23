using System.IO;
using System.Threading.Tasks;
using PdfPrintService.Models;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PdfPrintService.Converters;

/// <summary>
/// Converts given content from HTML or URL to PDF using PuppeteerSharp.
/// </summary>
public class DocumentConvertor
{
    static DocumentConvertor()
    {
        new BrowserFetcher().DownloadAsync().Wait();
    }

    public static async Task<Stream> ToPdf(HtmlToPdfRequest request)
    {
        await using var browser = await GetBrowser();
        await using var page = await browser.NewPageAsync();

        await page.EmulateMediaTypeAsync(MediaType.Screen);

        await page.SetContentAsync(request.Content);

        var options = GetPdfOptions();
        var content = await page.PdfStreamAsync(options);

        await browser.CloseAsync();

        return content;
    }

    public static async Task<Stream> ToPdf(UrlToPdfRequest request)
    {
        await using var browser = await GetBrowser();
        await using var page = await browser.NewPageAsync();

        await page.EmulateMediaTypeAsync(MediaType.Screen);

        await page.GoToAsync(request.Url);

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