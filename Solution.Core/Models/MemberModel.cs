using Solution.Database.Entities;

namespace Solution.Core.Models;

public class MemberModel : IObjectValidator<uint>
{
    public uint Id { get; set; }

    public string ImageId { get; set; }

    public string WebContentLink { get; set; }

    public string Name { get; set; }

    public uint TeamId { get; set; }

    public MemberModel() { }

    public MemberModel(uint id, string name, uint teamId)
    {
        Id = id;
        Name = name;
        TeamId = teamId;
    }

    public MemberModel(MemberEntity entity)
    {
        if(entity == null)
        {
            return;
        }

        Id = entity.Id;
        Name = entity.Name;
        TeamId = entity.TeamId;
    }

    public override bool Equals(object? obj)
    {
        return obj is MemberModel model &&
            this.Id == model.Id &&
            this.Name == model.Name &&
            this.TeamId == model.TeamId;
    }
}
