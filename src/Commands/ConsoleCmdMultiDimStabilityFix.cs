using MultiDimStabilityFix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiDimStabilityFix.Commands
{
    internal class ConsoleCmdMultiDimStabilityFix : ConsoleCmdAbstract
    {
        private static readonly ModLog<ConsoleCmdMultiDimStabilityFix> _log = new ModLog<ConsoleCmdMultiDimStabilityFix>();
        private static readonly string[] _commands = new string[] {
            "multidimstabilityfix",
            "mdsf"
        };
        private readonly string _help;

        public ConsoleCmdMultiDimStabilityFix()
        {
            var dict = new Dictionary<string, string>() {
                { "debug", "toggle debug logging mode" },
            };

            var i = 1; var j = 1;
            _help = $"Usage:\n  {string.Join("\n  ", dict.Keys.Select(command => $"{i++}. {GetCommands()[0]} {command}").ToList())}\nDescription Overview\n{string.Join("\n", dict.Values.Select(description => $"{j++}. {description}").ToList())}";
        }

        public override string[] getCommands()
        {
            return _commands;
        }

        public override string getDescription()
        {
            return "Configure or adjust settings for the Multi-Dim Stability Fix mod.";
        }

        public override string getHelp()
        {
            return _help;
        }

        public override void Execute(List<string> _params, CommandSenderInfo _senderInfo)
        {
            try
            {
                if (_params.Count == 0)
                {
                    SdtdConsole.Instance.Output($"At least 1 parameter is required; use '_help {_commands[0]}' to learn more.");
                    return;
                }

                switch (_params[0])
                {
                    case "debug":
                        ModApi.DebugMode = !ModApi.DebugMode;
                        SdtdConsole.Instance.Output($"Debug logging is now {(ModApi.DebugMode ? "enabled" : "disabled")}.");
                        _log.Info($"Debug logging has been {(ModApi.DebugMode ? "enabled" : "disabled")} by {SafelyGetSenderName(_senderInfo)}.");
                        return;
                }
                SdtdConsole.Instance.Output($"Invald request; use '_help {_commands[0]}' to learn more.");
            }
            catch (Exception e)
            {
                SdtdConsole.Instance.Output($"Exception encountered: \"{e.Message}\"\n{e.StackTrace}");
                _log.Error($"Exception encountered when {SafelyGetSenderName(_senderInfo)} tried to run 'offworldfix' console command.", e);
            }
        }

        private static string SafelyGetSenderName(CommandSenderInfo _senderInfo)
        {
            var clientInfo = _senderInfo.RemoteClientInfo;
            return clientInfo != null
                ? $"{clientInfo.playerName} ({clientInfo.entityId} // {clientInfo.CrossplatformId})"
                : "[[TELNET/CP/HOST]]";
        }
    }
}
