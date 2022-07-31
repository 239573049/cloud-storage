namespace token.Hubs.Views;

public class FileStreamView
{
    public Guid? StorageId { get; set; }

    public string? FileName { get; set; }

    public long Length { get; set; }
}