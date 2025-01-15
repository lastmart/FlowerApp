namespace FlowerApp.Service.Extensions;

public static class StringExtensions
{
    public static string Capitalize(this string source)
    {
        if (string.IsNullOrEmpty(source))
            return string.Empty;
        source = source.ToLower();
        var letters = source.ToCharArray();
        letters[0] = char.ToUpper(letters[0]);
        return new string(letters);
    }
}