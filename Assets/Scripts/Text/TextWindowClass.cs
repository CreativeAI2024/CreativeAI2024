using System.Collections;
using System.Collections.Generic;
using MessagePack;

[MessagePackObject(true)]
public class TextWindowClass
{
    public List<Content> Content { get; set; }

}
[MessagePackObject(true)]
public class Content
{
    public string Text { get; set; }
    public string Speaker { get; set; }
    public ChangeImage[] ChangeImage { get; set; }
    public QuestionData[] QuestionData { get; set; }
    public string QuestionFlagName { get; set; }
    public string BGM { get; set; }
    public string SE { get; set; }
}
[MessagePackObject(true)]
public class ChangeImage
{
    public string Image { get; set; }
    public string Sprite { get; set; }
}
[MessagePackObject(true)]
public class QuestionData
{
    public string Answer { get; set; }
    public string NextFlag { get; set; }
    public string NextTextFile {  get; set; }
}
