using NUnit.Framework.Constraints;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Utility.DataStructure;
using Utility.Extension.IO;

namespace Utility.DataTable.Editor {
	public static class DataTableGenerator {
		public static string AutomationFolderPath => Path.Combine(Application.dataPath, $"Scripts", $"Automation");

		public static void Generate(string nameSpace, string filename, List<string>[] values) {
#if UNITY_EDITOR
			string path = Path.Combine(AutomationFolderPath, nameSpace);

			DirectoryUtility.CreateOrNothing(AutomationFolderPath);
			DirectoryUtility.CreateOrNothing(path);

			string filePath = Path.Combine(path, filename);
			string finalPath = Path.ChangeExtension(filePath, ".cs");

			CreateGenericCode($"cs", finalPath, $"{filename}_CS.exe", values, nameSpace, filename);

			AssetDatabase.Refresh();
#endif
		}

		private static void CreateGenericCode(string provider, string sourceFilename, string assemblyName, List<string>[] values, string nameSpace, string filename) {
#if UNITY_EDITOR
			using var stream = new StreamWriter(sourceFilename, append: false);

			var textWriter = new IndentedTextWriter(stream);
			CodeDomProvider codeProvider = CodeDomProvider.CreateProvider(provider);
			CodeCompileUnit compileUnit = CreateCode(values, nameSpace, filename);

			var options = new CodeGeneratorOptions();
			codeProvider.GenerateCodeFromCompileUnit(compileUnit, stream, options);
#endif
		}

		private static CodeCompileUnit CreateCode(List<string>[] datas, string nameSpace, string filename) {
#if UNITY_EDITOR
			var compileUnit = new CodeCompileUnit();
			
			var codeNamespace = new CodeNamespace($"Automation.{nameSpace}");
			compileUnit.Namespaces.Add(codeNamespace);

			var generatedClass = new CodeTypeDeclaration();
			Type tableType = typeof(DataTableRow);
			var tableReference = new CodeTypeReference(tableType);
			generatedClass.BaseTypes.Add(tableReference);
			generatedClass.Name = $"{filename}";

			Type attributeType = typeof(SerializableAttribute);
			var atrributeReference = new CodeTypeReference(attributeType);
			var codeAttribute = new CodeAttributeDeclaration(atrributeReference);
			generatedClass.CustomAttributes.Add(codeAttribute);

			for (int i = 0; i < datas[0].Count; i++) {
				string data = datas[1][i];

				Type fieldType = Type.GetType($"System.{data}");
				if (data == "\"Dictionary<String, Single>\"") {
					fieldType = typeof(SerializedDictionary<String, Single>);
				}
				fieldType ??= Type.GetType($"{data},Assembly-CSharp");

				var field = new CodeMemberField() {
					Name = datas[0][i],
					Type = new CodeTypeReference(fieldType),
					Attributes = MemberAttributes.Public,
				};

				generatedClass.Members.Add(field);
			}

			codeNamespace.Types.Add(generatedClass);
			return compileUnit;
#endif
		}
	}
}