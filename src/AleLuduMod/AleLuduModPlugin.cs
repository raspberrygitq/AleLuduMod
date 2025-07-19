﻿using AmongUs.GameOptions;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Reactor;
using BepInEx.Configuration;
using System.Linq;

namespace AleLuduMod;

[BepInAutoPlugin("pl.townofus.aleludu")]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
[BepInDependency("gg.reactor.debugger", BepInDependency.DependencyFlags.SoftDependency)] // fix debugger overwriting MinPlayers
[BepInDependency("com.slushiegoose.townofus", BepInDependency.DependencyFlags.SoftDependency)] // load after town of us
public partial class AleLuduModPlugin : BasePlugin
{
    public const int MaxPlayers = 36;
    public const int MaxImpostors = 36 / 2;
    public static ConfigEntry<bool> Force4Columns { get; set; }
    private Harmony Harmony { get; } = new(Id);

    public override void Load()
    { 
        NormalGameOptionsV09.RecommendedImpostors = NormalGameOptionsV09.MaxImpostors = Enumerable.Repeat(36, 36).ToArray();
        NormalGameOptionsV09.MinPlayers = Enumerable.Repeat(4, 36).ToArray();
        HideNSeekGameOptionsV09.MinPlayers = Enumerable.Repeat(4, 36).ToArray();

        Force4Columns = Config.Bind("Settings", "Force 4 columns", true, "Always display 4 columns in meeting, vitals, etc.");

        Harmony.PatchAll();
    }
}