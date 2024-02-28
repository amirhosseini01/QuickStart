namespace Api.Common;

public class FileUploaderException : Exception
{
    public FileUploaderException()
    {
    }

    public FileUploaderException(string message): base(message)
    {
    }
}