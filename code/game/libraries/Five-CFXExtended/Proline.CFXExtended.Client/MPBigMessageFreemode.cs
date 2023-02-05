namespace Proline.ClassicOnline.Scaleforms
{
    public class MPBigMessageFreemode : ScaleformUI
    {
        public MPBigMessageFreemode() : base("MP_BIG_MESSAGE_FREEMODE")
        {
        }
        public void SETSHARDBACKGROUNDTARGETHEIGHT(int numStats)
        {
            CallFunction("SET_SHARD_BACKGROUND_TARGET_HEIGHT", numStats);
        }
        public void SETSHARDBACKGROUNDHEIGHT(float height)
        {
            CallFunction("SET_SHARD_BACKGROUND_HEIGHT", height);
        }
        public void ROLLDOWNBACKGROUND()
        {
            CallFunction("ROLL_DOWN_BACKGROUND");
        }
        public void ROLLUPBACKGROUND()
        {
            CallFunction("ROLL_UP_BACKGROUND");
        }
        public void UPDATEMESSAGE(string msgText)
        {
            CallFunction("UPDATE_MESSAGE", msgText);
        }
        public void SETRESPAWNBARPERCENTAGE(float precent)
        {
            CallFunction("SET_RESPAWN_BAR_PERCENTAGE", precent);
        }
        public void FLASHRESPAWNBAR(int duration)
        {
            CallFunction("FLASH_RESPAWN_BAR", duration);
        }
        public void UPDATESTRAPMESSAGE(string msgText)
        {
            CallFunction("UPDATE_STRAP_MESSAGE", msgText);
        }
        public void ADDTXDREFRESPONSE(int textureDict, int uniqueID, int success)
        {
            CallFunction("ADD_TXD_REF_RESPONSE", textureDict, uniqueID, success);
        }
        public void CLEARCREWRANKUPMPMESSAGE()
        {
            CallFunction("CLEAR_CREW_RANKUP_MP_MESSAGE");
        }
        public void SHOWBUSTEDMPMESSAGE()
        {
            CallFunction("SHOW_BUSTED_MP_MESSAGE");
        }
        public void SHOWWASTEDMPMESSAGE()
        {
            CallFunction("SHOW_WASTED_MP_MESSAGE");
        }
        public void SHOWRANKUPMPMESSAGE(string bigText)
        {
            CallFunction("SHOW_RANKUP_MP_MESSAGE", bigText);
        }
        public void SHOWCREWRANKUPMPMESSAGE(string titleStr, string msgStr, int rankNumber, int emblemTXD, int emblemTXN, int alpha)
        {
            CallFunction("SHOW_CREW_RANKUP_MP_MESSAGE", titleStr, msgStr, rankNumber, emblemTXD, emblemTXN, alpha);
        }
        public void SHOWLOCKEDUPMPMESSAGE()
        {
            CallFunction("SHOW_LOCKED_UP_MP_MESSAGE");
        }
        public void SHOWMISSIONENDMPMESSAGE()
        {
            CallFunction("SHOW_MISSION_END_MP_MESSAGE");
        }
        public void SHOWMISSIONFAILEDMPMESSAGE()
        {
            CallFunction("SHOW_MISSION_FAILED_MP_MESSAGE");
        }
        public void SHOWMISSIONPASSEDMESSAGE()
        {
            CallFunction("SHOW_MISSION_PASSED_MESSAGE");
        }
        public void SHOWWEAPONPURCHASED(string bigMessage, string weaponName, int weaponHash, string weaponDescription, int alpha)
        {
            CallFunction("SHOW_WEAPON_PURCHASED", bigMessage, weaponName, weaponHash, weaponDescription, alpha);
        }
        public void SHOWPLANEMESSAGE(string bigMessage, string planeName, int planeHash)
        {
            CallFunction("SHOW_PLANE_MESSAGE", bigMessage, planeName, planeHash);
        }
        public void SHOWTERRITORYCHANGEMPMESSAGE()
        {
            CallFunction("SHOW_TERRITORY_CHANGE_MP_MESSAGE");
        }
        public void SHOWMPMESSAGETOP()
        {
            CallFunction("SHOW_MP_MESSAGE_TOP");
        }
        public void SHOWCENTEREDMPMESSAGELARGE()
        {
            CallFunction("SHOW_CENTERED_MP_MESSAGE_LARGE");
        }
        public void SHOWCENTEREDMPMESSAGE()
        {
            CallFunction("SHOW_CENTERED_MP_MESSAGE");
        }
        public void SHOWCENTEREDTOPMPMESSAGE()
        {
            CallFunction("SHOW_CENTERED_TOP_MP_MESSAGE");
        }
        public void SHOWBIGMPMESSAGEWITHSTRAPANDRANK()
        {
            CallFunction("SHOW_BIG_MP_MESSAGE_WITH_STRAP_AND_RANK");
        }
        public void ShowBigMPMessageWithStrap()
        {
            CallFunction("SHOW_BIG_MP_MESSAGE_WITH_STRAP");
        }
        public void ShowBigMPMessage()
        {
            CallFunction("SHOW_BIG_MP_MESSAGE");
        }
        public void ShowShardCenteredMPMessage(string bigTxt, string msgTxt, int colId, int i)
        {
            CallFunction("SHOW_SHARD_CENTERED_MP_MESSAGE");
        }
        public void ShowShardCenteredMPMessageLarge()
        {
            CallFunction("SHOW_SHARD_CENTERED_MP_MESSAGE_LARGE");
        }
        public void ShowShardWastedMPMessage(string bigTxt, string msgTxt, int colId, bool someUnusedBoolean, bool darkenBackground)
        {
            CallFunction("SHOW_SHARD_WASTED_MP_MESSAGE", bigTxt, msgTxt, colId, someUnusedBoolean, darkenBackground);
        }
        public void ShowShardCenteredTopMPMessage(string bigTxt, string msgTxt, int colId, bool someUnusedBoolean, bool darkenBackground)
        {
            CallFunction("SHOW_SHARD_CENTERED_TOP_MP_MESSAGE");
        }
        public void ShowShardRankUpMPMessage()
        {
            CallFunction("SHOW_SHARD_RANKUP_MP_MESSAGE");
        }
        public void ShowShardCrewRankUpMessage()
        {
            CallFunction("SHOW_SHARD_CREW_RANKUP_MP_MESSAGE");
        }
        public void DoShard(object[] args, bool isCenter, int colID, int shardColID, int useLargeShard)
        {
            CallFunction("DO_SHARD", args, isCenter, colID, shardColID, useLargeShard);
        }
        public void ShardSetText(string bigText, string msgText, int colID)
        {
            CallFunction("SHARD_SET_TEXT", bigText, msgText, colID);
        }
        public void ShardAnimDelay(int delayTime)
        {
            CallFunction("SHARD_ANIM_DELAY", delayTime);
        }
        public void ShardAnimOut(int colID, int animTime, int textColourId)
        {
            CallFunction("SHARD_ANIM_OUT", colID, animTime, textColourId);
        }
        public void SetRankIconRGB(int r, int g, int b)
        {
            CallFunction("SET_RANK_ICON_RGB", r, g, b);
        }
        public void TransitionOut(int duration)
        {
            CallFunction("TRANSITION_OUT", duration);
        }
        public void ResetMovie()
        {
            CallFunction("RESET_MOVIE");
        }
        public void OverrideYPosition(float newYPosition)
        {
            CallFunction("OVERRIDE_Y_POSITION", newYPosition);
        }
        public void TransitionIn(int duration)
        {
            CallFunction("TRANSITION_IN", duration);
        }
        public void TransitionUp(int duration, int preventAutoExpansion)
        {
            CallFunction("TRANSITION_UP", duration, preventAutoExpansion);
        }
        public void TransitionDown(int duration)
        {
            CallFunction("TRANSITION_DOWN", duration);
        }
    }
}
