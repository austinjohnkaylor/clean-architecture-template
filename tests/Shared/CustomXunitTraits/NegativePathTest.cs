namespace Tests.Shared.CustomXunitTraits;

/// <summary>
/// Apply this attribute to an xUnit test method to specify it is a Negative Path (a.k.a Happy Path) test
/// </summary>
/// <remarks>https://www.brendanconnolly.net/organizing-tests-with-xunit-traits/</remarks>
[TraitDiscoverer("Tests.Shared.CustomXunitTraits.NegativePathTestDiscoverer", nameof(Shared))]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class NegativePathTestAttribute : Attribute, ITraitAttribute
{
    public NegativePathTestAttribute()
    {
        
    }
}

public class NegativePathTestDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "Negative Path Test");
    }
}
