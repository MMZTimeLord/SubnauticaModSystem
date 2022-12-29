using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SMLHelper.V2.Handlers;

namespace QuitToDesktop
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class QuitToDesktopePlugin : BaseUnityPlugin
    {
        // Mod Information: Original Base Code by Randy Knapp with QMM
        private const string myGUID = "com.mmztimelord.quittodesktop";
        private const string pluginName = "Quit To Desktop BepInEx Mod";
        private const string versionString = "2.0.0";

        private static readonly Harmony harmony = new Harmony(myGUID);

        public static ManualLogSource logger;

        private void Awake()
        {
            IngameMenuHandler.RegisterOnSaveEvent(new System.Action(QuitToDesktopMod.QTDConfig.Save));

            // Patch in our MOD
            harmony.PatchAll();
            Logger.LogInfo($"PluginName: {pluginName}, VersionString: {versionString} is loaded.");
            logger = Logger;
        }
    }
}
