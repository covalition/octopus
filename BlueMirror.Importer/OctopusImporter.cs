using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Octopus.CDIndex;

namespace BlueMirror.Importer
{

    public class OctopusImporter
    {

        sealed class Ver1ToVer2DeserializationBinder : SerializationBinder
        {

            public override Type BindToType(string assemblyName, string typeName) {
                // Type typeToDeserialize = null;
                if (assemblyName.StartsWith("Octopus.CDIndex")) {
                    assemblyName = System.Reflection.Assembly.GetExecutingAssembly().FullName;                     }
                typeName = typeName.Replace("Octopus.CDIndex, Version=", "BlueMirror.Importer, Version=");
                return Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
                // return typeToDeserialize;
            }
            
        }


        public CdInDatabaseList Deserialize(string filePath) {
            CdInDatabaseList cid = null;
            //try {
                Stream stream = new FileStream(filePath, FileMode.Open);
                try {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Binder = new Ver1ToVer2DeserializationBinder();
                    cid = (CdInDatabaseList)formatter.Deserialize(stream);
                }
                finally {
                    stream.Close();
                }
            //}
            //catch (Exception e) {
            //    Debug.WriteLine(e.Message);
            //}
            return cid;
        }

    }
}
