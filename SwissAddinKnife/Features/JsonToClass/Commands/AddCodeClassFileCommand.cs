using System.IO;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Pads.ProjectPad;
using SwissAddinKnife.Extensions;
using SwissAddinKnife.Features.JsonToClass.Exceptions;
using SwissAddinKnife.Features.JsonToClass.Services;
using SwissAddinKnife.Features.JsonToClass.Views;
using Xwt;


namespace SwissAddinKnife.Features.JsonToClass.Commands
{
    public class AddCodeClassFileCommand : CommandHandler
    {
        const string ClassOutputName = "Empty.cs";

        protected override void Run()
        {
            try
            {
                var projectFolder = (ProjectFolder)IdeApp.ProjectOperations.CurrentSelectedItem;
                string json = Clipboard.GetText();

                var filePath = new FilePath(Path.Combine(projectFolder.Path, ClassOutputName));
                string classCode = JsonToClassService.GenerateClassCodeFromJson(json);

                IdeApp.ProjectOperations.CurrentSelectedProject.CreateAndAddFileToProject(filePath, classCode);
            }
            catch (UninstalledQuicktypeException)
            {
                MessageDialog.ShowWarning("Ouch!", "Something has happened. You may not have QuickType installed");
            }
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = IdeApp.Workspace.IsOpen &&
                !string.IsNullOrEmpty(Clipboard.GetText()) &&
                IdeApp.ProjectOperations.CurrentSelectedItem is ProjectFolder;
        }

    }
}