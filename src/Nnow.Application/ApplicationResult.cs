namespace Nnow.Application;

public class ApplicationResult
{
    public bool IsSuccess { get { return Errors.Count == 0; } }
    public List<string> Errors { get; set; } = new List<string>();
}
