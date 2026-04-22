using Yuque.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Infrastructure.FileStroage
{
    public interface IFileStorageFactory
    {
        IFileStorage GetStorage(FileStorageType storageType);

        IFileStorage GetStorage();
    }
}
