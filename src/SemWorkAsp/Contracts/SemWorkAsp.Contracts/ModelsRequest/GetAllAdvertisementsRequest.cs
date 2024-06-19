namespace SemWorkAsp.Contracts;

public class GetAllAdvertisementsRequest
{
    public int PageNumber { get; set; }
    public int Batchsize { get; set; } = 30;
}