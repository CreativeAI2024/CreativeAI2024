using MessagePack;

namespace YourNamespace // ここを適切な名前に置き換えてください
{
    [MessagePackObject]
    public class MapData
    {
        [Key(0)]
        public string[] Tiles { get; set; } 
        [Key(1)]
        public string[] StylesFront { get; set; }
        [Key(2)]
        public string[] StylesMiddle { get; set; }
        [Key(3)]
        public string[] StylesBack { get; set; }
    }
}
