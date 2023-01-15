using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SMLHelper.V2.Handlers;

namespace CustomizedStorage
{
    [BepInDependency("com.ahk1221.smlhelper")]
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class CustomizedStoragePlugin : BaseUnityPlugin
    {

        // Mod Information: Edit the size/capacity of any container in the game, including the player inventory!
        // Original Base Code by Randy Knapp with QMM
        private const string myGUID = "com.mmztimelord.customizedstorage";
        private const string pluginName = "Customized Storage capacity";
        private const string versionString = "2.0.0"; // BepInEx release w/Options Menu Config Controls

        private static readonly Harmony harmony = new Harmony(myGUID);

        public static ManualLogSource logger;

        public void Awake()
        {
            // This will make sure our configuration is saved to a file for use later.
            IngameMenuHandler.RegisterOnSaveEvent(new System.Action(Mod.ModCFG.Save));

            // Patch in our MOD and create an entry in the Log of it loading.
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), myGUID);
            Logger.LogInfo($"PluginName: {pluginName}, VersionString: {versionString} is loaded.");
            logger = Logger;
        }
    }
}
