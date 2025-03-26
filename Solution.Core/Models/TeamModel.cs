using Solution.Database.Entities;

namespace Solution.Core.Models;

public class TeamModel
{
    public uint Id { get; set; }

    public string Name { get; set; }

    public uint Points { get; set; }

    public virtual ICollection<MemberEntity> Members { get; set; }

    public TeamModel(uint id, ICollection<MemberEntity> members)
    {
        Id = id;
        Members = members;
    }

    public TeamModel(TeamEntity entity)
    {
        if(entity == null)
        {
            return;
        }

        Id = entity.Id;
        Members = entity.Members;
    }

    public override bool Equals(object? obj)
    {
        return obj is TeamModel model &&
            this.Id == model.Id &&
            this.Members == model.Members;
    }
}
