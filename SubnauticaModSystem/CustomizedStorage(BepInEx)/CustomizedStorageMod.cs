using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using SMLHelper.V2.Handlers;

namespace CustomizedStorage
{
    public class Mod
    {
        // make Config structure available for us to use from the config file

        // make Mod Options available for use from the config file
        internal static ModCFG ModCFG { get; } = OptionsPanelHandler.RegisterModOptions<ModCFG>();
        //public static Config config;

    }

    // ============================== END Congfig code ============================ //
    // =============================== Patches code =============================== //
    //  This code composes the various patches that actuall affect game code.       //
    //  StorageContainer_Awake_Patch returns a value denoting the type of storage   //
    //  that was Awakened (? to be verified)                                        //
    // ============================================================================ //

    // Harmony patch to this section/object in the game
    [HarmonyPatch(typeof(StorageContainer))]
    [HarmonyPatch("Awake")]
    class StorageContainer_Awake_Patch
    {
        private static List<string> names = new List<string>();

        private static bool Prefix(StorageContainer __instance)
        {
            if (IsSmallLocker(__instance))
            {
                __instance.Resize(Mod.ModCFG.SML_WOption, Mod.ModCFG.SML_HOption);
            }
            else if (IsLargeLocker(__instance))
            {
                __instance.Resize(Mod.ModCFG.LKR_WOption, Mod.ModCFG.LKR_HOption);
            }
            else if (IsEscapePodLocker(__instance))
            {
                __instance.Resize(Mod.ModCFG.EPL_WOption, Mod.ModCFG.EPL_HOption);
            }
            else if (IsCyclopsLocker(__instance))
            {
                __instance.Resize(Mod.ModCFG.CYC_WOption, Mod.ModCFG.CYC_HOption);
            }
            else if (IsWaterproofLocker(__instance))
            {
                __instance.Resize(Mod.ModCFG.WPL_WOption, Mod.ModCFG.WPL_HOption);
            }
            else if (IsCarryAll(__instance))
            {
                __instance.Resize(Mod.ModCFG.CRY_WOption, Mod.ModCFG.CRY_HOption);
            }

            return true;
        }

        private static bool IsSmallLocker(StorageContainer __instance)
        {
            return __instance.gameObject.name.StartsWith("SmallLocker");
        }

        private static bool IsLargeLocker(StorageContainer __instance)
        {
            return __instance.gameObject.name.StartsWith("Locker");
        }

        private static bool IsEscapePodLocker(StorageContainer __instance)
        {
            return __instance.gameObject.GetComponent<SpawnEscapePodSupplies>() != null;
        }

        private static bool IsCyclopsLocker(StorageContainer __instance)
        {
            return __instance.gameObject.name.StartsWith("submarine_locker_01_door");
        }

        private static bool IsWaterproofLocker(StorageContainer __instance)
        {
            return __instance.gameObject.GetComponent<SmallStorage>() != null;
        }

        private static bool IsCarryAll(StorageContainer __instance)
        {
            return __instance.transform.parent != null && __instance.transform.parent.gameObject.name.StartsWith("docking_luggage_01_bag4");
        }
    }

    // Harmony patch to this section/object in the game
    [HarmonyPatch(typeof(Inventory))]
    [HarmonyPatch("Awake")]
    public class Inventory_Awake_Patch
    {
        public static readonly FieldInfo Inventory_container = typeof(Inventory).GetField("_container", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void Postfix(Inventory __instance)
       {
            // Hard debugging code
            
            //CustomizedStoragePlugin.logger.LogInfo("======================================== DEBUG LOGGING ========================================");
            //CustomizedStoragePlugin.logger.LogInfo("Options Inventory Size Set to:");
            //CustomizedStoragePlugin.logger.LogInfo("Width: " + Mod.ModCFG.Inv_WOption + ", Height: " + Mod.ModCFG.Inv_HOption);
            var container = (ItemsContainer)Inventory_container.GetValue(__instance);
            container.Resize(Mod.ModCFG.Inv_WOption, Mod.ModCFG.Inv_HOption);
            //CustomizedStoragePlugin.logger.LogInfo("======================================= RESIZE SUCCESS! =======================================");
        }
    }

    // Harmony patch to this section/object in the game
    [HarmonyPatch(typeof(SeamothStorageContainer))]
    [HarmonyPatch("Init")]
    class SeamothStorageContainer_Init_Patch
    {
        private static bool Prefix(SeamothStorageContainer __instance)
        {
            __instance.width = Mod.ModCFG.SMS_WOption;
            __instance.height = Mod.ModCFG.SMS_HOption;
            return true;
        }
    }

    // Harmony patch to this section/object in the game
    [HarmonyPatch(typeof(Exosuit))]
    [HarmonyPatch("UpdateStorageSize")]
    class Exosuit_UpdateStorageSize_Patch
    {
        private static void Postfix(Exosuit __instance)
        {
            int height = Mod.ModCFG.EXO_BHOption + (Mod.ModCFG.EXO_HPSMOption * __instance.modules.GetCount(TechType.VehicleStorageModule));
            __instance.storageContainer.Resize(Mod.ModCFG.EXO_WOption, height);
        }
    }

    // Harmony patch to this section/object in the game
    [HarmonyPatch(typeof(BaseBioReactor))]
    [HarmonyPatch("get_container")]
    class BaseBioReactor_get_container_Patch
    {
        private static readonly FieldInfo BaseBioReactor_container = typeof(BaseBioReactor).GetField("_container", BindingFlags.NonPublic | BindingFlags.Instance);

        private static void Postfix(BaseBioReactor __instance)
        {
            ItemsContainer container = (ItemsContainer)BaseBioReactor_container.GetValue(__instance);
            container.Resize(Mod.ModCFG.BIO_WOption, Mod.ModCFG.BIO_HOption);
        }
    }

    // Harmony patch to this section/object in the game
    [HarmonyPatch(typeof(FiltrationMachine))]
    [HarmonyPatch("Start")]
    class FiltrationMachine_Start_Patch
    {
        private static void Postfix(FiltrationMachine __instance)
        {
            __instance.maxSalt = (Mod.ModCFG.FIL_WOption * Mod.ModCFG.FIL_HOption) / 2;
            __instance.maxWater = (Mod.ModCFG.FIL_WOption * Mod.ModCFG.FIL_HOption) / 2;
            __instance.storageContainer.Resize(Mod.ModCFG.FIL_WOption, Mod.ModCFG.FIL_HOption);
        }
    }

    // Harmony patch to this section/object in the game
    [HarmonyPatch(typeof(uGUI_ItemsContainer))]
    [HarmonyPatch("OnResize")]
    class uGUI_ItemsContainer_OnResize_Patch
    {
        private static void Postfix(uGUI_ItemsContainer __instance, int width, int height)
        {
            var x = __instance.rectTransform.anchoredPosition.x;
            if (height == 9)
            {
                __instance.rectTransform.anchoredPosition = new Vector2(x, -39);
            }
            else if (height == 10)
            {
                __instance.rectTransform.anchoredPosition = new Vector2(x, -75);
            }
            else
            {
                __instance.rectTransform.anchoredPosition = new Vector2(x, -4);
            }

            var y = __instance.rectTransform.anchoredPosition.y;
            var sign = Mathf.Sign(x);
            if (width == 8)
            {
                __instance.rectTransform.anchoredPosition = new Vector2(sign * (284 + 8), y);
            }
            else
            {
                __instance.rectTransform.anchoredPosition = new Vector2(sign * 284, y);

            }
        }
    }
    // ============================= END Patches code ============================= //
}

