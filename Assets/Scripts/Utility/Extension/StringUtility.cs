using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Utility.DataStructure;

namespace Utility.Extension {
	public static class StringUtility {
		public static object ParseToType(string value, Type type) {
			if (type == typeof(int))
				return Convert.ToInt32(value);
			else if (type == typeof(char))
				return Convert.ToInt16(value);
			else if (type == typeof(float))
				return Convert.ToSingle(value);
			else if (type == typeof(byte))
				return Convert.ToByte(value);
			else if (type == typeof(sbyte))
				return Convert.ToSByte(value);
			else if (type == typeof(bool))
				return Convert.ToBoolean(value);
			else if (type == typeof(string))
				return value;
			else if (type == typeof(SerializedDictionary<string, float>)) {
				string[] items = value.Split(';');

				var dictionary = new SerializedDictionary<string, float>();
				foreach (string item in items) {
					if (item != "") {
						string curt = string.Concat(item.Where((c) => !Char.IsWhiteSpace(c)));
						string[] values = curt.Split('=');

						try {
							dictionary.Add(values[0], Convert.ToSingle(values[1]));
						} catch (Exception e) {
							Debug.Log(e.Message);
						}
					}
				}

				return dictionary;
			}
			else if (type.IsEnum)
				return Enum.Parse(type, value);
			else {
				return null;
			}
		}
	}
}