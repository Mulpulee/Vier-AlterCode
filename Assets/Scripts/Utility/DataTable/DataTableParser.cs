using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using Utility.Extension;

namespace Utility.DataTable {
	public static class DataTableParser {
		public static readonly string ID = "ID";
		public static readonly string SplitRE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
		public static readonly string LineSplitRE = @"\r\n|\n\r|\n|\r";
		public static readonly char[] TrimChars = { '\"' };

		public static Dictionary<int, TRow> Read<TRow>(TextAsset asset) where TRow : DataTableRow, new() => ReadData<TRow>(asset.text);
		public static Dictionary<int, TRow> ReadData<TRow>(string data) where TRow : DataTableRow, new() {
			Type entryType = typeof(TRow);
			var result = new Dictionary<int, TRow>();
			string[] lines = Regex.Split(data, LineSplitRE);

			if (lines.Length < 2) {
				return result;
			}

			FieldInfo[] field = entryType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			string[] header = Regex.Split(lines[0], SplitRE);

			for (int i = 2; i < lines.Length; i++) {
				string[] values = Regex.Split(lines[i], SplitRE);
				if (values.Length <= 0 || values[0] == "") {
					continue;
				}

				var entry = new TRow();

				for (int j = 0; j < header.Length && j < values.Length; j++) {
					string value = values[j];
					value = value.TrimStart(TrimChars).TrimEnd(TrimChars).Replace("\\", "");

					value = value.Replace("<br>", "\n");
					value = value.Replace("<c>", ",");

					string finalValue = value;

					if (header[j].Equals(ID)) {
						try {
							entry.ID = Convert.ToInt32(finalValue);
						} catch(Exception ex) {
							Debug.Log(ex.Message);
						}
						continue;
					}

					Type type = field[j - 1].FieldType;
					object parsedValue = StringUtility.ParseToType(finalValue, type);

					field[j - 1].SetValue(entry, parsedValue);
				}
				result.Add(entry.ID, entry);
			}

			return result;
		}
		public static object ReadDynamic(string data, Type rowType) {
			MethodInfo method = typeof(DataTableParser).GetMethod("ReadData");
			MethodInfo generic = method.MakeGenericMethod(rowType);
			return generic.Invoke(null, new object[] { data });
		}
	}
}