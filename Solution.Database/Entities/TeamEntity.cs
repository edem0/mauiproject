using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Database.Entities;

[Table("Team")]
public class TeamEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    [StringLength(25)]
    public string Name { get; set; }

    [Required]
    public uint Points {  get; set; }

    [Range(1, 10)]
    public virtual ICollection<MemberEntity> Members { get; set; }
}
