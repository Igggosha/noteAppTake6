namespace WebApplication2.Models;

// unused
public class ProgressNoteViewModel
{
    public string Name { get; set; }
    public List<float> NumberRow1 { get; set; }
    public List<float> NumberRow2 { get; set; }
    public List<int> TagIds { get; set; } = new List<int>();

    public static ProgressNoteViewModel fromEntity(BaseNoteEntity entity)
    {
        var vm = new ProgressNoteViewModel() {Name = entity.Name};

        var pairs = entity.Content.Split(' ');
        
        var numberRow1 = new List<float>();
        var numberRow2 = new List<float>();
        foreach (var pair in pairs)
        {
            var nums = pair.Split(',');
            float num1;
            float num2;
            if (nums.Length == 2 && float.TryParse(nums[0], out num1) && float.TryParse(nums[0], out num2))
            {
                numberRow1.Add(num1);
                numberRow2.Add(num2);
            }
            
        }
        
        vm.NumberRow1 = numberRow1;
        vm.NumberRow2 = numberRow2;
        
        return vm;
    }
}