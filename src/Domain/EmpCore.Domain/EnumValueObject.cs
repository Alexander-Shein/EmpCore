using System.Reflection;
using System.Text.RegularExpressions;

namespace EmpCore.Domain;

[Serializable]
public abstract class EnumValueObject<TEnumeration> : ValueObject
    where TEnumeration : EnumValueObject<TEnumeration>
{
    private int? _cachedHashCode;
        
    private static readonly Dictionary<string, TEnumeration> Enumerations =
        GetEnumerations().ToDictionary(e => e.Id, StringComparer.OrdinalIgnoreCase);

    protected EnumValueObject(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            if (id == null) throw new ArgumentNullException(nameof(id),"The enum key cannot be null.");
            throw new ArgumentException("The enum key cannot be empty.");
        }

        Id = id.Trim();
    }

#if NET40
        public static ICollection<TEnumeration> All = Enumerations.Values.OfType<TEnumeration>().ToList();
#else
    public static IReadOnlyList<TEnumeration> All = Enumerations.Values.OfType<TEnumeration>().ToList();
#endif

    public virtual string Id { get; protected set; }
        
    public static bool operator ==(EnumValueObject<TEnumeration> a, string b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;

        return a.Id.Equals(b);
    }

    public static bool operator !=(EnumValueObject<TEnumeration> a, string b)
    {
        return !(a == b);
    }

    public static bool operator ==(string a, EnumValueObject<TEnumeration> b)
    {
        return b == a;
    }

    public static bool operator !=(string a, EnumValueObject<TEnumeration> b)
    {
        return !(b == a);
    }
        
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (GetUnproxiedType(this) != GetUnproxiedType(obj)) return false;

        var enumValueObject = (EnumValueObject<TEnumeration>)obj;

        return GetEqualityComponents().SequenceEqual(enumValueObject.GetEqualityComponents());
    }
        
    public override int GetHashCode()
    {
        if (!_cachedHashCode.HasValue)
        {
            _cachedHashCode = GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        return _cachedHashCode.Value;
    }
        
    public static Result<TEnumeration> FromId(string id)
    {
        if (id == null) return GetInvalidEnumFailure(String.Empty);
        if (String.IsNullOrWhiteSpace(id)) return GetInvalidEnumFailure(id);
        
        return Enumerations.ContainsKey(id.Trim())
            ? Enumerations[id]
            : GetInvalidEnumFailure(id);
    }

    public static bool Is(string possibleId)
    {
        return All.Select(e => e.Id).Contains(possibleId?.Trim(), StringComparer.OrdinalIgnoreCase);
    }

    public override string ToString() => Id;

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Id;
    }
        
    private static TEnumeration[] GetEnumerations()
    {
        var enumerationType = typeof(TEnumeration);

        return enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(info => info.FieldType == typeof(TEnumeration))
            .Select(info => (TEnumeration)info.GetValue(null))
            .ToArray();
    }

    private static InvalidEnumFailure GetInvalidEnumFailure(string value)
    {
        return new InvalidEnumFailure(value, typeof(TEnumeration).Name, All.Select(e => e.Id));
    }
}

[Serializable]
public abstract class EnumValueObject<TEnumeration, TId> : ValueObject
    where TEnumeration : EnumValueObject<TEnumeration, TId>
    where TId : struct, IComparable
{
    private int? _cachedHashCode;

    private static readonly Dictionary<TId, TEnumeration> EnumerationsById =
        GetEnumerations().ToDictionary(e => e.Id);

    private static readonly Dictionary<string, TEnumeration> EnumerationsByName =
        GetEnumerations().ToDictionary(e => e.Name, StringComparer.OrdinalIgnoreCase);

    protected EnumValueObject(TId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            if (name == null) throw new ArgumentNullException(nameof(name),"The name cannot be null.");
            throw new ArgumentException("The name cannot be empty.");
        }

        Id = id;
        Name = name.Trim();
    }

    public TId Id { get; protected set; }

    public string Name { get; protected set; }

    public static bool operator ==(EnumValueObject<TEnumeration, TId> a, TId b)
    {
        if (a is null) return false;

        return a.Id.Equals(b);
    }

    public static bool operator !=(EnumValueObject<TEnumeration, TId> a, TId b)
    {
        return !(a == b);
    }

    public static bool operator ==(TId a, EnumValueObject<TEnumeration, TId> b)
    {
        return b == a;
    }

    public static bool operator !=(TId a, EnumValueObject<TEnumeration, TId> b)
    {
        return !(b == a);
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (GetUnproxiedType(this) != GetUnproxiedType(obj)) return false;

        var enumValueObject = (EnumValueObject<TEnumeration, TId>) obj;

        return GetEqualityComponents().SequenceEqual(enumValueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        if (!_cachedHashCode.HasValue)
        {
            _cachedHashCode = GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        return _cachedHashCode.Value;
    }

    public static Result<TEnumeration> FromId(TId id)
    {
        return EnumerationsById.ContainsKey(id)
            ? EnumerationsById[id]
            : GetInvalidEnumFailureById(id.ToString());
    }

    public static Result<TEnumeration> FromName(string name)
    {
        if (name == null) return GetInvalidEnumFailureByName(String.Empty);
        if (String.IsNullOrWhiteSpace(name)) return GetInvalidEnumFailureByName(name);
        
        return EnumerationsByName.ContainsKey(name.Trim())
            ? EnumerationsByName[name]
            : GetInvalidEnumFailureByName(name);
    }


#if NET40
        public static ICollection<TEnumeration> All = EnumerationsById.Values.OfType<TEnumeration>().ToList();
#else
    public static IReadOnlyList<TEnumeration> All = EnumerationsById.Values.OfType<TEnumeration>().ToList();
#endif

    public static bool Is(string possibleName)
    {
        return All.Select(e => e.Name).Contains(possibleName?.Trim(), StringComparer.OrdinalIgnoreCase);
    }

    public static bool Is(TId possibleId)
    {
        return All.Select(e => e.Id).Contains(possibleId);
    }

    public override string ToString() => Name;

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Id;
    }

    private static TEnumeration[] GetEnumerations()
    {
        var enumerationType = typeof(TEnumeration);

        return enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(info => info.FieldType == typeof(TEnumeration))
            .Select(info => (TEnumeration) info.GetValue(null))
            .ToArray();
    }
    
    private static InvalidEnumFailure GetInvalidEnumFailureByName(string name)
    {
        return new InvalidEnumFailure(name, typeof(TEnumeration).Name, All.Select(e => e.Name));
    }
    
    private static InvalidEnumFailure GetInvalidEnumFailureById(string id)
    {
        return new InvalidEnumFailure(id, typeof(TEnumeration).Name, All.Select(e => e.Id.ToString()));
    }
}

public class InvalidEnumFailure : Failure
{
    public string Value { get; }

    public InvalidEnumFailure(string value, string enumName, IEnumerable<string> possibleValues)
        : base(
            $"invalid_{ToSnakeCase(enumName)}",
            $"Invalid value '{value}' for {SplitWords(enumName)}. Possible values: ({String.Join(',', possibleValues)}).")
    {
        Value = value;
    }
    
    private static string SplitWords(string enumName)
    {
        return Regex.Replace(enumName,
            "([a-z])([A-Z])",
            "$1 $2",
            RegexOptions.CultureInvariant);
    }
    
    private static string ToSnakeCase(string enumName)
    {
        return Regex.Replace(enumName,
            "([a-z])([A-Z])",
            "$1_$2",
            RegexOptions.CultureInvariant).ToLowerInvariant();
    }
}