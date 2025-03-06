namespace ClassLibrary1;

public class SampleClass
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string FullName { get; set; }

    public SampleClass(string fullName)
    {
        FullName = fullName;
        SplitFullName();
    }

    private void SplitFullName()
    {
        var names = FullName.Split(' ');
        FirstName = names[0];
        LastName = names[1];
    }
}