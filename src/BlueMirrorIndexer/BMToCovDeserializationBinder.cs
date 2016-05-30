using System;
using System.Runtime.Serialization;

namespace BlueMirrorIndexer
{
    sealed class BMToCovDeserializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName) {
            if (assemblyName.StartsWith("BlueMirrorIndexer"))
                assemblyName = System.Reflection.Assembly.GetExecutingAssembly().FullName;
            typeName = typeName.Replace("BlueMirrorIndexer, Version=", "Octopus.Covalition, Version=");
            return Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
        }
    }
}