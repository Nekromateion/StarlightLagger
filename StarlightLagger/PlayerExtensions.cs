using VRC;
using VRC.SDKBase;
using System.Runtime.CompilerServices;

namespace StarlightLagger
{
    internal static class PlayerExtensions
    {
        //these extensions were made by Love, Day, and probably a few other coders credits to them
        internal static Player LocalPlayer => Player.prop_Player_0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static VRCPlayerApi GetVRCPlayerApi(this Player player)
        {
            return player.field_Private_VRCPlayerApi_0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int GetActorNumber(this Player player)
        {
            return player.GetVRCPlayerApi().playerId;
        }
    }
}