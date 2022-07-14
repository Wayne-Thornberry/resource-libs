namespace Proline.ClassicOnline.MGame.Data
{

    public struct WeaponComponent
    {
        public string Name { get; set; }
        public int Ammo { get; set; }
        public int Hash { get; set; }
    }

    public struct PedDecoration
    {
        public string Hash { get; set; }
        public string CollectionHash { get; set; }
    }

    public class PedOutfit
    {
        public string OutfitName { get; set; } = "";
        public OutfitComponent[] Components { get; set; } = new OutfitComponent[12];
    }

    public struct PedOverlay
    {
        public int Index { get; set; }
        public float Opacity { get; set; }
        public int ColorType { get; set; }
        public int PrimaryColor { get; set; }
        public int SecondaryColor { get; set; }
    }

    public struct PedFeature
    {
        public float Value { get; set; }
    }

    public struct PedHair
    {
        public int Color { get; set; }
        public int HighlightColor { get; set; }
    }

    public class PedLooks
    {
        public int Mother { get; set; }
        public int Father { get; set; }
        public float Resemblence { get; set; } = 0.5f;
        public float SkinTone { get; set; } = 0.5f;
        public int EyeColor { get; set; }

        public PedHair Hair { get; set; } = new PedHair();
        public PedFeature[] Features { get; } = new PedFeature[20];
        public PedOverlay[] Overlays { get; } = new PedOverlay[13];
        public PedDecoration[] Decorations { get; set; } = new PedDecoration[0];
    }

    public class PedLoadout
    {
        public WeaponComponent[] Weapons { get; set; }
    }

    public struct CharacterStats
    {
        public int WalletBalance { get; set; }
        public int BankBalance { get; set; }
        public int XpLevel { get; set; }
        public int XpExperience { get; set; }
        public int Stamina { get; set; }
        public int Strength { get; set; }
        public int SpecialAbility { get; set; }
        public int LungCapacity { get; set; }
        public int WheelieAbility { get; set; }
        public int FlyingAbility { get; set; }
        public int ShootingAbility { get; set; }
        public int StealthAbility { get; set; }
    }

    public struct CharacterBrief
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }

    public class Character
    {
        public int Id { get; set; }

        public CharacterBrief Brief { get; set; }
        public CharacterStats Stats { get; set; }
        public CharacterPed Ped { get; set; } = new CharacterPed();
    }

    public class CharacterPed
    {
        public string Name { get; set; } = "NEW CHARACTER";
        public char Gender { get; set; } = 'm';
        public string SpawnLocation { get; set; } = "LAST_LOCATION";
        public float[] LastPosition { get; set; } = { 0f, 0f, 70f };
        public bool IsDead { get; set; }
        public bool IsArrested { get; set; }
        public PedLooks Looks { get; set; } = new PedLooks();
        public PedOutfit Outfit { get; set; } = new PedOutfit();
        public PedLoadout Loadout { get; set; } = new PedLoadout();
    }
}