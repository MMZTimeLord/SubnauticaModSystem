using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace CustomizedStorage
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class CustomizedStorage : BaseUnityPlugin
    {
        
        // Mod Information: Edit the size/capacity of any container in the game, including the player inventory!
        // Original Base Code by RandyKnapp - Updated for BepInEx by d2allgr
        public const string
            MODNAME = "CustomizedStorage",
            AUTHOR = "RandyKnapp - Updated by d2allgr",
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
