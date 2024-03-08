using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tests.Shared.CustomXunitTraits;

/// <summary>
/// Apply this attribute to your xUnit test method to specify it is an Architecture test
/// </summary>
/// <remarks>https://www.brendanconnolly.net/organizing-tests-with-xunit-traits/</remarks>
[TraitDiscoverer(nameof(ArchitectureTestDiscoverer), nameof(Shared))] 
[AttributeUsage(AttributeTargets.Class)]
public class ArchitectureTestAttribute : Attribute, ITraitAttribute;

public class ArchitectureTestDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "ArchitectureTest");
    }
}
