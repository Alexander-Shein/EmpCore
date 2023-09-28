namespace EmpCore.Domain;

/// <summary>
/// https://en.wikipedia.org/wiki/Design_by_contract
/// </summary>
public static class Contracts
{
    public static void Require(bool precondition, string message = "")
    {
        if (!precondition)
            throw new ContractException(message);
    }
    
    [Serializable]
    public class ContractException : Exception
    {
        public ContractException() { }
        public ContractException(string message) : base(message) { }
        public ContractException(string message, Exception inner) : base(message, inner) { }
    }
}

