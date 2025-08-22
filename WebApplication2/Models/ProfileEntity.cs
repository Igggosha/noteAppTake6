using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Models;

[Table("Profiles")]
public class ProfileEntity
{
    public IdentityUser User;

    [Key]
    [ForeignKey(nameof(User))]
    [Column("user_id", TypeName = "nvarchar(450)")]
    public string UserId { get; set; }
    
    
    List<NoteEntityModel> Notes = new List<NoteEntityModel>();
    List<ProgressNoteEntityModel> ProgressNotes = new List<ProgressNoteEntityModel>();

    [Column("last_online")] public DateTime LastOnline { get; set; }

    [Column("created_at")] public DateTime CreatedAt { get; set; }
}