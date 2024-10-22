using MessagePack;
using System.Collections.Generic;

[MessagePackObject(true)]
public class MyClass
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Items { get; set; }
}

