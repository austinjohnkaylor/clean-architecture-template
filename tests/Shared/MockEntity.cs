using Source.Domain;

namespace Tests.Shared;

/// <summary>
/// A mock entity for used for testing purposes.
/// </summary>
public class MockEntity : EntityBase
{
    public string FirstName { get; set; }
    public char MiddleInitial { get; set; }
    public string LastName { get; set; }
    public string Age { get; set; }
}