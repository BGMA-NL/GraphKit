namespace GraphKit.Infrastructure.Internal
{
    public class Attachment
    {
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public required string FileBase64  { get; set; }
    }
}