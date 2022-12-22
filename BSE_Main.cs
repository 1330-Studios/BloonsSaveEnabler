using MelonLoader;

using static HarmonyLib.AccessTools;

[assembly: MelonAuthorColor(255, 200, 200, 255)] // Color defined in other 1330 Studios mods
[assembly: MelonColor(0xFF, 0xF8, 0xF8, 0xFF)] // Ghost White
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6-Epic")]
[assembly: MelonInfo(typeof(BloonsSaveEnabler.BSE_Main), "Bloons Save Enabler", "1.0.1", "1330 Studios LLC")]

namespace BloonsSaveEnabler;
public sealed class BSE_Main : MelonMod {
    public override void OnInitializeMelon() {
        Patch("Assets.Scripts.Unity.Modding:CheckForMods", "BloonsSaveEnabler.BSE_Main:Modding_CheckForMods");
        Patch("Assets.Scripts.Unity.Modding:ReportAnalytics", "BloonsSaveEnabler.BSE_Main:Modding_ReportAnalytics");

        MelonLogger.Msg(System.Drawing.Color.Coral, "Bloons Save Enabler has loaded!");
    }

    private void Patch(string originalMethodName, string targetMethodName) {
        try {
            var original = Method(originalMethodName);
            var target = Method(targetMethodName);

            HarmonyInstance.Patch(original, new(target));
        } catch (Exception e) {
            LoggerInstance.BigError(e.Message);
            LoggerInstance.WriteLine();
            LoggerInstance.Error("BSE Patch failed, code update required");
        }
    }

    #region Patches

    internal static bool Modding_CheckForMods(ref bool __result) => __result = false;
    internal static bool Modding_ReportAnalytics(ref object __result) {
        __result = Task.CompletedTask;
        return false;
    }

    #endregion
}