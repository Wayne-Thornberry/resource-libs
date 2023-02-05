namespace Proline.ClassicOnline.Scaleforms
{
    public class MPRankBar : ScaleformHud
    {

        public MPRankBar() : base(19)
        {

        }

        public void Show()
        {
            CallFunction("SHOW");
        }

        public void SetColor(int color)
        {
            CallFunction("SET_COLOUR", color);
        }

        public void OverrideOnScreenDuration(int duration)
        {
            CallFunction("OVERRIDE_ONSCREEN_DURATION", duration);
        }

        public void SetRankScores(int prevLevelXp, int nextLevelXp, int oldXp, int curXp, int playerLevel)
        {
            CallFunction("SET_RANK_SCORES", prevLevelXp, nextLevelXp, oldXp, curXp, playerLevel);
        }

        public void StayOnScreen()
        {
            CallFunction("STAY_ON_SCREEN");
        }
    }
}