using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

[MessagePackObject(true)]
public class TalkData
{
    public Content[] Content { get; set; }
}

[MessagePackObject(true)]
public class Content
{
    public string Speaker { get; set; }
    public string Text { get; set; }
    public ChangeImage[] ChangeImage { get; set; }
    public QuestionData[] QuestionData { get; set; }
    public string BGM { get; set; }
    public string SE { get; set; }
}

[MessagePackObject(true)]
public struct ChangeImage
{
    public string ImageName { get; set; }
    public string SpriteName { get; set; }
}

[MessagePackObject(true)]
public struct QuestionData
{
    public string Answer { get; set; }
    public string NextFlag { get; set; }
    public string NextTalkData { get; set; }
}