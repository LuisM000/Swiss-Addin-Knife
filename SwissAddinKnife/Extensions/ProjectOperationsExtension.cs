﻿using System;
using System.IO;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace SwissAddinKnife.Extensions
{
    public static class ProjectOperationsExtension
    {
        public static void CreateAndAddFileToProject(this Project project, FilePath filePath, string content)
        {
            File.WriteAllText(filePath, content);
            IdeApp.ProjectOperations.AddFilesToProject(project, new FilePath[] { filePath }, filePath.ParentDirectory);
        }
    }
}
