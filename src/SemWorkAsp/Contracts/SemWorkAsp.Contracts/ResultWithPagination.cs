namespace SemWorkAsp.Contracts;

public class ResultWithPagination<T>
{
    public IEnumerable<T> Result { get; set; }
    public int AvailablePages { get; set; }
}