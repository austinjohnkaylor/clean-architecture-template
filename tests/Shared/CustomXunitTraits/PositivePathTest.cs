namespace Tests.Shared.CustomXunitTraits;

/// <summary>
/// Apply this attribute to an xUnit test method to specify it is a Positive Path (a.k.a Happy Path) test
/// </summary>
/// <remarks>https://www.brendanconnolly.net/organizing-tests-with-xunit-traits/</remarks>
[TraitDiscoverer("Tests.Shared.CustomXunitTraits.PositivePathTestDiscoverer", nameof(Shared))]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class PositivePathTestAttribute : Attribute, ITraitAttribute
{
    public PositivePathTestAttribute()
    {
        
    }
}

public class PositivePathTestDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "Positive Path Test");
    }
}
