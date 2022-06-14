using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using Volo.Abp.DependencyInjection;

namespace token.Application.Helpers;


public class ZipUtility : ISingletonDependency
{
    /// <summary>
    /// 压缩Stream
    /// </summary>
    /// <param name="streams">文件名|需要压缩的Stream</param>
    /// <returns>压缩的Stream</returns>
    public async Task<Stream> PackageManyZipAsync(Dictionary<string, Stream> streams)
    {
        var buffer = new byte[6500];
        MemoryStream returnStream = new MemoryStream();
        var zipMs = new MemoryStream();

        //解决压缩中文文件名称乱码
        ZipConstants.DefaultCodePage = Encoding.GetEncoding("UTF-8").CodePage;
        using (var zipStream = new ZipOutputStream(zipMs))
        {
            zipStream.SetLevel(9);
            foreach (var kv in streams)
            {
                string fileName = kv.Key;
                using (var streamInput = kv.Value)
                {
                    zipStream.PutNextEntry(new ZipEntry(fileName));
                    await zipStream.WriteAsync(kv.Value.GetAllBytes());
                    while (true)
                    {
                        var readCount = streamInput.Read(buffer, 0, buffer.Length);
                        if (readCount > 0)
                        {
                            zipStream.Write(buffer, 0, readCount);
                        }
                        else
                        {
                            break;
                        }
                    }
                    zipStream.Flush();
                }
            }
            zipStream.Finish();
            zipMs.Position = 0;
            zipMs.CopyTo(returnStream, 5600);
        }
        returnStream.Position = 0;
        return returnStream;
    }
}