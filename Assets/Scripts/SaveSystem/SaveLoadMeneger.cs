using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveLoadManager
    {
        public static void Save<T>(string key, T data)
        {
            var dataSting = JsonUtility.ToJson(data, true);
            PlayerPrefs.SetString(key, dataSting);
        }

        public static T Load<T>(string key) where T : new()
        {
            if (!PlayerPrefs.HasKey(key))
            {
                var data = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<T>(data);
            }
            else
            {
                return new T();
            }
        }
    }
}
