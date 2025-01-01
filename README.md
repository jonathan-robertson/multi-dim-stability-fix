# Multi-Dim Stability Fix

[![🧪 Tested with 7DTD 1.2 (b27)](https://img.shields.io/badge/🧪%20Tested%20with-7DTD%201.2%20(b27)-blue.svg)](https://7daystodie.com/)
[![✅ Single Player Supported](https://img.shields.io/badge/✅%20Single%20Player-Supported-blue.svg)](https://7daystodie.com/)
[![❌ Dedicated Servers and P2P Unsupported](https://img.shields.io/badge/❌%20Dedicated%20Servers%20and%20P2P-Unsupported-blue.svg)](https://7daystodie.com/)
[![📦 Automated Release](https://github.com/jonathan-robertson/multi-dim-stability-fix/actions/workflows/release.yml/badge.svg)](https://github.com/jonathan-robertson/multi-dim-stability-fix/actions/workflows/release.yml)

## Summary

7 Days to Die mod: Provides a fix for the multi-dim stability exploit allowing players to detect underground tunnels or bases by holding a multi-dim block and hovering the placement ghost frame over that piece of terrain.

### Support

🗪 If you would like support for this mod, please feel free to reach out via [Discord](https://discord.gg/hYa2sNHXya).

## Features

TODO

### Admin Commands

> ℹ️ You can always search for this command or any command by running:
>
> - `help * <partial or complete command name>`
> - or get details about this (or any) command and its options by running `help <command>`

| primary | alternate |     params     | description                                                     |
| :-----: | :-------: | :------------: | --------------------------------------------------------------- |
|  TODO   |   TODO    | `debug` / `dm` | enable/disable debug logging for this mod (disabled by default) |

*Note that leaving debug mode on can have a negative impact on performance. It is therefore recommended to only turn it on while troubleshooting and then disable it afterwards.*

## Setup

Without proper installation, this mod will not work as expected. Using this guide should help to complete the installation properly.

If you have trouble getting things working, you can reach out to me for support via [Support](#support).

### Environment / EAC / Hosting Requirements

| Environment          | Compatible | Does EAC Need to be Disabled? | Who needs to install? |
| -------------------- | ---------- | ----------------------------- | --------------------- |
| Dedicated Server     | No         | N/A                           | N/A                   |
| Peer-to-Peer Hosting | No         | N/A                           | N/A                   |
| Single Player Game   | Yes        | Yes                           | self (of course)      |

> 🤔 If you aren't sure what some of this means, details steps are provided below to walk you through the setup process.

### Map Considerations for Installation or UnInstallation

- Does **adding** this mod require a fresh map?
  - No, you can drop this mod into an ongoing map without any trouble.
- Does **removing** this mod require a fresh map?
  - No, you can remove this mod without causing any trouble in an ongoing map.

### Windows PC (Single Player)

> ℹ️ If you plan to host a multiplayer game, only the host PC will need to install this mod. Other players connecting to your session do not need to install anything for this mod to work 😉

1. 📦 Download the latest release by navigating to [this link](https://github.com/jonathan-robertson/multi-dim-stability-fix/releases/latest/) and clicking the link for `multi-dim-stability-fix.zip`
2. 📂 Unzip this file to a folder named `multi-dim-stability-fix` by right-clicking it and choosing the `Extract All...` option (you will find Windows suggests extracting to a new folder named `multi-dim-stability-fix` - this is the option you want to use)
3. 🕵️ Locate and create your mods folder (if missing): in another window, paste this address into to the address bar: `%APPDATA%\7DaysToDie`, then enter your `Mods` folder by double-clicking it. If no `Mods` folder is present, you will first need to create it, then enter your `Mods` folder after that
4. 🚚 Move this new `multi-dim-stability-fix` folder into your `Mods` folder by dragging & dropping or cutting/copying & pasting, whichever you prefer
5. ♻️ Stop the game if it's currently running, then start the game again without EAC by navigating to your install folder and running `7DaysToDie.exe`
    - running from Steam or other launchers usually starts 7 Days up with the `7DaysToDie_EAC.exe` program instead, but running 7 Days directly will skip EAC startup

#### Critical Reminders

- ⚠️ it is **NECESSARY** for the host to run with EAC disabled or the DLL file in this mod will not be able to run
- 😉 other players **DO NOT** need to disable EAC in order to connect to your game session, so you don't need to walk them through these steps
- 🔑 it is also **HIGHLY RECOMMENDED** to add a password to your game session
  - while disabling EAC is 100% necessary (for P2P or single player) to run this mod properly, it also allows other players to run any mods they want on their end (which could be used to gain access to admin commands and/or grief you or your other players)
  - please note that *dedicated servers* do not have this limitation and can have EAC fully enabled; we have setup guides for dedicated servers as well, listed in the next 2 sections: [Windows/Linux Installation (Server via FTP from Windows PC)](#windowslinux-installation-server-via-ftp-from-windows-pc) and [Linux Server Installation (Server via SSH)](#linux-server-installation-server-via-ssh)
