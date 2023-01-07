using System;
using UnityEngine.UI;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace CustomizedStorage
{
    // ======================== Options Mod Menu File code ======================== //
    //                                                                              //
    //    This section declares the Menu Attributes & Structure for SMLHelper.      //
    //                                                                              //
    // The MenuAttribute lets us set the title the options menu in the "Mods" tab.  //
    // ============================================================================ //
    [Menu("Customized Storage")]
    public class ModCFG : ConfigFile
    {
        // Set all the Storage Types and Option Menu items.
        //
        // This mod allows storage width and height Limited for initial release
        //
        [Slider("Inventory Width", 3, 300, DefaultValue = 6, Step = 3), OnChange(nameof(UpdateConfig))]
        public int Inv_WOption;
        [Slider("Inventory Height", 4, 400, DefaultValue = 8, Step = 4), OnChange(nameof(UpdateConfig))]
        public int Inv_HOption;
        [Slider("Escape Pod Locker Width", 2, 200, DefaultValue = 4, Step = 2)]
        public int EPL_WOption = 4;
        [Slider("Escape Pod Locker Height", 4, 400, DefaultValue = 8, Step = 4)]
        public int EPL_HOption = 8;
        [Slider("Waterproof Locker Width", 2, 200, DefaultValue = 4, Step = 2)]
        public int WPL_WOption = 4;
        [Slider("Waterproof Locker Height", 2, 200, DefaultValue = 4, Step = 2)]
        public int WPL_HOption = 4;
        [Slider("Small Locker Width", 2, 200, DefaultValue = 5, Step = 2)]
        public int SML_WOption = 5;
        [Slider("Small Locker Height", 3, 300, DefaultValue = 6, Step = 3)]
        public int SML_HOption = 6;
        [Slider("Locker Width", 3, 300, DefaultValue = 6, Step = 3)]
        public int LKR_WOption = 6;
        [Slider("Locker Height", 4, 400, DefaultValue = 8, Step = 4)]
        public int LKR_HOption = 8;
        [Slider("Carry-All Width", 3, 150, DefaultValue = 4, Step = 3)]
        public int CRY_WOption = 3;
        [Slider("Carry-All Height", 3, 150, DefaultValue = 4, Step = 3)]
        public int CRY_HOption = 3;
        [Slider("SeamothStorage Width", 3, 300, DefaultValue = 6, Step = 3)]
        public int SMS_WOption = 6;
        [Slider("SeamothStorage Height", 3, 300, DefaultValue = 6, Step = 3)]
        public int SMS_HOption = 6;
        [Slider("Bio-Reactor Width", 2, 200, DefaultValue = 4, Step = 2)]
        public int BIO_WOption = 4;
        [Slider("Bio-Reactor Height", 2, 200, DefaultValue = 4, Step = 2)]
        public int BIO_HOption = 4;
        [Slider("Cyclops Locker Width", 3, 150, DefaultValue = 3, Step = 3)]
        public int CYC_WOption = 3;
        [Slider("Cyclops Locker Height", 3, 300, DefaultValue = 6, Step = 3)]
        public int CYC_HOption = 6;
        [Slider("Exosuit Width", 3, 300, DefaultValue = 6, Step = 3)]
        public int EXO_WOption = 6;
        [Slider("Exosuit Base Height", 2, 200, DefaultValue = 4, Step = 2)]
        public int EXO_BHOption = 4;
        [Slider("Exosuit Height Per Storage Module", 1, 100, DefaultValue = 1)]
        public int EXO_HPSMOption = 1;
        [Slider("Filtration Machine Width", 2, 400, DefaultValue = 4, Step = 2)]
        public int FIL_WOption = 4;
        [Slider("Filtration Machine Height", 2, 400, DefaultValue = 4, Step = 2)]
        public int FIL_HOption = 4;

        public void UpdateConfig()
        {
            ModCFG modCFG = new ModCFG();
            Config config = new Config();
            config.Inventory.width = modCFG.Inv_WOption;
            config.Inventory.height = modCFG.Inv_HOption;
            config.EscapePodLocker.width = modCFG.EPL_WOption;
            config.EscapePodLocker.height = modCFG.EPL_HOption;
            config.WaterproofLocker.width = modCFG.WPL_WOption;
            config.WaterproofLocker.height = modCFG.WPL_HOption;
            config.SmallLocker.width = modCFG.SML_WOption;
            config.SmallLocker.height = modCFG.SML_HOption;
            config.Locker.width = modCFG.LKR_WOption;
            config.Locker.height = modCFG.LKR_HOption;
            config.SeamothStorage.width = modCFG.SMS_WOption;
            config.SeamothStorage.height = modCFG.SMS_HOption;
            config.CarryAll.width = modCFG.CRY_WOption;
            config.CarryAll.height = modCFG.CRY_HOption;
            config.CyclopsLocker.width = modCFG.CYC_WOption;
            config.CyclopsLocker.height = modCFG.CYC_HOption;
            config.BioReactor.width = modCFG.BIO_WOption;
            config.BioReactor.height = modCFG.BIO_HOption;
            config.Exosuit.width = modCFG.EXO_WOption;
            config.Exosuit.baseHeight = modCFG.EXO_BHOption;
            config.Exosuit.heightPerStorageModule = modCFG.EXO_HPSMOption;
            config.FiltrationMachine.width = modCFG.FIL_WOption;
            config.FiltrationMachine.height = modCFG.FIL_HOption;
            config.FiltrationMachine.maxSalt = (modCFG.FIL_WOption * modCFG.FIL_HOption) / 2;
            config.FiltrationMachine.maxWater = (modCFG.FIL_WOption * modCFG.FIL_HOption) / 2;
        }
    }
    // ====================== END Options Mod Menu File code ====================== //

    // =========================== Congfig.cs File code =========================== //
    //  This creates our Size and CustomSize classes:                               //
    //      ExosuitConfig and FiltrationMachineConfig                               //
    //  Last: builds our Config class to hold the defaults                          //
    // ============================================================================ //
    [Serializable]
    class Size
    {
        public int width;
        public int height;

        public Size(int w, int h)
        {
            width = w;
            height = h;
        }

    }

    [Serializable]
    class ExosuitConfig
    {
        static ModCFG modCFG = new ModCFG();
        public int width = modCFG.EXO_WOption;                   // Default is 6
        public int baseHeight = modCFG.EXO_BHOption;              // Default is 4
        public int heightPerStorageModule = modCFG.EXO_HPSMOption;// Default is 1
    }

    [Serializable]
    class FiltrationMachineConfig
    {
        static ModCFG modCFG = new ModCFG();
        public int width = modCFG.FIL_WOption;  // Default is 4
        public int height = modCFG.FIL_HOption; // Default is 4

        // These values are calculated off width x height / 2 favoring truncating any remainder
        public int maxSalt = (modCFG.FIL_WOption * modCFG.FIL_HOption) / 2;             // Default is 8
        public int maxWater = (modCFG.FIL_WOption * modCFG.FIL_HOption) / 2;            // Default is 8
    }

    [Serializable]
    class Config
    {
        static ModCFG modCFG = new ModCFG();
        public Size Inventory = new Size(6, 6);       // Default is 6 x 6
        public Size EscapePodLocker = new Size(modCFG.EPL_WOption, modCFG.EPL_HOption); // Default is 4 x 8
        public Size WaterproofLocker = new Size(modCFG.WPL_WOption, modCFG.WPL_HOption);// Default is 4 x 4
        public Size SmallLocker = new Size(modCFG.SML_WOption, modCFG.SML_HOption);     // Default is 4 x 8
        public Size Locker = new Size(modCFG.LKR_WOption, modCFG.LKR_HOption);          // Default is 6 x 8
        public Size SeamothStorage = new Size(modCFG.SMS_WOption, modCFG.SMS_HOption);  // Default is 6 x 8
        public Size CarryAll = new Size(modCFG.CRY_WOption, modCFG.CRY_HOption);        // Default is 3 x 3
        public Size CyclopsLocker = new Size(modCFG.CYC_WOption, modCFG.CYC_HOption);   // Default is 3 x 6
        public Size BioReactor = new Size(modCFG.BIO_WOption, modCFG.BIO_HOption);      // Default is 6 x 8
        public ExosuitConfig Exosuit = new ExosuitConfig();
        public FiltrationMachineConfig FiltrationMachine = new FiltrationMachineConfig();
    }
    // ========================= END Congfig.cs File code ========================= //
}

