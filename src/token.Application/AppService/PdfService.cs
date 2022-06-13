using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using token.Application.Contracts.AppService;
using Volo.Abp.DependencyInjection;

namespace token.Application.AppService;

public class PdfService:IPdfService,ISingletonDependency
{

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
            _=await d.ReadAsync(bytes);
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
}
