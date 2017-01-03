using System.IO.Abstractions;

namespace PersonSearchServices.NinjectModule
{
    public class PersonSearchNinjectModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IFileSystem>().To<FileSystem>();
        }
    }
}
