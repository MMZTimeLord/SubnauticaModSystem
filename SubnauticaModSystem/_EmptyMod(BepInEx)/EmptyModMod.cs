using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace EmptyMod
{
    // -----------------------------  Configuration Section  ----------------------------- //
    //                                                                                     //
    //       This section declares the Menu Attributes & Structure for SMLHelper.          //
    //                                                                                     //
    // The MenuAttribute allows us to set the title of our options menu in the "Mods" tab. //
    [Menu("Empty Mod")]
    public class EMConfig : ConfigFile
    {
        // A Toggle is used to represent true or false
        //
        // Default is true
        [Toggle("Empty Mod Test Toggle")]
        public bool EMTestToggle = true;
    }
    // ---------------------------  End Configuration Section  --------------------------- //

    class EmptyMod_Mod
    {
        internal static EMConfig EMConfig { get; } = OptionsPanelHandler.RegisterModOptions<EMConfig>();
        // EMConfig.EMTestToggle will now be available for use from the config file

        // Harmony patch to this section/object in the game
        [HarmonyPatch(typeof(IngameMenu), nameof(IngameMenu.Open))]
        public static class EMIngameMenu_Patch
        {

            // static declarations go here
            // example -> static GameObject quitConfirmation;

            // Pre or Post depending upon what you need
            [HarmonyPostfix]
            public static void Postfix(IngameMenu __instance)
            {
                // Main code goes here. This will be injected
            }
        }
    }
}
