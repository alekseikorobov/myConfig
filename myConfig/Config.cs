using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace myConfig
{
    public class Config<T> where T : class,new()
    {
        private string path;

        public T config
        {
            get; set;
        }

        public Config(string path)
        {
            this.path = path;

            config = readConfig<T>();
        }
        public Config()
        {
        }
        T readConfig<T>() where T:class,new()
        {
            if (File.Exists(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (TextReader textReader = new StreamReader(path, Encoding.Default))
                {
                    T c = (T)xmlSerializer.Deserialize(textReader);
                    return c;
                }
            }
            else
            {
                T t = new T();
                return writeConfig(t);
            }
        }
        public T writeConfig<T>(T c)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (TextWriter textWriter = new StreamWriter(path, false, Encoding.Default))
            {
                xmlSerializer.Serialize(textWriter, c);
            }
            return c;
        }
    }
}
