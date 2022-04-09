using System.Collections.Generic; 

namespace Proline.ExampleClient.ComponentUI.Menus
{
    public class FeaturesMenu : Menu
    {
        private MenuListItem _featuresBrowItemList;
        private MenuListItem _featuresEyesItemList;
        private MenuListItem _featuresNoseItemList;
        private MenuListItem _featuresNoseProfileItemList;
        private MenuListItem _featuresNoseTipItemList;
        private MenuListItem _featuresCheekbonesItemList;
        private MenuListItem _featuresCheeksItemList;
        private MenuListItem _featuresLipsItemList;
        private MenuListItem _featuresJawItemList;
        private MenuListItem _featuresChinProfileItemList;
        private MenuListItem _featuresChinShapeItemList;

        public FeaturesMenu() : base("Character Creator", "FEATURES")
        {
        }
    }
}