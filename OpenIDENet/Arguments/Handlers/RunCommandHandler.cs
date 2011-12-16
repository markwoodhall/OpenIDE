using System;
using System.Linq;
using OpenIDENet.Languages;
using OpenIDENet.UI;
using OpenIDENet.CommandBuilding;
using System.Collections.Generic;
using System.IO;

namespace OpenIDENet.Arguments.Handlers
{
	class RunCommandHandler : ICommandHandler
	{
        private ICommandHandler[] _commandHandlers;

		public CommandHandlerParameter Usage {
			get {
				var usage = new CommandHandlerParameter(
					SupportedLanguage.All,
					CommandType.Run,
					Command,
					"Launches the command execution window");
				return usage;
			}
		}

		public string Command { get { return "run"; } }
		
        public RunCommandHandler(ICommandHandler[] handlers)
        {
            _commandHandlers = handlers;
        }

		public void Execute (string[] arguments, Func<string, ProviderSettings> getTypesProviderByLocation)
		{
			var form = new RunCommandForm(Directory.GetCurrentDirectory(), "", new CommandBuilder(getHandlerParameters().Cast<BaseCommandHandlerParameter>()));
			form.ShowDialog();
		}

        private IEnumerable<CommandHandlerParameter> getHandlerParameters()
        {
            var parameters = new List<CommandHandlerParameter>();
            _commandHandlers.ToList()
                .ForEach(x => 
                    {
                        var usage = x.Usage;
                        if (usage != null)
                            parameters.Add(usage);
                    });
            return parameters;
        }
	}
}
