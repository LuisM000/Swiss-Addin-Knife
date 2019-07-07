using System;
using System.Collections.Generic;
using System.IO;
using ImageGenerator;

namespace SwissAddinKnife.Features.AssetsGenerator.Utils
{
    public static class ImageOutputPropertiesFactory
    {
        public static IList<ImageOutputProperties> CreateForXamarin4x(string fileExtension, string rootPath)
        {
            ImageFormat imageFormat = new ImageFormat(fileExtension);
            return new List<ImageOutputProperties>()
            {
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath,"Images","iOS")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() {Percentage = 0.75},
                    ImageName = new ImageName() {Suffix = "@3x"},
                    OverwriteFile = true
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath,"Images","iOS")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() {Percentage = 0.5},
                    ImageName = new ImageName() {Suffix = "@2x"},
                    OverwriteFile = true
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath,"Images","iOS")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() {Percentage = 0.25},
                    OverwriteFile = true
                },

                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "Images", "Android", "drawable-xxxhdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 1 },
                    OverwriteFile = true
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "Images", "Android", "drawable-xxhdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.75 },
                    OverwriteFile = true
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "Images", "Android", "drawable-xhdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.5 },
                    OverwriteFile = true
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "Images", "Android", "drawable-hdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.375 },
                    OverwriteFile = true
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "Images", "Android", "drawable-mdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.25 },
                    OverwriteFile = true
                },
                 new ImageOutputProperties()
                 {
                     FolderPath = new FolderPath()
                     {
                         IsAbsolute = true,
                         Path = Path.Combine(rootPath, "Images", "Android", "drawable")
                     },
                     ImageFormat = imageFormat,
                     ImageDimensions = new ImageDimensions() { Percentage = 0.25 },
                    OverwriteFile = true
                 },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "Images", "Android", "drawable-ldpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.1875 },
                    OverwriteFile = true
                }

            };
        }

        public static IList<ImageOutputProperties> CreateForXamarin3x(string fileExtension, string rootPath)
        {
            ImageFormat imageFormat = new ImageFormat(fileExtension);
            return new List<ImageOutputProperties>()
            {
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath,"Images","iOS")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() {Percentage = 1},
                    ImageName = new ImageName() {Suffix = "@3x"},
                    OverwriteFile = true
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath,"Images","iOS")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() {Percentage = 2d/3d},
                    ImageName = new ImageName() {Suffix = "@2x"},
                    OverwriteFile = true
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath,"Images","iOS")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() {Percentage = 1d/3d},
                    OverwriteFile = true
                },


                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "Images", "Android", "drawable-xxhdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 1 },
                    OverwriteFile = true
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "Images",  "Android","drawable-xhdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 2d/3d },
                    OverwriteFile = true
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "Images",  "Android","drawable-hdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.5 },
                    OverwriteFile = true
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "Images",  "Android","drawable-mdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 1d/3d },
                    OverwriteFile = true
                },
                 new ImageOutputProperties()
                 {
                     FolderPath = new FolderPath()
                     {
                         IsAbsolute = true,
                         Path = Path.Combine(rootPath, "Images",  "Android","drawable")
                     },
                     ImageFormat = imageFormat,
                     ImageDimensions = new ImageDimensions() { Percentage = 1d/3d },
                     OverwriteFile = true
                 },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "Images", "Android", "drawable-ldpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.25 },
                    OverwriteFile = true
                }

            };
        }

        public static IList<ImageOutputProperties> CreateForAndroid4xWithDrawable(string fileExtension, string rootPath, bool overwriteFiles)
        {
            return CreateForAndroid4x(fileExtension, rootPath, overwriteFiles, true);
        }

        public static IList<ImageOutputProperties> CreateForAndroid3xWithDrawable(string fileExtension, string rootPath, bool overwriteFiles)
        {
            return CreateForAndroid3x(fileExtension, rootPath, overwriteFiles, true);
        }

        public static IList<ImageOutputProperties> CreateForAndroid4xWithoutDrawable(string fileExtension, string rootPath, bool overwriteFiles)
        {
            return CreateForAndroid4x(fileExtension, rootPath, overwriteFiles, false);
        }

        public static IList<ImageOutputProperties> CreateForAndroid3xWithoutDrawable(string fileExtension, string rootPath, bool overwriteFiles)
        {
            return CreateForAndroid3x(fileExtension, rootPath, overwriteFiles, false);
        }

        public static IList<ImageOutputProperties> CreateForAndroid4x(string fileExtension, string rootPath,
                                                                        bool overwriteFiles, bool includeDrawableImage)
        {
            ImageFormat imageFormat = new ImageFormat(fileExtension);
            var imageOutputProperties = new List<ImageOutputProperties>()
            {
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "drawable-xxxhdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 1 },
                    OverwriteFile = overwriteFiles
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "drawable-xxhdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.75 },
                    OverwriteFile = overwriteFiles
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "drawable-xhdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.5 },
                    OverwriteFile = overwriteFiles
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "drawable-hdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.375 },
                    OverwriteFile = overwriteFiles
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "drawable-mdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.25 },
                    OverwriteFile = overwriteFiles
                },                 
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "drawable-ldpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.1875 },
                    OverwriteFile = overwriteFiles
                }
            };

            if(includeDrawableImage)
            {
                imageOutputProperties.Add(new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "drawable")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.25 },
                    OverwriteFile = overwriteFiles
                });
            }

            return imageOutputProperties;
        }

        public static IList<ImageOutputProperties> CreateForAndroid3x(string fileExtension, string rootPath,
                                                                        bool overwriteFiles, bool includeDrawableImage)
        { 
            ImageFormat imageFormat = new ImageFormat(fileExtension);
            var imageOutputProperties = new List<ImageOutputProperties>()
            {
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "drawable-xxhdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 1 },
                    OverwriteFile = overwriteFiles
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "drawable-xhdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 2d/3d },
                    OverwriteFile = overwriteFiles
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "drawable-hdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.5 },
                    OverwriteFile = overwriteFiles
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "drawable-mdpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 1d/3d },
                    OverwriteFile = overwriteFiles
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "drawable-ldpi")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 0.25 },
                    OverwriteFile = overwriteFiles
                }
            };

            if(includeDrawableImage)
            {
                imageOutputProperties.Add(new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath, "drawable")
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() { Percentage = 1d / 3d },
                    OverwriteFile = overwriteFiles
                });
            }

            return imageOutputProperties;
        }

        public static IList<ImageOutputProperties> CreateForIOS4x(string fileExtension, string rootPath, bool overwriteFiles)
        {
            ImageFormat imageFormat = new ImageFormat(fileExtension);
            return new List<ImageOutputProperties>()
            {
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath)
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() {Percentage = 0.75},
                    ImageName = new ImageName() {Suffix = "@3x"},
                    OverwriteFile = overwriteFiles
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath)
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() {Percentage = 0.5},
                    ImageName = new ImageName() {Suffix = "@2x"},
                    OverwriteFile = overwriteFiles
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath)
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() {Percentage = 0.25},
                    OverwriteFile = overwriteFiles
                }
            };
        }
        public static IList<ImageOutputProperties> CreateForIOS3x(string fileExtension, string rootPath, bool overwriteFiles)
        {
            ImageFormat imageFormat = new ImageFormat(fileExtension);
            return new List<ImageOutputProperties>()
            {
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath)
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() {Percentage = 1},
                    ImageName = new ImageName() {Suffix = "@3x"},
                    OverwriteFile = overwriteFiles
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath)
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() {Percentage = 2d/3d},
                    ImageName = new ImageName() {Suffix = "@2x"},
                    OverwriteFile = overwriteFiles
                },
                new ImageOutputProperties()
                {
                    FolderPath = new FolderPath()
                    {
                        IsAbsolute = true,
                        Path = Path.Combine(rootPath)
                    },
                    ImageFormat = imageFormat,
                    ImageDimensions = new ImageDimensions() {Percentage = 1d/3d},
                    OverwriteFile = overwriteFiles
                }
            };
        }
    }
}
