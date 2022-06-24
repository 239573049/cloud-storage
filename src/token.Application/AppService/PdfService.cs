using System.Drawing;
using System.Drawing.Imaging;
using iText.Html2pdf;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using token.Application.Contracts.AppService;
using token.Application.Helpers;
using Volo.Abp.DependencyInjection;
using Image = iText.Layout.Element.Image;

namespace token.Application.AppService;

public class PdfService : IPdfService, ISingletonDependency
{
    private readonly ZipUtility _zipUtility;
    public PdfService(ZipUtility zipUtility)
    {
        _zipUtility = zipUtility;
    }

    /// <inheritdoc />
    public async Task<byte[]> MangePdfAsync(List<Stream> streams)
    {
        if(streams.Count < 1)
            throw new Exception("合并的PDF至少俩个以上文件");

        var memoryStream = new MemoryStream();
        var pdfDocument = new PdfDocument(new PdfReader(streams.First()), new PdfWriter(memoryStream));
        var merger = new PdfMerger(pdfDocument);

        var pdfList = streams.Skip(1).Select(x => new PdfDocument(new PdfReader(x)));

        foreach(var d in pdfList)
        {
            merger.Merge(d, 1, d.GetNumberOfPages());
        }

        pdfDocument.Close();

        streams.ForEach(x => x.Close());

        return await Task.FromResult(memoryStream.ToArray());
    }

    /// <inheritdoc />
    public async Task<byte[]> ImgToPdfAsync(List<Stream> streams)
    {
        var memoryStream = new MemoryStream();
        var pdfWriter = new PdfWriter(memoryStream);
        var pdfDocument = new PdfDocument(new PdfWriter(pdfWriter));
        var document = new Document(pdfDocument);

        foreach(var d in streams)
        {
            var bytes = new byte[d.Length];
            _ = await d.ReadAsync(bytes);
            d.Close();
            var imageData = ImageDataFactory.Create(bytes);

            var image = new Image(imageData);
            image.SetWidth(pdfDocument.GetDefaultPageSize().GetWidth() - 50);
            image.SetAutoScaleHeight(true);
            document.Add(image);
        }

        pdfDocument.Close();
        document.Close();
        var resultBytes = memoryStream.GetBuffer();
        memoryStream.Close();

        return resultBytes;
    }

    /// <inheritdoc />
    public async Task<byte[]> PdfToImgAsync(List<Stream> streams)
    {
        var dictionary = new Dictionary<string, Stream>();
        foreach (var s in streams)
        {
            var memoryStream = new MemoryStream();
            var doc = new Spire.Pdf.PdfDocument();
            doc.LoadFromStream(s);
            
            System.Drawing.Image emf = doc.SaveAsImage(0,Spire.Pdf.Graphics.PdfImageType.Bitmap);
            var zoomImg = new Bitmap(emf.Size.Width * 2, emf.Size.Height * 2);
            using (Graphics g = Graphics.FromImage(zoomImg))
            {
                g.ScaleTransform(2.0f, 2.0f);
                g.DrawImage(emf, new Rectangle(new Point(0, 0), emf.Size), new Rectangle(new Point(0, 0), emf.Size), GraphicsUnit.Pixel);
            }
            
            emf.Save(memoryStream, ImageFormat.Png);
            dictionary.Add($"{Guid.NewGuid():N}.png",memoryStream);
        }

        var zip=await _zipUtility.PackageManyZipAsync(dictionary);
        
        return await zip.GetAllBytesAsync();
    }
}
