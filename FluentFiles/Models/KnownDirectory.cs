using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FluentFiles.Models
{
    public class KnownDirectory
    {
        public static readonly KnownDirectory Desktop = new KnownDirectory("Desktop", "\uE7F9", UserDataPaths.GetDefault().Desktop);
        public static readonly KnownDirectory Downloads = new KnownDirectory("Downloads", "\uE896", UserDataPaths.GetDefault().Downloads);
        public static readonly KnownDirectory Documents = new KnownDirectory("Documents", "\uF000", KnownFolders.DocumentsLibrary);
        public static readonly KnownDirectory Music = new KnownDirectory("Music", "\uE8D6", KnownFolders.MusicLibrary);
        public static readonly KnownDirectory Pictures = new KnownDirectory("Pictures", "\uEB9F", KnownFolders.PicturesLibrary);
        public static readonly KnownDirectory Videos = new KnownDirectory("Videos", "\uE714", KnownFolders.VideosLibrary);

        public string DisplayName { get; private set; }

        public string Icon { get; private set; }

        public IStorageFolder Folder { get; private set; }

        public KnownDirectory(string displayName, string icon, IStorageFolder folder)
        {
            DisplayName = displayName;
            Icon = icon;
            Folder = folder;
        }

        public KnownDirectory(string displayName, string icon, string path)
        {
            DisplayName = displayName;
            Icon = icon;
            Folder = StorageFolder.GetFolderFromPathAsync(path).AsTask().Result;
        }

    }
}
