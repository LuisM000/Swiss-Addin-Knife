using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using SwissAddinKnife.Features.AssetsInspector.Views;

namespace SwissAddinKnife.Features.AssetsInspector.Commands
{
    public class OpenAssetsAnalyzerCommand : CommandHandler
    {
        protected override void Run()
        {
            using (var imageGeneratorDialog = new AssetsAnalyzerDialog(IdeApp.ProjectOperations.CurrentSelectedSolution))
            {
                imageGeneratorDialog.Run(Xwt.MessageDialog.RootWindow);
            }
        }

    }
}