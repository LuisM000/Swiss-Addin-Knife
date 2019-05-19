using System;
using MonoDevelop.Components.Commands;
using SwissAddinKnife.Features.AssetsGenerator.Views;

namespace SwissAddinKnife.Features.AssetsGenerator.Commands
{
    public class OpenGeneratorCommand : CommandHandler
    {
        protected override void Run()
        {
            using (var imageGeneratorDialog = new ImageGeneratorDialog())
            {
                imageGeneratorDialog.Run(Xwt.MessageDialog.RootWindow);
            }
        }

    }
}