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
        // Storage width limited so it does not break the GUI
        // Storage height limited to 1000 via 10x multiplier toggle
        // 
        [Toggle("Height x10 Multiplier (Non-Functional)")]
        public bool X10Set = false;
        [Slider("Inventory Width", 1, 8, DefaultValue = 6)]
        public int Inv_WOption = 6;
        [Slider("Inventory Height", 1, 100, DefaultValue = 200)]
        public int Inv_HOption = 8;
        [Slider("Escape Pod Locker Width", 1, 8, DefaultValue = 4)]
        public int EPL_WOption = 4;
        [Slider("Escape Pod Locker Height", 1, 100, DefaultValue = 200)]
        public int EPL_HOption = 8;
        [Slider("Waterproof Locker Width", 1, 8, DefaultValue = 4)]
        public int WPL_WOption = 4;
        [Slider("Waterproof Locker Height", 1, 100, DefaultValue = 200)]
        public int WPL_HOption = 4;
        [Slider("Small Locker Width", 1, 8, DefaultValue = 5)]
        public int SML_WOption = 5;
        [Slider("Small Locker Height", 1, 100, DefaultValue = 200)]
        public int SML_HOption = 6;
        [Slider("Locker Width", 1, 8, DefaultValue = 6)]
        public int LKR_WOption = 6;
        [Slider("Locker Height", 1, 100, DefaultValue = 200)]
        public int LKR_HOption = 8;
        [Slider("Carry-All Width", 1, 8, DefaultValue = 3)]
        public int CRY_WOption = 3;
        [Slider("Carry-All Height", 1, 100, DefaultValue = 200)]
        public int CRY_HOption = 3;
        [Slider("SeamothStorage Width", 1, 8, DefaultValue = 6)]
        public int SMS_WOption = 6;
        [Slider("SeamothStorage Height", 1, 100, DefaultValue = 200)]
        public int SMS_HOption = 6;
        [Slider("Bio-Reactor Width", 1, 8, DefaultValue = 4)]
        public int BIO_WOption = 4;
        [Slider("Bio-Reactor Height", 1, 100, DefaultValue = 200)]
        public int BIO_HOption = 4;
        [Slider("Cyclops Locker Width", 1, 8, DefaultValue = 3)]
        public int CYC_WOption = 3;
        [Slider("Cyclops Locker Height", 1, 100, DefaultValue = 200)]
        public int CYC_HOption = 6;
        [Slider("Exosuit Width", 1, 8, DefaultValue = 6)]
        public int EXO_WOption = 6;
        [Slider("Exosuit Base Height", 1, 100, DefaultValue = 200)]
        public int EXO_BHOption = 4;
        [Slider("Exosuit Height Per Storage Module", 1, 100, DefaultValue = 1)]
        public int EXO_HPSMOption = 1;
        [Slider("Filtration Machine Width", 1, 8, DefaultValue = 2)]
        public int FIL_WOption = 2;
        [Slider("Filtration Machine Height", 1, 100, DefaultValue = 200)]
        public int FIL_HOption = 2;
    }

    /*public class Eval
    {
        public static void X10(bool x, int x10Return)
        {
            if (x == true)
            {
                x10Return = 10;
            }
            else
            {
                x10Return = 1;
            }
            return;
        }
    }*/
}
