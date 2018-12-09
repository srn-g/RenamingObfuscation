using dnlib.DotNet;

namespace RenamingObfuscation.Interfaces
{
    public interface IRenaming
    {
        ModuleDefMD Rename(ModuleDefMD module);
    }
}
