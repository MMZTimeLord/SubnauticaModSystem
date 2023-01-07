using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace CustomizedStorage
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class CustomizedStorage : BaseUnityPlugin
    {
        public const string
            MODNAME = "CustomizedStorage",
            AUTHOR = "RandyKnapp",
            GUID = AUTHOR + "_" + MODNAME,
            VERSION = "1.0.5.0";

        private void Awake()
        {
            Mod.LoadConfig();
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);
            Logger.LogInfo(MODNAME + " " + VERSION + " " + "loaded.");
        }
    }
}
