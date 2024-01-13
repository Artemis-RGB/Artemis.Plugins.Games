using Artemis.Plugins.Games.Ultrakill.GSI.Patches;
using BepInEx;
using HarmonyLib;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Artemis.Plugins.Games.Ultrakill.GSI
{
    [BepInPlugin("com.artemis.gsi", "Artemis GSI", "0.1")]
    public class ArtemisGsiPlugin : BaseUnityPlugin
    {
        public static ArtemisWebClient ArtemisWebClient => _artemisWebClient;
        private static ArtemisWebClient _artemisWebClient;

        private NewMovement player = null;
        private GunControl guns = null;

        public void Awake()
        {
            try
            {
                _artemisWebClient = new ArtemisWebClient();
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return;
            }
            //Harmony harmony = new Harmony("com.artemis.gsi");
            // harmony.PatchAll();

            Debug.Log("patched artemis");
            ArtemisWebClient.StartTimer();
        }

        public void Start()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnSceneChanged(Scene from, Scene to)
        {
            Debug.Log($"Scene changed to {to.name}");
            player = null;
            if (SceneManager.GetActiveScene().name.StartsWith("Level") || SceneManager.GetActiveScene().name == "uk_construct")
            {
                player = FindObjectOfType<NewMovement>();
                Debug.Log($"Found player! name:{player.gameObject.name}");
            }
            if (guns == null)
            {
                guns = MonoSingleton<GunControl>.Instance;
            }
        }

        public void Update()
        {
            if (player != null)
            {
                //Debug.Log($"Health: {player.hp}");
                //Debug.Log($"Speed: {player.rb.velocity.magnitude}");
                //Debug.Log($"Stamina: {player.boostCharge}");
                //Debug.Log($"Jumping: {player.jumping}");
                //Debug.Log($"Dead: {player.dead}");
                //Debug.Log($"Gun slot: {guns.currentSlot}");
                //Debug.Log($"variation: {guns.currentVariation}");

                ArtemisPlayer.Health = player.hp;
                ArtemisPlayer.Speed = player.rb.velocity.magnitude;
                ArtemisPlayer.Stamina = player.boostCharge;
                ArtemisPlayer.Jumping = player.jumping;
                ArtemisPlayer.Dead = player.dead;
                ArtemisPlayer.CurrentGun = guns.currentSlot;
                ArtemisPlayer.CurrentGunVariation = guns.currentVariation;
            }
        }

        public void OnApplicationQuit()
        {
            ArtemisWebClient?.StopTimer();
        }
    }
}
