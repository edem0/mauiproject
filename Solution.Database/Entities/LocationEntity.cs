using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Database.Entities;

[Table("Location")]
public class LocationEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    public string AreaName { get; set; }

    [Required]
    public uint HouseNumber { get; set; }

    [ForeignKey("City")]
    public uint CityId { get; set; }

    public virtual CityEntity City { get; set; }


    public virtual IReadOnlyCollection<CompetitionEntity> Competitions { get; set; }
}
