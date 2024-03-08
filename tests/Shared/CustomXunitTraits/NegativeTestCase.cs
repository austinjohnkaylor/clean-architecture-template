namespace Tests.Shared.CustomXunitTraits;

/// <summary>
/// Apply this attribute to an xUnit test method to specify it is a Negative Path (a.k.a Happy Path) test
/// </summary>
/// <remarks>https://www.brendanconnolly.net/organizing-tests-with-xunit-traits/</remarks>
[TraitDiscoverer("Tests.Shared.CustomXunitTraits.NegativeTestCaseDiscoverer", nameof(Shared))]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class NegativeTestCaseAttribute : Attribute, ITraitAttribute
{
    public NegativeTestCaseAttribute()
    {
        
    }
}

public class NegativeTestCaseDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "Negative Test Case");
    }
}
