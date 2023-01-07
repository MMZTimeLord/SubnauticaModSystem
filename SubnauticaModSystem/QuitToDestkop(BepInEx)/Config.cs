using UnityEngine.UI;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace QuitToDesktop
{
    // -----------------------------  Configuration Section  ----------------------------- //
    //                                                                                     //
    //       This section declares the Menu Attributes & Structure for SMLHelper.          //
    //                                                                                     //
    // The MenuAttribute allows us to set the title of our options menu in the "Mods" tab. //
    [Menu("Quit to Desktop")]
    public class QTDConfig : ConfigFile
    {
        // A Toggle is used to represent true or false
        //
        // Default is true
        [Toggle("Save Warning upon Quit to Desktop")]
        public bool SaveWarningQTD = true;
    }
    // ---------------------------  End Configuration Section  --------------------------- //

}
