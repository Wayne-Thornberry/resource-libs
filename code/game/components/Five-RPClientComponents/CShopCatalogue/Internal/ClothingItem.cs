using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShopCatalogue.Internal
{
    public class ForcedComponents
    {
        public List<Item> Item { get; set; }
    }

    public class NewItem
    {
        public string tagNameHash { get; set; }
    }

    public class Item
    {
        public string nameHash { get; set; }
        public int enumValue { get; set; }
        public string eCompType { get; set; }
    }

    public class RestrictionTags
    {
        public List<NewItem> Item { get; set; }
    }

    public class ClothingItem
    {
        public string lockHash { get; set; }
        public long cost { get; set; }
        public string textLabel { get; set; }
        public string uniqueNameHash { get; set; }
        public string eShopEnum { get; set; }
        public int locate { get; set; }
        public string scriptSaveData { get; set; }
        public RestrictionTags restrictionTags { get; set; }
        public ForcedComponents forcedComponents { get; set; }
        public string variantComponents { get; set; }
        public string variantProps { get; set; }
        public int drawableIndex { get; set; }
        public int localDrawableIndex { get; set; }
        public string eCompType { get; set; }
        public int textureIndex { get; set; }
        public bool isInOutfit { get; set; }
    }
}
