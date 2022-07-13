using System.Collections.Generic;
using Proline.ClassicOnline.MScreen.Menus.MenuItems;

namespace Proline.ClassicOnline.MScreen.Menus
{
    public struct Heritage
    {
        public string Mother;
        public string Father;
        public int Resemblance;
        public int Skin;
    }

    public class HeritageMenu : Menu
    {

        private MenuListItem _motherListItem;
        private MenuListItem _fatherListItem;
        private MenuSliderItem _resemblanceSliderItem;
        private MenuSliderItem _skinSliderItem;

        private List<string> _fathers;
        private List<string> _mothers;

        public HeritageMenu() : base("Character Creator", "HERITAGE")
        {
            _fathers = new List<string>
            {
                "Adrian",
                "Alex",
                "Andrew",
                "Angel",
                "Anthony",
                "Benjamin",
                "Daniel",
                "Diego",
                "Ethan",
                "Evan",
                "Gabriel",
                "Isaac",
                "Joshua",
                "Juan",
                "Kevin",
                "Louis",
                "Michael",
                "Noah",
                "Samuel",
                "Santiago",
                "Vincent",
                "Claude",
                "Niko",
                "John",
            };
            _mothers = new List<string>
            {
                "Amelia",
                "Ashley",
                "Audrey",
                "Ava",
                "Briana",
                "Camila",
                "Charlotte",
                "Elizabeth",
                "Emma",
                "Evelyn",
                "Giselle",
                "Grace",
                "Hannah",
                "Isabella",
                "Jasmine",
                "Natalie",
                "Nicole",
                "Olivia",
                "Sophie",
                "Violet",
                "Zoe",
                "Misty",
            };

            _motherListItem = new MenuListItem("Mother", _mothers, 0);
            _fatherListItem = new MenuListItem("Father", _fathers, 0);
            _resemblanceSliderItem = new MenuSliderItem("Resemblance", 0, 10, 5, true)
            {
                SliderRightIcon = MenuItem.Icon.FEMALE,
                SliderLeftIcon = MenuItem.Icon.MALE
            };
            _skinSliderItem = new MenuSliderItem("Skin Tone", 0, 10, 5, true)
            {
                SliderRightIcon = MenuItem.Icon.FEMALE,
                SliderLeftIcon = MenuItem.Icon.MALE
            };

            AddMenuItem(_motherListItem);
            AddMenuItem(_fatherListItem);
            AddMenuItem(_resemblanceSliderItem);
            AddMenuItem(_skinSliderItem);
        }

        public Heritage GetHeritage()
        {
            return new Heritage
            {
                Mother = _motherListItem.GetCurrentSelection(),
                Father = _fatherListItem.GetCurrentSelection(),
                Resemblance = _resemblanceSliderItem.Position,
                Skin = _skinSliderItem.Position,
            };
        }
    }
}