namespace EmpCore.QueryStack.Dapper;

public class ReadOnlyConnectionString
{
    public string Value { get; }

    public ReadOnlyConnectionString(string connectionString)
    {
        if (String.IsNullOrWhiteSpace(connectionString))
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            throw new ArgumentException("Cannot be empty", nameof(connectionString));
        }

        Value = connectionString;
    }
}