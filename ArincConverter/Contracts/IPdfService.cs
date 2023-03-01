namespace ArincConverter.Contracts
{
    public interface IPdfService
    {
        string TextToPdf(string header, string text);
        string TextToPdf(List<(string, string, string, string)> list);
        string TextToPdf(Dictionary<string, List<string>> dictionary);
        string ImagesToPdf(List<byte[]> files);
    }
}
