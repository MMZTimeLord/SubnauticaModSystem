using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace QuitToDesktop
{
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

    class QuitToDesktopMod
    {
        internal static QTDConfig QTDConfig { get; } = OptionsPanelHandler.RegisterModOptions<QTDConfig>();

    [HarmonyPatch(typeof(IngameMenu), nameof(IngameMenu.Open))]
        public static class IngameMenu_Open_Patch
        {

            static Button quitButton;
            static GameObject quitConfirmation;
            static GameObject quitConfirmationWithSaveWarning;

            [HarmonyPostfix]
            public static void Postfix(IngameMenu __instance)
            {
                if (__instance != null && quitButton == null)
                {
                    // make a copy of Confirmation with Save Warning Menu
                    var quitConfirmationWithSaveWarningPrefab = __instance.gameObject.FindChild("QuitConfirmationWithSaveWarning");
                    quitConfirmationWithSaveWarning = GameObject.Instantiate(quitConfirmationWithSaveWarningPrefab, __instance.gameObject.FindChild("QuitConfirmationWithSaveWarning").transform.parent);
                    quitConfirmationWithSaveWarning.name = "QuitToDesktopConfirmationWithSaveWarning";

                    // get the No Button from Confirmation with Save Warning Menu and add the needed listeners to it
                    var noButtonWithSaveWarningPrefab = quitConfirmationWithSaveWarning.gameObject.transform.Find("ButtonNo").GetComponent<Button>();
                    noButtonWithSaveWarningPrefab.onClick.RemoveAllListeners();
                    noButtonWithSaveWarningPrefab.onClick.AddListener(() => { __instance.Close(); });

                    // get the Yes Button from Confirmation with Save Warning Menu and add the needed listeners to it
                    var yesButtonWithSaveWarningPrefab = quitConfirmationWithSaveWarning.gameObject.transform.Find("ButtonYes").GetComponent<Button>();
                    yesButtonWithSaveWarningPrefab.onClick.RemoveAllListeners();
                    yesButtonWithSaveWarningPrefab.onClick.AddListener(() => { __instance.QuitGame(true); });

                    // make a copy of Confirmation Menu
                    var quitConfirmationPrefab = __instance.gameObject.FindChild("QuitConfirmation");
                    quitConfirmation = GameObject.Instantiate(quitConfirmationPrefab, __instance.gameObject.FindChild("QuitConfirmation").transform.parent);
                    quitConfirmation.name = "QuitToDesktopConfirmation";

                    // get the No Button from Confirmation Menu and add the needed listeners to it
                    var noButtonPrefab = quitConfirmation.gameObject.transform.Find("ButtonNo").GetComponent<Button>();
                    noButtonPrefab.onClick.RemoveAllListeners();
                    noButtonPrefab.onClick.AddListener(() => { __instance.Close(); });

                    // get the Yes Button from Confirmation Menu and add the needed listeners to it
                    var yesButtonPrefab = quitConfirmation.gameObject.transform.Find("ButtonYes").GetComponent<Button>();
                    yesButtonPrefab.onClick.RemoveAllListeners();
                    yesButtonPrefab.onClick.AddListener(() => { __instance.QuitGame(true); });

                    // make the Quit To Desktop Button
                    var buttonPrefab = __instance.quitToMainMenuButton;
                    quitButton = GameObject.Instantiate(buttonPrefab, __instance.quitToMainMenuButton.transform.parent);
                    quitButton.name = "ButtonQuitToDesktop";
                    quitButton.onClick.RemoveAllListeners();
                    // quitButton.onClick.AddListener(() => { __instance.gameObject.FindChild("QuitConfirmationWithSaveWarning").SetActive(false); }); // set the confirmation with save false so it doesn't conflict
                    // quitButton.onClick.AddListener(() => { __instance.gameObject.FindChild("QuitConfirmation").SetActive(false); }); // set the Quit To Main Menu confirmation to false so it doesn't conflict
                }

                if (QTDConfig.SaveWarningQTD == true)
                {
                    quitButton.onClick.AddListener(() => { quitConfirmationWithSaveWarning.SetActive(true); });
                    quitButton.onClick.AddListener(() => { quitConfirmation.SetActive(false); });
                }
                else
                {
                    quitButton.onClick.AddListener(() => { quitConfirmationWithSaveWarning.SetActive(false); });
                    quitButton.onClick.AddListener(() => { quitConfirmation.SetActive(true); });
                }

                IEnumerable <TMPro.TextMeshProUGUI> NBtexts = quitButton.GetComponents<TMPro.TextMeshProUGUI>().Concat(quitButton.GetComponentsInChildren<TMPro.TextMeshProUGUI>());
                foreach (TMPro.TextMeshProUGUI text in NBtexts)
                {
                    text.text = "Quit to Desktop";
                }

                IEnumerable <TMPro.TextMeshProUGUI> MMtexts = __instance.quitToMainMenuButton.GetComponents<TMPro.TextMeshProUGUI>().Concat(__instance.quitToMainMenuButton.GetComponentsInChildren<TMPro.TextMeshProUGUI>());
                foreach (TMPro.TextMeshProUGUI text in MMtexts)
                {
                    text.text = "Quit to Main Menu";
                }
            }
        }
    }
}
