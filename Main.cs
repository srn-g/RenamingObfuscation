using dnlib.DotNet;
using RenamingObfuscation.Classes;
using RenamingObfuscation.Interfaces;

namespace RenamingObfuscation
{
    public static class Main
    {
        public static void DoRenaming(string inPath, string outPath)
        {
            ModuleDefMD module = ModuleDefMD.Load(inPath);

            module = RenamingObfuscation(module);

            module.Write(outPath);
        }

        private static ModuleDefMD RenamingObfuscation(ModuleDefMD inModule)
        {
            ModuleDefMD module = inModule;

            IRenaming rnm = new NamespacesRenaming();

            module = rnm.Rename(module);

            rnm = new ClassesRenaming();

            module = rnm.Rename(module);

            rnm = new MethodsRenaming();

            module = rnm.Rename(module);

            rnm = new PropertiesRenaming();

            module = rnm.Rename(module);

            rnm = new FieldsRenaming();

            module = rnm.Rename(module);

            return module;
        }
    }
}
