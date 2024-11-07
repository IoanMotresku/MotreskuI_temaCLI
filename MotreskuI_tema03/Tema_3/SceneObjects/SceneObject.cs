using System.IO;
using System.Xml.Serialization;

namespace Tema_3
{
    public class SceneObject<T>
    {
        public void SerializeXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());

            using (FileStream file = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                serializer.Serialize(file, this);
            }
        }

        public T DeserializeXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (FileStream file = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                return (T)serializer.Deserialize(file);
            }
        }
    }
}
