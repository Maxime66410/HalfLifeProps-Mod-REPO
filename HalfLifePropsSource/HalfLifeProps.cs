using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using REPOLib;
using WebSocketSharp;

namespace HalfLifeProps;

[BepInPlugin("Maxime66410.HalfLifeProps", "HalfLifeProps", "1.0")]
[BepInDependency(REPOLib.MyPluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
public class HalfLifeProps : BaseUnityPlugin
{
    internal static HalfLifeProps Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger => Instance._logger;
    private ManualLogSource _logger => base.Logger;
    internal Harmony? Harmony { get; set; }

    private void Awake()
    {
        Instance = this;

        // Prevent the plugin from being deleted
        this.gameObject.transform.parent = null;
        this.gameObject.hideFlags = HideFlags.HideAndDontSave;

        Patch();

        Logger.LogInfo($"{Info.Metadata.GUID} v{Info.Metadata.Version} has loaded!");
    }

    internal void Patch()
    {
        Harmony ??= new Harmony(Info.Metadata.GUID);
        Harmony.PatchAll();
    }

    internal void Unpatch()
    {
        Harmony?.UnpatchSelf();
    }

    private void Start()
    {
        foreach (var bundle in AssetBundle.GetAllLoadedAssetBundles())
        {
            if (bundle.name == "halflifeprops")
            {
                foreach (var asset in bundle.GetAllAssetNames())
                {
                    Logger.LogInfo($"Asset: {asset}");
                }
                Logger.LogInfo($"{bundle.name} Is Loaded Successfully");
                Logger.LogInfo("Thank you for using HalfLifeProps!");
            }
        }
    }

    private void Update()
    {
        // Code that runs every frame goes here
    }
}