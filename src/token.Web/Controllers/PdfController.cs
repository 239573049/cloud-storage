using Microsoft.AspNetCore.Mvc;
using token.Application.Contracts.AppService;
using token.Domain.Shared;

namespace token.Controllers;

/// <summary>
/// Pdf服务
/// </summary>
[Route("api/[controller]")]
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
            FileDownloadName = $"{Guid.NewGuid():N}合并后的文件.pdf"
        };
    }
}
