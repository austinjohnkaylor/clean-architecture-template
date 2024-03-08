namespace Tests.Shared.CustomXunitTraits;

/// <summary>
/// Apply this attribute to an xUnit test method to specify it is a Happy-Path test
/// </summary>
/// <remarks>https://www.brendanconnolly.net/organizing-tests-with-xunit-traits/</remarks>
[TraitDiscoverer("Tests.Shared.CustomXunitTraits.HappyPathTestDiscoverer", nameof(Shared))]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class HappyPathTestAttribute : Attribute, ITraitAttribute
{
    public HappyPathTestAttribute()
    {
        
    }
}

public class HappyPathTestDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "Happy Path Test");
    }
}
