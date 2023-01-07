using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using HarmonyLib;

namespace CustomizedStorage
{
    /// <summary>
    /// Class to mod Storage Capacity
    /// </summary>
                                                                                
    // =========================== Congfig.cs File code =========================== //
    //  This creates our Size and CustomSize classes:                               //
    //      ExosuitConfig and FiltrationMachineConfig                               //
    //  Last: builds our Config class to hold the defaults                          //
    // ============================================================================ //
    [Serializable]
    class Size : IComparable
    {
        public int width;
        public int height;

        public Size(int w, int h)
        {
            width = w;
            height = h;
        }

        public int CompareTo(object otherObj)
        {
            Size other = otherObj as Size;
            if (width < other.width || height < other.height)
            {
                return -1;
            }
            else if (width > other.width || height > other.height)
            {
                return 1;
            }
            return 0;
        }

        public override string ToString()
        {
            return "(" + width + ", " + height + ")";
        }
    }

    [Serializable]
    class ExosuitConfig
    {
        public int width = 6;
        public int baseHeight = 4;
        public int heightPerStorageModule = 1;
    }

    [Serializable]
    class FiltrationMachineConfig
    {
        public int width = 2;
        public int height = 2;
        public int maxSalt = 2;
        public int maxWater = 2;
    }

    [Serializable]
    class Config
    {
        public Size Inventory = new Size(6, 8);
        public Size SmallLocker = new Size(5, 6);
        public Size Locker = new Size(6, 8);
        public Size EscapePodLocker = new Size(4, 8);
        public Size CyclopsLocker = new Size(3, 6);
        public Size WaterproofLocker = new Size(4, 4);
        public Size CarryAll = new Size(3, 3);
        public ExosuitConfig Exosuit = new ExosuitConfig();
        public Size SeamothStorage = new Size(4, 4);
        public Size BioReactor = new Size(4, 4);
        public FiltrationMachineConfig FiltrationMachine = new FiltrationMachineConfig();
    }
    // ========================= END Congfig.cs File code ========================= //

    // ============================= Mod.cs File code ============================= //
    //  This code is all about loading and saving the Config.JSON file and          //
    //  validating it. NOT needed with in-game menu mod options. SMLHelper does     //
    //  this job for us and the player need not open a file to change the config    //
    // ============================================================================ //
    static class Mod
    {
        public static Config config;

        public static string GetModPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private static string GetConfigPath()
        {
            return GetModPath() + "/config.json";
        }

        public static void LoadConfig()
        {
            string filePath = GetConfigPath();
            if (!File.Exists(filePath))
            {
                config = new Config();
                File.WriteAllText(filePath, JsonConvert.SerializeObject(config, Formatting.Indented));
                return;
            }

            string configJson = File.ReadAllText(filePath);
            config = JsonConvert.DeserializeObject<Config>(configJson);
            if (config == null)
            {
                config = new Config();
            }

            ValidateConfig();
        }

        private static void ValidateConfig()
        {
            Config defaultConfig = new Config();
            if (config == null)
            {
                config = defaultConfig;
                return;
            }
            var min = new Size(1, 1);
            var max = new Size(10000, 10000);

            config.Inventory = ValidateConfigValue("Inventory", min, max, config.Inventory, defaultConfig.Inventory);
            config.SmallLocker = ValidateConfigValue("SmallLocker", min, max, config.SmallLocker, defaultConfig.SmallLocker);
            config.Locker = ValidateConfigValue("Locker", min, max, config.Locker, defaultConfig.Locker);
            config.EscapePodLocker = ValidateConfigValue("EscapePodLocker", min, max, config.EscapePodLocker, defaultConfig.EscapePodLocker);
            config.CyclopsLocker = ValidateConfigValue("CyclopsLocker", min, max, config.CyclopsLocker, defaultConfig.CyclopsLocker);
            config.WaterproofLocker = ValidateConfigValue("WaterproofLocker", min, max, config.WaterproofLocker, defaultConfig.WaterproofLocker);
            config.CarryAll = ValidateConfigValue("CarryAll", min, max, config.CarryAll, defaultConfig.CarryAll);
            config.SeamothStorage = ValidateConfigValue("SeamothStorage", min, max, config.SeamothStorage, defaultConfig.SeamothStorage);

            config.Exosuit.width = ValidateConfigValue("Exosuit.width", min.width, max.width, config.Exosuit.width, defaultConfig.Exosuit.width);
            config.Exosuit.baseHeight = ValidateConfigValue("Exosuit.baseHeight", min.height, max.height, config.Exosuit.baseHeight, defaultConfig.Exosuit.baseHeight);

            config.FiltrationMachine.width = ValidateConfigValue("FiltrationMachine.width", min.width, max.width, config.FiltrationMachine.width, defaultConfig.FiltrationMachine.width);
            config.FiltrationMachine.height = ValidateConfigValue("FiltrationMachine.height", min.height, max.height, config.FiltrationMachine.height, defaultConfig.FiltrationMachine.height);
            var totalSpace = config.FiltrationMachine.width * config.FiltrationMachine.height;
            var totalContents = config.FiltrationMachine.maxSalt + config.FiltrationMachine.maxWater;
            if (totalContents > totalSpace)
            {
                Console.WriteLine("Config values for 'FiltrationMachine' were not valid. Total capacity is {0} but the maxWater and maxSalt combined are more than that. (maxSalt={1}, maxWater={2})",
                    totalSpace,
                    config.FiltrationMachine.maxSalt,
                    config.FiltrationMachine.maxWater
                );
                config.FiltrationMachine.maxSalt = Mathf.FloorToInt(totalSpace / 2);
                config.FiltrationMachine.maxWater = Mathf.CeilToInt(totalSpace / 2);
            }
        }

        public static T ValidateConfigValue<T>(string field, T min, T max, T value, T defaultValue) where T : IComparable
        {
            if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            {
                Console.WriteLine("Config value for '{0}' ({1}) was not valid. Must be between {2} and {3}",
                    field,
                    value,
                    min,
                    max
                );
                return defaultValue;
            }
            return value;
        }
    }
    // =========================== END Mod.cs File code =========================== //

    // =========================== Patches.cs File code =========================== //
    //  This code composes the various patches that actuall affect game code.       //
    //  StorageContainer_Awake_Patch returns a value denoting the type of storage   //
    //  that was Awakened (? to be verified)                                        //
    // ============================================================================ //
    namespace Patches
    {
        [HarmonyPatch(typeof(StorageContainer))]
        [HarmonyPatch("Awake")]
        class StorageContainer_Awake_Patch
        {
            private static List<string> names = new List<string>();

            private static bool Prefix(StorageContainer __instance)
            {
                if (IsSmallLocker(__instance))
                {
                    SetSize(__instance, Mod.config.SmallLocker);
                }
                else if (IsLargeLocker(__instance))
                {
                    SetSize(__instance, Mod.config.Locker);
                }
                else if (IsEscapePodLocker(__instance))
                {
                    SetSize(__instance, Mod.config.EscapePodLocker);
                }
                else if (IsCyclopsLocker(__instance))
                {
                    SetSize(__instance, Mod.config.CyclopsLocker);
                }
                else if (IsWaterproofLocker(__instance))
                {
                    SetSize(__instance, Mod.config.WaterproofLocker);
                }
                else if (IsCarryAll(__instance))
                {
                    SetSize(__instance, Mod.config.CarryAll);
                }

                return true;
            }

            private static void SetSize(StorageContainer __instance, Size size)
            {
                __instance.Resize(size.width, size.height);
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

        [HarmonyPatch(typeof(Inventory))]
        [HarmonyPatch("Awake")]
        class Inventory_Awake_Patch
        {
            private static readonly FieldInfo Inventory_container = typeof(Inventory).GetField("_container", BindingFlags.NonPublic | BindingFlags.Instance);

            private static void Postfix(Inventory __instance)
            {
                var container = (ItemsContainer)Inventory_container.GetValue(__instance);
                container.Resize(Mod.config.Inventory.width, Mod.config.Inventory.height);
            }
        }

        [HarmonyPatch(typeof(SeamothStorageContainer))]
        [HarmonyPatch("Init")]
        class SeamothStorageContainer_Init_Patch
        {
            private static bool Prefix(SeamothStorageContainer __instance)
            {
                __instance.width = Mod.config.SeamothStorage.width;
                __instance.height = Mod.config.SeamothStorage.height;
                return true;
            }
        }

        [HarmonyPatch(typeof(Exosuit))]
        [HarmonyPatch("UpdateStorageSize")]
        class Exosuit_UpdateStorageSize_Patch
        {
            private static void Postfix(Exosuit __instance)
            {
                int height = Mod.config.Exosuit.baseHeight + (Mod.config.Exosuit.heightPerStorageModule * __instance.modules.GetCount(TechType.VehicleStorageModule));
                __instance.storageContainer.Resize(Mod.config.Exosuit.width, height);
            }
        }

        [HarmonyPatch(typeof(BaseBioReactor))]
        [HarmonyPatch("get_container")]
        class BaseBioReactor_get_container_Patch
        {
            private static readonly FieldInfo BaseBioReactor_container = typeof(BaseBioReactor).GetField("_container", BindingFlags.NonPublic | BindingFlags.Instance);

            private static void Postfix(BaseBioReactor __instance)
            {
                ItemsContainer container = (ItemsContainer)BaseBioReactor_container.GetValue(__instance);
                container.Resize(Mod.config.BioReactor.width, Mod.config.BioReactor.height);
            }
        }

        [HarmonyPatch(typeof(FiltrationMachine))]
        [HarmonyPatch("Start")]
        class FiltrationMachine_Start_Patch
        {
            private static void Postfix(FiltrationMachine __instance)
            {
                __instance.maxSalt = Mod.config.FiltrationMachine.maxSalt;
                __instance.maxWater = Mod.config.FiltrationMachine.maxWater;
                __instance.storageContainer.Resize(Mod.config.FiltrationMachine.width, Mod.config.FiltrationMachine.height);
            }
        }

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
    }
    // ========================= END Patches.cs File code ========================= //
}
