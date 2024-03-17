using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PdfPrintService.Converters;

/// <summary>
/// Converts HTML to PDF using PuppeteerSharp.
/// </summary>
public class DocumentConvertor
{
    public async Task<Stream> ConvertToHtml(string html)
    {
        var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            Args = ["--no-sandbox"]
        });

        await using var page = await browser.NewPageAsync();

        await page.EmulateMediaTypeAsync(MediaType.Screen);
        await page.SetContentAsync(html);

        var content = await page.PdfStreamAsync(new PdfOptions
        {
            Format = PaperFormat.A4,
            PrintBackground = true
        });

        await browser.CloseAsync();

        return content;
    }
}