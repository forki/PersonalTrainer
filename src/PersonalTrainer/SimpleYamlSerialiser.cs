using System;
using System.IO;
using System.Reflection;
using NLog;
using YamlDotNet.Serialization;

namespace Figroll.PersonalTrainer
{
    public class SimpleYamlSerialiser<T> where T : class
    {
        private readonly Serializer _json = new Serializer();
        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        public T Load(string filename)
        {
            var data = default(T);

            if (File.Exists(filename))
            {
                try
                {
                    using (var streamReader = new StreamReader(filename))
                    {
                        var deserializer = new Deserializer();
                        data = deserializer.Deserialize<T>(streamReader);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Error loading settings file");
                    throw;
                }
            }

            return data;
        }

        public void Save(T data, string filename)
        {
            using (var streamWriter = new StreamWriter(filename))
            {
                var serializer = new Serializer();
                serializer.Serialize(streamWriter, data);
            }
        }
    }
}