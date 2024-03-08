using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Utility.Extension.IO;
using Utility.Google.Sheet;
using Utility.ProjectSetting;

namespace Utility.DataTable.Editor {
	public static class AutoTableGenerator {
		private static readonly string AssetFolderName = $"DataTableAsset";
		private static readonly string AssetPath = Path.Combine(Application.dataPath, $"Resources", AssetFolderName);

		[MenuItem("Automation/Generate/DownloadTable")]
		public static void Generate() {
			var tableNames = new List<string>();
			List<KeyValuePair<string, string>> tableDatas = DownloadTables();
			Dictionary<string, List<string>[]> columnByTable = GetTableColumns(tableDatas);

			DirectoryUtility.CreateOrNothing(AssetPath);

			int count = 0;
			foreach (KeyValuePair<string, List<string>[]> item in columnByTable) {
				string typename = $"{item.Key}Row";
				DataTableGenerator.Generate($"DataTable", typename, item.Value);

				Type generatedType = Type.GetType($"Automation.DataTable.{typename},Assembly-CSharp");
				DataTableAssetGenerator.Generate($"DataTableAsset", $"{item.Key}Asset", generatedType);
				
				object table = DataTableParser.ReadDynamic(tableDatas[count].Value, generatedType);
				Type constructedType = typeof(DataTable<>).MakeGenericType(generatedType);

				object dataTable = Activator.CreateInstance(constructedType, table);

				Type assetType = Type.GetType($"{item.Key}Asset,Assembly-CSharp");
				GenerateAsset(assetType, $"{item.Key}Asset", dataTable);

				tableNames.Add(item.Key);

				count++;
			}
		}

		private static List<KeyValuePair<string, string>> DownloadTables() {
			var tables = new List<KeyValuePair<string, string>>();

			for (int i = 0; i < ProjectSettingManager.Settings.SheetNames.Count; i++) {
				List<List<string>> sheet = GoogleSheetDownloader.DownloadSheet(ProjectSettingManager.Settings.SheetNames[i]);
				string csv = GoogleSheetConverter.SheetToCSV(sheet);
				var table = new KeyValuePair<string, string>(ProjectSettingManager.Settings.SheetNames[i], csv);
				
				tables.Add(table);
			}

			return tables;
		}
		private static Dictionary<string, List<string>[]> GetTableColumns(List<KeyValuePair<string, string>> datas) {
			var tableColumns = new Dictionary<string, List<string>[]>();
			
			for (int i = 0; i < datas.Count; i++) {
				string[] lines = Regex.Split(datas[i].Value, DataTableParser.LineSplitRE);

				var header = new List<string>(Regex.Split(lines[0], DataTableParser.SplitRE));
				var types = new List<string>(Regex.Split(lines[1], DataTableParser.SplitRE));

				if (!header.Contains(DataTableParser.ID)) {
					continue;
				}

				int index = header.IndexOf(DataTableParser.ID);
				header.Remove(DataTableParser.ID);
				types.RemoveAt(index);

				var item = new List<string>[2] { header, types };
				tableColumns.Add(datas[i].Key, item);
			}

			return tableColumns;
		}

		private static void GenerateAsset(Type type, string filename, object tableValue) {
			string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(Path.Combine($"Assets", $"Resources", AssetFolderName, $"{filename}.asset"));

			var check = Resources.Load(Path.Combine(AssetFolderName, filename));
			if (check != null) {
				type.GetField("Table").SetValue(check, tableValue);
				EditorUtility.SetDirty(check);
				return;
			}

			ScriptableObject asset = ScriptableObject.CreateInstance(type);
			type.GetField("Table").SetValue(asset, tableValue);
			EditorUtility.SetDirty(asset);

			AssetDatabase.CreateAsset(asset, assetPathAndName);
		}
	}
}