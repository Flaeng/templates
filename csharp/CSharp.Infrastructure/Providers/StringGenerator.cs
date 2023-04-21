namespace CSharp.Infrastructure.Providers;

[Flags]
public enum Characters
{
    All,
    Letters,
    LowercaseLetters,
    UppercaseLetters,
    Numbers,
    SpecialCharacters
}
public interface IStringGenerator
{
    string GenerateString(int minLength, int maxLength, Characters characters = Characters.All);
}
public static class StringGeneratorExtensions
{
    public static string GenerateString(this IStringGenerator generator, int length, Characters characters = Characters.All)
    {
        return generator.GenerateString(length, length, characters);
    }
}
public class StringGenerator : IStringGenerator
{
    public string GenerateString(int minLength, int maxLength, Characters characters = Characters.All)
    {
        var random = new Random();
        char[] charArr = GetCharacters(characters);

        char[] result = new char[random.Next(maxLength - minLength) + minLength];
        for (int i = 0; i < result.Length; i++)
            result[i] = charArr[random.Next(charArr.Length)];

        return new string(result);
    }

    private char[] GetCharacters(Characters characters)
    {
        List<int> result = new();

        if (characters.HasFlag(Characters.All) 
            || characters.HasFlag(Characters.Letters)
            || characters.HasFlag(Characters.LowercaseLetters))
            result.AddRange(Enumerable.Range('a', 'z' - 'a'));

        if (characters.HasFlag(Characters.All) 
            || characters.HasFlag(Characters.Letters)
            || characters.HasFlag(Characters.UppercaseLetters))
            result.AddRange(Enumerable.Range('A', 'Z' - 'A'));

        if (characters.HasFlag(Characters.All) || characters.HasFlag(Characters.Numbers))
            result.AddRange(Enumerable.Range('0', '9' - '0'));

        if (characters.HasFlag(Characters.All) || characters.HasFlag(Characters.SpecialCharacters))
            result.AddRange(new int[] { '*', '!', '#', '%', '&', '=', '_', '-', '+', '?' });

        return result
            .Select(x => (char)x)
            .ToArray();
    }
}