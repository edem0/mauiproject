using Microsoft.EntityFrameworkCore.Storage.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Database.Entities;

[Table("Member")]
public class MemberEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [StringLength(128)]
    public string? ImageId { get; set; }

    [StringLength(512)]
    public string? WebContentLink { get; set; }

    [Required]
    [StringLength(20)]
    public string Name { get; set; }

    [ForeignKey("Team")]
    public uint TeamId { get; set; }

    public virtual TeamEntity Team { get; set; }
}
