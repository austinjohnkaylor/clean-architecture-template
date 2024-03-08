using Bogus;

namespace Tests.Shared;

/// <summary>
/// Generates mock entities for testing purposes using the <see cref="Bogus"/> library
/// </summary>
public class MockEntityGenerator
{
    private static uint _id = 1;
    public static List<MockEntity>? MockEntities { get; set; }
    private static Faker? _f;
    
    /// <summary>
    /// Generates a specified number of <see cref="MockEntity"/> using a <see cref="Faker"/> instance
    /// </summary>
    /// <param name="numberOfEntitiesToGenerate">The number of <see cref="MockEntity"/> to generate</param>
    public static void Generate(int numberOfEntitiesToGenerate)
    {
        _f = new Faker();
        MockEntities = new List<MockEntity>();

        for (var i = 0; i < numberOfEntitiesToGenerate; i++)
        {
            MockEntity entity = new()
            {
                Id = _id++,
                FirstName = _f.Person.FirstName,
                MiddleInitial = _f.Person.FirstName[0],
                LastName = _f.Person.LastName,
                Age = _f.Random.Number(1, 100).ToString()
            };
            MockEntities.Add(entity);
        }
    }
}