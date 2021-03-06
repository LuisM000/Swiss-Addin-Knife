﻿using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Pads.ProjectPad;
using MonoDevelop.Projects;
using SwissAddinKnife.Features.TextResources.Views;

namespace SwissAddinKnife.Features.TextResources.Commands
{
    public class OpenTextResourcesDialogCommand : CommandHandler
    {
        protected override void Run()
        {
            var folderPath = (ProjectFolder)IdeApp.ProjectOperations.CurrentSelectedItem;
            var window = new TextResourcesWindow(folderPath.Path)
            {
                Modal = true
            };
            window.Maximize();
            window.ShowAll();
        }

    }
}