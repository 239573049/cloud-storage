using Microsoft.AspNetCore.Mvc;
using token.Application.Contracts.AppService;
using token.Domain.Shared;
using Volo.Abp;

namespace token.Controllers;

/// <summary>
/// Pdf服务
/// </summary>
[Route("api/pdf")]
[ApiController]
public class PdfController:ControllerBase
{
    private readonly IPdfService _pdfService;
    /// <inheritdoc />
    public PdfController(IPdfService pdfService)
    {
        _pdfService = pdfService;
    }

    /// <summary>
    /// 合并Pdf
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    [HttpPost("mange-pdf")]
    public async Task<IActionResult> MangePdfAsync(List<IFormFile> files)
    {
        if (files.Any(x => !x.FileName.EndsWith(".pdf")))
        {
            throw new BusinessException(message: "存在后缀名不为pdf的文件");
        }
        
        var stream =  files.Select(x=>x.OpenReadStream()).ToList();
        var result = await _pdfService.MangePdfAsync(stream);
        return new FileStreamResult(new MemoryStream(result), FileType.Pdf)
        {
            FileDownloadName = $"{Guid.NewGuid():N}合并后的文件.pdf"
        };
    }

    /// <summary>
    /// 图片转Pdf
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    [HttpPost("img-to-pdf")]
    public async Task<IActionResult> ImgToPdfAsync(List<IFormFile> files)
    {
        var stream =  files.Select(x=>x.OpenReadStream()).ToList();
        var result = await _pdfService.ImgToPdfAsync(stream);
        
        return new FileStreamResult(new MemoryStream(result), FileType.Pdf)
        {
            FileDownloadName = $"{Guid.NewGuid():N}图片转换Pdf.pdf"
        };
    }

    /// <summary>
    /// Pdf转换图片
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    [HttpPost("pdf-to-img")]
    public async Task<IActionResult> PdfToImgAsync(List<IFormFile> files)
    {
        if (files.Any(x => !x.FileName.EndsWith(".pdf")))
        {
            throw new BusinessException(message: "存在后缀名不为pdf的文件");
        }
        
        var stream =  files.Select(x=>x.OpenReadStream()).ToList();
        var result = await _pdfService.PdfToImgAsync(stream);
        
        return new FileStreamResult(new MemoryStream(result), FileType.Stream)
        {
            FileDownloadName = $"{Guid.NewGuid():N}图片转换Pdf.zip"
        };
    }
}
