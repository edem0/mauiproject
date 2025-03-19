using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Database.Entities;

[Table("Location")]
[Index(nameof(PostalCode), IsUnique = true)]
public class LocationEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    [Range(1000, 9999)]
    public uint PostalCode { get; set; }

    [Required]
    public string LocationName { get; set; }


    public virtual IReadOnlyCollection<CompetitionEntity> Competitions { get; set; }
}
