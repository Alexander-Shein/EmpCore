namespace EmpCore.Domain;

public class Failure : ValueObject
{
    private const string AllowedCodeCharacters = "abcdefghijklmnopqrstuvwxyz_1234567890";

    public string Code { get; }
    public string Message { get; }

    public Failure(string code, string message)
    {
        ValidateCode(code);
        ValidateMessage(message);

        Code = code.Trim();
        Message = message.Trim();
    }

    private static void ValidateCode(string code)
    {
        const string EmptyMessage = "Code cannot be empty or consist only of white-space characters.";
        const string InvalidFormatMessage = "Code must consist only of lowercase letters, digits and underscores.";
        const string InvalidFirstCharMessage = "Code cannot start with '_'.";
        const string InvalidLastCharMessage = "Code cannot end with '_'.";

        if (String.IsNullOrWhiteSpace(code))
        {
            if (code == null) throw new ArgumentNullException(nameof(code));
            throw new ArgumentException(EmptyMessage, nameof(code));
        }
        if (!code.All(c => AllowedCodeCharacters.Contains(c))) throw new FormatException(InvalidFormatMessage);
        if (code.StartsWith('_')) throw new FormatException(InvalidFirstCharMessage);
        if (code.EndsWith('_')) throw new FormatException(InvalidLastCharMessage);
    }

    private static void ValidateMessage(string message)
    {
        const string EmptyMessage = "Message cannot be empty or consist only of white-space characters.";

        if (String.IsNullOrWhiteSpace(message))
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            throw new ArgumentException(EmptyMessage, nameof(message));
        }
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Code;
    }
}
