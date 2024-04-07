using Newtonsoft.Json;

namespace NetCoreSession.ExtensionMethods
{
    public static class SessionExtension
    {
        //Bir metodun Extension method olabilmesi icin ilk parametresini cok özel alması gerekir...Bu ilk parametre verilirken this keyword'u ile baslanmalıdır...Ve 
        public static void SetObject(this ISession session, string key, object value)
        {
            string serializedObject = JsonConvert.SerializeObject(value);
            session.SetString(key, serializedObject);
        }

        public static T GetObject<T>(this ISession session, string key) where T : class
        {
            string serializedObject = session.GetString(key);
            if (string.IsNullOrEmpty(serializedObject))
            {
                return null;
            }
            T deserializedObject = JsonConvert.DeserializeObject<T>(serializedObject);
            return deserializedObject;
        }
    }
}
