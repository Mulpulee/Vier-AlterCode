using System;
using System.Text;
using UnityEngine;

namespace Utility.Extension {
    public static class EncryptionUtility {
        public static string ToEncrytedString<T>(T @object) {
            string json = JsonUtility.ToJson(@object);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            string encrytedJson = Convert.ToBase64String(bytes);

            return encrytedJson;
        }

        public static T FromEncrytedString<T>(string encrytedString) {
            byte[] bytes = Convert.FromBase64String(encrytedString);
            string json = Encoding.UTF8.GetString(bytes);

            T @object = JsonUtility.FromJson<T>(json);

            return @object;
        }
    }
}