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
        private CdInDatabaseList deserialize(string filePath) {
            CdInDatabaseList cid = null;
            try {
                Stream stream = new FileStream(filePath, FileMode.Open);
                try {
                    IFormatter formatter = new BinaryFormatter();
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
