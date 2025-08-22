using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Db;

public class NoteDbContext: IdentityDbContext
{
    public DbSet<NoteEntityModel> Notes { get; set; }
    
    public DbSet<ProgressNoteEntityModel> ProgressNotes { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<ProfileEntity> Profiles { get; set; }
    
    public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options)
    {
        
    }
}