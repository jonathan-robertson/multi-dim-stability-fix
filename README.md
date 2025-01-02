# Multi-Dim Stability Fix

[![🧪 Tested with 7DTD 1.2 (b27)](https://img.shields.io/badge/🧪%20Tested%20with-7DTD%201.2%20(b27)-blue.svg)](https://7daystodie.com/)
[![✅ Single Player Supported](https://img.shields.io/badge/✅%20Single%20Player-Supported-blue.svg)](https://7daystodie.com/)
[![❌ Dedicated Servers and P2P Unsupported](https://img.shields.io/badge/❌%20Dedicated%20Servers%20and%20P2P-Unsupported-blue.svg)](https://7daystodie.com/)
[![📦 Automated Release](https://github.com/jonathan-robertson/multi-dim-stability-fix/actions/workflows/release.yml/badge.svg)](https://github.com/jonathan-robertson/multi-dim-stability-fix/actions/workflows/release.yml)

## Summary

7 Days to Die mod: Provides a fix for the multi-dim stability exploit allowing players to detect underground tunnels or bases by holding a multi-dim block and hovering the placement ghost frame over that piece of terrain.

### Demonstration of Issue / Exploit for Reproduction Purposes

[![Screenshot](media/navezgane-multidim-stability-exploit.png)](https://youtu.be/EQpqDDOOJP4)

📺 [Link to Video](https://youtu.be/EQpqDDOOJP4)

## Features

This client-side mod addresses an exploit that currently allows players to detect underground tunnels by holding out any multi-dim (multi-dimension) block which also doesn't support stability.

> 🛈 This can be used to identify any gap in the ground between bedrock and your placement location at any distance.

An example of this kind of block would be the `Workbench` or a `bale of hay`.

### Who is this mod for?

- This is primarily meant for demonstration purposes to help with a bug report I plan to file with The Fun Pimps, designers of 7 Days to Die.
- *Maybe* Overhaul modders would also benefit from this, but probably not anyone else (since this is client-side only, due to limitations within the game's systems).

### Admin Commands

> 🛈 You can always search for this command or any command by running:
>
> - `help * <partial or complete command name>`
> - or get details about this (or any) command and its options by running `help <command>`

|        primary         | alternate |     params     | description                                                           |
| :--------------------: | :-------: | :------------: | --------------------------------------------------------------------- |
| `multidimstabilityfix` |  `mdsf`   | `debug` / `dm` | enable/disable debug/trace logging for this mod (disabled by default) |

*Note that leaving debug mode on can have a negative impact on performance. It is therefore recommended to only turn it on while troubleshooting and then disable it afterwards.*

## The Exploit Explained

Some odd behavior exists that can be used to take advantage of locating underground player bases for raiding purposes.

Context: a block that...

- has `MultiBlockDim` set to a Y/height value greater than 1
- also has `StabilitySupport` set to `false`

The adjustment I made was in `StabilityCalculator.GetBlockStabilityIfPlaced`

1. Near the beginning of this method is a check for `if (block.isMultiBlock)`... which it is in this case.
2. Each of the blocks that make up this Multi-Dimension block are collected while the minimum and maximum vector positions are determined.
3. After this, some additional work is performed which I won't get into...
4. And finally, each block is analyzed for stability mechanics.
   - *this is where the problem takes place*

For reasons I don't perfectly understand and will not speculate on, the game starts by checking the block positions at the top of the multi-dim block and this results in a stability failure - if and only if there is not a complete chain of blocks (standard blocks or terrain) between the proposed placement location and bedrock.

> 🛈 The client-side callstack goes PlayerMoveController.Update -> RenderDisplacedCube.Update -> RenderDisplacedCube.update0 -> StabilityCalculator.GetBlockStabilityIfPlaced.
>
> ⚠️ This is a client-side issue that cannot be addressed by a server-side mod; HELP US, FUN PIMPS - YOU'RE OUR ONLY HOPE!!

### Possible Fix Proposal

So how can we fix this?

My approach is to skip any blocks which are not identified as the lowest layer of blocks within this Multi-Dimension block.

Once adjusted, multi-dim blocks of height > 1 will suggest stability just like other multi-dim blocks of height 1 or non-multi-dim blocks do.

Inside, we find this block of code (keep in mind, this is decompiled, so the variable names do not reflect source):

```cs
// ...
for (var i = block.multiBlockPos.Length - 1; i >= 0; i--)
{
    var vector3i3 = block.multiBlockPos.Get(i, type, rotation) + _pos;
    if (!StabilityCalculator.posPlaced.ContainsKey(vector3i3))
    {
        StabilityCalculator.posPlaced.Add(vector3i3, num);
        vector3i = Vector3i.Min(vector3i, vector3i3); // <----- NOTICE THIS; WE REFER TO IT BELOW
        vector3i2 = Vector3i.Max(vector3i2, vector3i3);
    }
}
// ...
using (var enumerator = StabilityCalculator.posPlaced.Keys.GetEnumerator())
{
    while (enumerator.MoveNext())
    {
        var vector3i5 = enumerator.Current;
        // === START NEW =========================================================================
        if (vector3i5.y != vector3i.y) // verify if this block position is at base of multi-dim
        {
            continue; // skip block positions which are not along the lower edge of this multi-dim
        }
        // === END NEW ===========================================================================
        if (vector3i5.x == vector3i.x || vector3i5.x == vector3i2.x || vector3i5.y == vector3i.y || vector3i5.y == vector3i2.y || vector3i5.z == vector3i.z || vector3i5.z == vector3i2.z)
        {
          // ...
```

## Setup

Without proper installation, this mod will not work as expected. Using this guide should help to complete the installation properly.

If you have trouble getting things working, you can reach out to me for support via [Support](#support).

### Environment / EAC / Hosting Requirements

| Environment          | Compatible | Does EAC Need to be Disabled? | Who needs to install? |
| -------------------- | ---------- | ----------------------------- | --------------------- |
| Single Player Game   | Yes        | Yes                           | self (of course)      |
| Peer-to-Peer Hosting | No         | N/A                           | N/A                   |
| Dedicated Server     | No         | N/A                           | N/A                   |

> 🛈 If you aren't sure what some of this means, details steps are provided below to walk you through the setup process.

### Map Considerations for Installation or UnInstallation

- Does **adding** this mod require a fresh map?
  - No, you can drop this mod into an ongoing map without any trouble.
- Does **removing** this mod require a fresh map?
  - No, you can remove this mod without causing any trouble in an ongoing map.

### Windows PC (Single Player)

> 🛈 Please remember that this mod is not going to work as expected in multiplayer. It is a client-side concept mod only.

1. 📦 Download the latest release by navigating to [this link](https://github.com/jonathan-robertson/multi-dim-stability-fix/releases/latest/) and clicking the link for `multi-dim-stability-fix.zip`
2. 📂 Unzip this file to a folder named `multi-dim-stability-fix` by right-clicking it and choosing the `Extract All...` option (you will find Windows suggests extracting to a new folder named `multi-dim-stability-fix` - this is the option you want to use)
3. 🕵️ Locate and create your mods folder (if missing): in another window, paste this address into to the address bar: `%APPDATA%\7DaysToDie`, then enter your `Mods` folder by double-clicking it. If no `Mods` folder is present, you will first need to create it, then enter your `Mods` folder after that
4. 🚚 Move this new `multi-dim-stability-fix` folder into your `Mods` folder by dragging & dropping or cutting/copying & pasting, whichever you prefer
5. ♻️ Stop the game if it's currently running, then start the game again without EAC by navigating to your install folder and running `7DaysToDie.exe`
    - running from Steam or other launchers usually starts 7 Days up with the `7DaysToDie_EAC.exe` program instead, but running 7 Days directly will skip EAC startup

#### Critical Reminders

- ⚠️ it is **NECESSARY** for you to to run with EAC disabled or the DLL file in this mod will not be able to run
