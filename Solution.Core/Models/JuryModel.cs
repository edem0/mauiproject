using Solution.Database.Entities;

namespace Solution.Core.Models;

public class JuryModel
{
    public uint Id { get; set; }

    public string Name { get; set; }

    public string EmailAddress { get; set; }

    public string PhoneNumber { get; set; }

    public uint CompetitionId { get; set; }
}
