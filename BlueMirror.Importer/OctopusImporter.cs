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
            public override Type BindToType(
               string assemblyName, string typeName) {

                Type typeToDeserialize = null;
                //Octopus.CDIndex, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
                // For each assemblyName/typeName that you wish to deserialize
                // to a different type, set typeToCreate to the desired type
                //String assemVer1 = "MyAssem, Version=1.0.0.0, " +
                //   "Culture=neutral, PublicKeyToken=b77a5c561934e089";
                //String typeVer1 = "Foo";
                // typename: System.Collections.Generic.List`1[[Octopus.CDIndex.FileInDatabase, Octopus.CDIndex, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null]]
                // if (assemblyName.StartsWith("Octopus.CDIndex") == assemVer1 && typeName == typeVer1) {
                if (assemblyName.StartsWith("Octopus.CDIndex")) {
                    assemblyName = System.Reflection.Assembly.GetExecutingAssembly().FullName;                
                }
                typeName = typeName.Replace("Octopus.CDIndex, Version=", "BlueMirror.Importer, Version=");
                //int start = typeName.IndexOf("Octopus.CDIndex, Version=");
                //if (start > -1) {
                //    const string END_NAME_OF_TYPE = "Culture=neutral, PublicKeyToken=null";
                //    int end = typeName.IndexOf(END_NAME_OF_TYPE);
                //    if (end > -1) {
                //        end += END_NAME_OF_TYPE.Length;
                //        typeName.re
                //    }
                //}

                // To return the type, do this:
                typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));

                return typeToDeserialize;
            }

            
        }


        public CdInDatabaseList Deserialize(string filePath) {
            CdInDatabaseList cid = null;
            try {
                Stream stream = new FileStream(filePath, FileMode.Open);
                try {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Binder = new Ver1ToVer2DeserializationBinder();
                    cid = (CdInDatabaseList)formatter.Deserialize(stream);
                }
                finally {
                    stream.Close();
                }
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }
            return cid;
        }

    }
}
