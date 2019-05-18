using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using SwissAddinKnife.Features.JsonToClass.Views;

namespace SwissAddinKnife.Features.JsonToClass.Commands
{
    public class OpenJsonToClassGeneratorViewCommand : CommandHandler
    {
        protected override void Run()
        {
            IdeApp.Workbench.GetPad<JsonToClassGeneratorView>().BringToFront();
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = IdeApp.Workspace.IsOpen;
        }
    }
}