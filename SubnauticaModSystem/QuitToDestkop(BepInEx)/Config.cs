using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

/// The MenuAttribute allows us to set the title of our options menu in the "Mods" tab.
[Menu("Quit to Desktop")]
public class QTDConfig : ConfigFile
{
    /// A Toggle is used to represent true or false
    ///
    /// Default is true
    [Toggle("Save Warning upon Quit to Desktop")]
    public bool SaveWarningQTD = true;
}