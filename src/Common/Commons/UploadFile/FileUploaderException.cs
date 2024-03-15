namespace Common.Commons;

public class FileUploaderException : Exception
{
    public FileUploaderException()
    {
    }

    public FileUploaderException(string message) : base(message)
    {
    }
}