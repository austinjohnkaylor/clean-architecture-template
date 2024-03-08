namespace Tests.Shared.CustomXunitTraits;

/// <summary>
/// Apply this attribute to your xUnit test class to specify it is a Unit test
/// </summary>
/// <remarks>https://www.brendanconnolly.net/organizing-tests-with-xunit-traits/</remarks>
[TraitDiscoverer("Tests.Shared.CustomXunitTraits.UnitTestDiscoverer", nameof(Shared))]
[AttributeUsage(AttributeTargets.Class)]
public class UnitTestAttribute : Attribute, ITraitAttribute
{
    public UnitTestAttribute()
    {
        
    }
}

public class UnitTestDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "Unit Test");
    }
}
