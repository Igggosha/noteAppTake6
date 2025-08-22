using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

[Table("ProgressNotes")]
public class ProgressNoteEntityModel: BaseNoteEntity
{
    public static ProgressNoteEntityModel FromViewModel(NoteViewModel fromViewModel, string? authorId, List<TagEntity> tags)
    {
        var e = new ProgressNoteEntityModel()
        {
            Name = fromViewModel.Name,
            Content = fromViewModel.Content,
            Created = DateTime.Now,
            ProfileId = authorId,
            Tags = tags
        };
        return e;
    }
    
    

    public static ProgressNoteEntityModel FromProgressViewModel(ProgressNoteViewModel vm, string? authorId, List<TagEntity> tags)
    {
        var e = new ProgressNoteEntityModel()
        {
            Name = vm.Name, 
            Content = "",
            Created = DateTime.Now,
            ProfileId = authorId,
            Tags = tags
        };
        for (var i = 0; i < vm.NumberRow1.Count; i++)
        {
            e.Content += $"{vm.NumberRow1[i]},{vm.NumberRow2[i]} ";
        }
        return e;
    }
    public static ProgressNoteEntityModel FromNoteEntity(NoteEntityModel e)
    {
        return new ProgressNoteEntityModel()
        {
            Name = e.Name,
            Content = e.Content,
            Created = e.Created,
            Id = e.Id,
            Profile = e.Profile,
            ProfileId = e.ProfileId,
            Tags = e.Tags
        };
    }
}