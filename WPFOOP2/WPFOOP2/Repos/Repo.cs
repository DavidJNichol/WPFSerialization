using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WPFOOP2.Repos
{
    [Serializable]
    public class Repo
    { 
        public Model entry { get; set; }

        public string FilePath { get; set; }

        public Repo()
        {
            entry = new Model();
            FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WPFSaveFile.bin");
            
        }

        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream iostream = new FileStream(this.FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(iostream, entry);
            }
        }

        public Model Load()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            Model model = (Model)formatter.Deserialize(stream);
            stream.Close();
            return model;
        }

    }
}
