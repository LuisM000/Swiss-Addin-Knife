using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using SwissAddinKnife.Features.JsonToClass.Exceptions;
using SwissAddinKnife.Features.JsonToClass.Services;
using Xwt;

namespace SwissAddinKnife.Features.JsonToClass.Commands
{
    public class CreateCodeClassFromJsonCommand : CommandHandler
    {
        protected override void Run()
        {
            try
            {
                string json = Clipboard.GetText();
                string classCode = JsonToClassService.GenerateClassCodeFromJson(json);

                IdeApp.Workbench.ActiveDocument.Editor.InsertAtCaret(classCode);
            }
            catch (UninstalledQuicktypeException)
            {
                MessageDialog.ShowWarning("Ouch!", "Something has happened. You may not have QuickType installed");
            }
        }

        protected override void Update(CommandInfo info)
        {
            info.Enabled = IdeApp.Workbench.ActiveDocument?.Editor != null &&
                            !string.IsNullOrEmpty(Clipboard.GetText());
        }
    }
}