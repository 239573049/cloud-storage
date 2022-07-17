namespace CloudStorage.Application.Contracts.CloudStorages.Views;

public class UploadFileInput
{
    public byte[] Bytes { get; set; }

    public long Length { get; set; }

    public string? Name { get; set; }
}