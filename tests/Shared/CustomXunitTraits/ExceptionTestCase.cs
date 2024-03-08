namespace Tests.Shared.CustomXunitTraits;

/// <summary>
/// Apply this attribute to your xUnit test method to specify it is an Exception test case
/// </summary>
/// <remarks>https://www.brendanconnolly.net/organizing-tests-with-xunit-traits/</remarks>
[TraitDiscoverer("Tests.Shared.CustomXunitTraits.ExceptionTestCaseDiscoverer", nameof(Shared))]
[AttributeUsage(AttributeTargets.Class)]
public class ExceptionTestCaseAttribute : Attribute, ITraitAttribute
{
    public ExceptionTestCaseAttribute()
    {
        
    }
}

public class ExceptionTestCaseDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "Exception Test Case");
    }
}