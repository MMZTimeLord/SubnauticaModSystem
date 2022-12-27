using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace QuitToDesktop
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class QuitToDesktopePlugin : BaseUnityPlugin
    {
        private const string myGUID = "com.mmztimelord.quittodesktop";
        private const string pluginName = "Quit To Desktop BepInEx Mod";
        private const string versionString = "1.0.5";

        private static readonly Harmony harmony = new Harmony(myGUID);

        public static ManualLogSource logger;

        private void Awake()
        {
            harmony.PatchAll();
            Logger.LogInfo(pluginName + " " + versionString + " " + "loaded.");
            logger = Logger;
        }
    }
}
