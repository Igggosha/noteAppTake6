using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

[Table("Tags")]
public class TagEntity
{
    [Key]
    [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public int Id { get; set; }
    
    [Column("Name", TypeName = "nvarchar(50)")]
    public string Name { get; set; } = "";
    
    public List<NoteEntityModel> Notes { get; set; } = new();
    public List<ProgressNoteEntityModel> ProgressNotes { get; set; } = new();
}