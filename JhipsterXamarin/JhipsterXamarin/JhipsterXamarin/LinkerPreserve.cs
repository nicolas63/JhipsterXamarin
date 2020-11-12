using System;
using System.Collections.Generic;
using System.Text;
using Akavache.Sqlite3;

namespace JhipsterXamarin
{
    public static class LinkerPreserve
    {
        static LinkerPreserve()
        {
            var persistentName = typeof(SQLitePersistentBlobCache).FullName;
            var encryptedName = typeof(SQLiteEncryptedBlobCache).FullName;
        }
    }
}
