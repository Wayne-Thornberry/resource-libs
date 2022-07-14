using System.Collections.Generic;
using Proline.ClassicOnline.MScreen.Menus.MenuItems;

namespace Proline.ClassicOnline.MScreen.Menus
{
    public struct Appearance
    {
        public string Hair;
    }

    public class AppearanceMenu : Menu
    {

        private MenuListItem _hairListItem;
        private MenuListItem _appearanceEyebrowsListItem;
        private MenuListItem _appearanceFacialHairListItem;
        private MenuListItem _appearanceSkinBlemishesListItem;
        private MenuListItem _appearanceSkinAgingListItem;
        private MenuListItem _appearanceSkinComplexionListItem;
        private MenuListItem _appearanceMolesFrecklesListItem;
        private MenuListItem _appearanceSkinDamageListItem;
        private MenuListItem _appearanceEyeColorListItem;
        private MenuListItem _appearanceEyeMakeupListItem;
        private MenuListItem _appearanceLipstickListItem;

        private List<string> _maleHairs;
        private List<string> _femaleHairs;


        public AppearanceMenu() : base("Character Creator", "APPEARANCE")
        {
            _maleHairs = new List<string>();// DataController.ApparelCollection.Hairstyles.Where(i=>i.Gender == 'm' || i.Gender == 'u').Select(i=>i.Name).ToList();
            _femaleHairs = new List<string>();// DataController.ApparelCollection.Hairstyles.Where(i=>i.Gender == 'f' || i.Gender == 'u').Select(i=>i.Name).ToList();
            _hairListItem = new MenuListItem("Hair", _maleHairs, 0);
            AddMenuItem(_hairListItem);
        }

        public Appearance GetAppearance()
        {
            return new Appearance
            {
                Hair = _hairListItem.GetCurrentSelection(),
            };
        }

        public void Refesh(char gender)
        {
            _hairListItem.ListItems = gender == 'm' ? _maleHairs : _femaleHairs;
        }
    }
}