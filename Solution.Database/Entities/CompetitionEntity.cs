using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Database.Entities;

[Table("Competition")]
public class CompetitionEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [ForeignKey("Location")]
    public uint LocationId { get; set; }

    public virtual LocationEntity Location { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public ICollection<TeamEntity> Teams { get; set; }

    public ICollection<JudgeEntity> Jury { get; set; }
}
