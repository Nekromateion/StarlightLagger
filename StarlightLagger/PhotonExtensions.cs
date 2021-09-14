using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Runtime.CompilerServices;

namespace StarlightLagger
{
    internal static class PhotonExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] internal static void OpRaiseEvent(byte code, object customObject, RaiseEventOptions raiseEventOptions, SendOptions sendOptions)
        {
            Il2CppSystem.Object customObject2 = Serialization.FromManagedToIL2CPP<Il2CppSystem.Object>(customObject);
            PhotonNetwork.field_Public_Static_LoadBalancingClient_0.Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0(code, customObject2, raiseEventOptions, sendOptions);
        }
    }
}