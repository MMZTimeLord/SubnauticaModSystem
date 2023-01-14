using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SMLHelper.V2.Handlers;

namespace QuitToDesktop
{
    [BepInDependency("com.ahk1221.smlhelper")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class QuitToDesktopPlugin : BaseUnityPlugin
    {
        // Mod Information: Adds a "Quit to Desktop" button and renames the "Quit" to "Quit to Main Menu" to the in game menu.
        // Includes Option->Mods to turn on/off the Save warning for "Quit to Desktop"
        // Original Base Code by RandyKnapp and metious with QMM
        private const string myGUID = "com.mmztimelord.quittodesktop";
        private const string pluginName = "Quit To Desktop BepInEx Mod";
        private const string versionString = "2.0.0";

        private static readonly Harmony harmony = new Harmony(myGUID);

        public static ManualLogSource logger;

        private void Awake()
        {
            // This will make sure our configuration is saved to a file for use later.
            IngameMenuHandler.RegisterOnSaveEvent(new System.Action(QuitToDesktopMod.QTDConfig.Save));

            // Patch in our MOD
            harmony.PatchAll();
            Logger.LogInfo($"PluginName: {pluginName}, VersionString: {versionString} is loaded.");
            logger = Logger;
        }
    }
}
