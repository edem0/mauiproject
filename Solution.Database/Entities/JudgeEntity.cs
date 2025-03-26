using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Solution.Database.Entities;

[Table("Judge")]
[Index(nameof(EmailAddress), nameof(PhoneNumber), IsUnique = true)]
public class JudgeEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [StringLength(128)]
    public string? ImageId { get; set; }

    [StringLength(512)]
    public string? WebContentLink { get; set; }

    [Required]
    [StringLength(25)]
    public string Name { get; set; }

    [Required]
    [StringLength(50)]
    public string EmailAddress { get; set; }

    [Required]
    [StringLength(20)]
    public string PhoneNumber { get; set; }

    [ForeignKey("Competition")]
    public uint CompetitionId { get; set; }

    public virtual CompetitionEntity Competition { get; set; }
}