

namespace Proline.CFXExtended.Core.Scaleforms
{
    public class MPRankBar2 : ScaleformUI
    {

        public MPRankBar2() : base("MP_RANK_BAR")
        {
        }
        public void Ready()
        {
            CallFunction("READY");
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