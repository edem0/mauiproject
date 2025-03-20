namespace Solution.Database.Entities;


[Table("City")]
public class CityEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    [Range(1000, 9999)]
    public uint PostalCode { get; set; }

    [Required]
    public string CityName { get; set; }

    public virtual IReadOnlyCollection<LocationEntity> Locations { get; set; }

}
