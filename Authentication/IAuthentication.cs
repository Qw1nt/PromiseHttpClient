namespace HttpClient.Authentication
{
    public interface IAuthentication
    {
        (string, string) GetHeader();
    }
}