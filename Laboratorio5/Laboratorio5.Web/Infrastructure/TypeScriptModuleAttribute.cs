using System;

namespace Laboratorio5.Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public sealed class TypeScriptModuleAttribute : Attribute
    {
        private readonly string moduleName;
        public TypeScriptModuleAttribute(string moduleName)
        {
            this.moduleName = moduleName;
        }
    }
}
