using Newtonsoft.Json;
using System.Collections.Generic;

namespace Assets.CourseGame.Develop.CommonServices.DataManagement
{
    public class JsonSerializer : IDataSerializer
    {
        public TData Deserialize<TData>(string serializedData)
        {
            return JsonConvert.DeserializeObject<TData>(serializedData, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }

        public string Serialize<TData>(TData data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto
            });
        }
    }

    public class EnemyDatas : ISaveData
    {
        List<EnemyData> enemyDatas;
    }

    public abstract class EnemyData
    {
        public int Level;
    }

    public abstract class GolemEnemyData
    {
        public string AbilityName;
    }

    public abstract class BehilderEnemyyData
    {
        public int Damage;
    }
}
