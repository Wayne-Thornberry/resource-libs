namespace Proline.ClassicOnline.GCharacter.Data
{
    public class CharacterLooks
    {
        public int Mother { get; set; }
        public int Father { get; set; }
        public float Resemblence { get; set; } 
        public float SkinTone { get; set; } 
        public int EyeColor { get; set; }

        public PedHair Hair { get; set; } 
        public PedFeature[] Features { get; set; } 
        public PedOverlay[] Overlays { get; set;  }
        public PedDecoration[] Decorations { get; set; }
    }
}
