public class MyTestClass
{
    private string _username;
    private string _password;

    public MyTestClass(string username, string password)
    {
        _username = username ?? throw new ArgumentNullException(nameof(username));
        _password = password ?? throw new ArgumentNullException(nameof(password));
    }
}
