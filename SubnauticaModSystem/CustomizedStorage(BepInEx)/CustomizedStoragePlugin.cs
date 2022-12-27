using BepInEx;
using BepInEx.Logging;

namespace CustomizedStorage
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class CustomizedStorage : BaseUnityPlugin
    {
        private const string myGUID = "CustomizedStorage(BepInEx)";
        private const string pluginName = "Customized Storage";
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
