using Akavache.Sqlite3;
using MvvmCross;

namespace JhipsterXamarin
{
    /// <summary>
    /// Linker preserve for Akavache (https://github.com/reactiveui/Akavache#handling-xamarin-linker).
    /// Ensures the Akavache.Sqlite3 dll will not be removed by Xamarin build tools.
    /// Note: This class file is *required* for iOS to work correctly.
    /// </summary>
    [Preserve]
    public static class LinkerPreserve
    {
        static LinkerPreserve()
        {
            var persistentName = typeof(SQLitePersistentBlobCache).FullName;
            var encryptedName = typeof(SQLiteEncryptedBlobCache).FullName;
        }
    }
}
