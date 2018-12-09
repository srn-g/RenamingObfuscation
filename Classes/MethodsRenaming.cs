using dnlib.DotNet;
using RenamingObfuscation.Interfaces;

namespace RenamingObfuscation.Classes
{
    public class MethodsRenaming : IRenaming
    {
        public ModuleDefMD Rename(ModuleDefMD module)
        {
            ModuleDefMD moduleToRename = module;

            foreach (TypeDef type in moduleToRename.GetTypes())
            {
                if (type.IsGlobalModuleType)
                    continue;

                if (type.Name == "GeneratedInternalTypeHelper")
                    continue;

                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody)
                        continue;

                    if (method.Name == ".ctor" || method.Name == ".cctor")
                        continue;

                    method.Name = Utils.GenerateRandomString();
                }
            }

            return moduleToRename;
        }
    }
}
