namespace WebApplication2.Models;

public class NoteViewModel
{
    public string Name { get; set; } = "";
    public string Content { get; set; } = "";
    public List<int> TagIds { get; set; } = new List<int>();

    public static NoteViewModel FromEntity(NoteEntityModel noteEntityModel)
    {
        var noteViewModel = new NoteViewModel
        {
            Name = noteEntityModel.Name,
            Content = noteEntityModel.Content,
            TagIds = noteEntityModel.Tags.Select(t => t.Id).ToList()
        };
        return noteViewModel;
    }
}