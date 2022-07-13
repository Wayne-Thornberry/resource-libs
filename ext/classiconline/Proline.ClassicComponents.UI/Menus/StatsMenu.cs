using Proline.ClassicOnline.MScreen.Menus.MenuItems;

namespace Proline.ClassicOnline.MScreen.Menus
{
    public class StatsMenu : Menu
    {
        private MenuItem _staminaItem;
        private MenuItem _strengthItem;
        private MenuItem _flyingItem;
        private MenuItem _shootingItem;
        private MenuItem _stealthItem;

        public StatsMenu() : base("CHARACTER CREATOR", "STATS")
        {


            _staminaItem = new MenuItem("Stamina");
            _strengthItem = new MenuItem("Strength");
            _flyingItem = new MenuItem("Flying");
            _shootingItem = new MenuItem("Shooting");
            _stealthItem = new MenuItem("Stealth");

            AddMenuItem(_staminaItem);
            AddMenuItem(_strengthItem);
            AddMenuItem(_flyingItem);
            AddMenuItem(_shootingItem);
            AddMenuItem(_stealthItem);
        }
    }
}