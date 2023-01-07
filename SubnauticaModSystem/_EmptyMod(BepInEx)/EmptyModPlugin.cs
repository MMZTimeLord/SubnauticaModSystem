using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SMLHelper.V2.Handlers;

namespace EmptyMod
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class EmptyModPlugin : BaseUnityPlugin
    {
        // Mod Information: fill in these fields and replace this instruction
        private const string myGUID = "com.username.modname";
        private const string pluginName = "Mod description";
        private const string versionString = "1.0.0";

        private static readonly Harmony harmony = new Harmony(myGUID);

        public static ManualLogSource logger;

        private void Awake()
        {
            // This will make sure our configuration is saved to a file for use later.
            IngameMenuHandler.RegisterOnSaveEvent(new System.Action(EmptyMod_Mod.EMConfig.Save));

            // Patch in our MOD and create an entry in the Log of it loading.
            harmony.PatchAll();
            Logger.LogInfo($"PluginName: {pluginName}, VersionString: {versionString} is loaded.");
            logger = Logger;
        }
    }
}
