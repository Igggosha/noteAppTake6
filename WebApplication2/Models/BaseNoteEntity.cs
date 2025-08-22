using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public abstract class BaseNoteEntity
{
    [Key]
    [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public int Id { get; set; } = 0;
    
    [Column("Name", TypeName = "nvarchar(50)")]
    public string Name { get; set; } = "";
    
    [Column("Content", TypeName = "nvarchar(500)")]
    public string Content { get; set; } = "";

    [Column("Created")] public DateTime Created { get; set; } = DateTime.Now;
    
    [Column("Tags")]
    public List<TagEntity> Tags { get; set; } = new();
    
    
    [ForeignKey(nameof(Profile))]
    [Column("ProfileId", TypeName = "nvarchar(450)")]
    public string? ProfileId { get; set; }
    
    public ProfileEntity? Profile { get; set; }
}