namespace Tests.Shared.CustomXunitTraits;

/// <summary>
/// Apply this attribute to your xUnit test class to specify it is an Architecture test
/// </summary>
/// <remarks>https://www.brendanconnolly.net/organizing-tests-with-xunit-traits/</remarks>
[TraitDiscoverer("Tests.Shared.CustomXunitTraits.ArchitectureTestDiscoverer", nameof(Shared))]
[AttributeUsage(AttributeTargets.Class)]
public class ArchitectureTestAttribute : Attribute, ITraitAttribute
{
    public ArchitectureTestAttribute()
    {
        
    }
}

public class ArchitectureTestDiscoverer : ITraitDiscoverer
{
    public virtual IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "ArchitectureTest");
    }
}
