using System;
using System.Linq;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using SwissAddinKnife.Extensions;
using SwissAddinKnife.Features.AssetsGenerator.Views;

namespace SwissAddinKnife.Features.AssetsGenerator.Commands
{
    public class OpenImageAssetsGeneratorCommand : CommandHandler
    {
        protected override void Run()
        {
            using (var imageAssetsGenerator = new ImageAssetsGeneratorDialog(IdeApp.ProjectOperations.CurrentSelectedSolution))
            {
                imageAssetsGenerator.Run(Xwt.MessageDialog.RootWindow);
            }
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = CurrentSolutionContainsAndroidOrIOSProject();
        }

        private bool CurrentSolutionContainsAndroidOrIOSProject()
        {
            var currentSolution = IdeApp.ProjectOperations.CurrentSelectedSolution;
            return currentSolution.ContainsAnyAndroidProject() || currentSolution.ContainsAnyIOSProject();
        }
    }
}
