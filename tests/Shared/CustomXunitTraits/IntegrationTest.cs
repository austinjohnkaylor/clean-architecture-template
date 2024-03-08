namespace Tests.Shared.CustomXunitTraits;

/// <summary>
/// Apply this attribute to your xUnit test class to specify it is an Integration test
/// </summary>
/// <remarks>https://www.brendanconnolly.net/organizing-tests-with-xunit-traits/</remarks>
[TraitDiscoverer(nameof(IntegrationTestDiscoverer), nameof(Shared))] 
[AttributeUsage(AttributeTargets.Class)]
public class IntegrationTestAttribute : Attribute, ITraitAttribute;

public class IntegrationTestDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "IntegrationTest");
    }
}
