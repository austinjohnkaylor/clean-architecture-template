namespace Tests.Shared.CustomXunitTraits;

/// <summary>
/// Apply this attribute to your xUnit test method to specify it is a Mutation test case
/// </summary>
/// <remarks>https://www.brendanconnolly.net/organizing-tests-with-xunit-traits/</remarks>
[TraitDiscoverer("Tests.Shared.CustomXunitTraits.MutationTestCaseDiscoverer", nameof(Shared))]
[AttributeUsage(AttributeTargets.Class)]
public class MutationTestCaseAttribute : Attribute, ITraitAttribute
{
    public MutationTestCaseAttribute()
    {
        
    }
}

public class MutationTestCaseDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "Mutation Test Case");
    }
}