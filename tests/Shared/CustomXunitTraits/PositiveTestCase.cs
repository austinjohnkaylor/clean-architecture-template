namespace Tests.Shared.CustomXunitTraits;

/// <summary>
/// Apply this attribute to an xUnit test method to specify it is a Positive Path (a.k.a Happy Path) test
/// </summary>
/// <remarks>https://www.brendanconnolly.net/organizing-tests-with-xunit-traits/</remarks>
[TraitDiscoverer("Tests.Shared.CustomXunitTraits.PositiveTestCaseDiscoverer", nameof(Shared))]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class PositiveTestCaseAttribute : Attribute, ITraitAttribute
{
    public PositiveTestCaseAttribute()
    {
        
    }
}

public class PositiveTestCaseDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "Positive Test Case");
    }
}
