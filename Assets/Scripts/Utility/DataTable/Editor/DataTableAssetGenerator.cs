using System;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Utility.Extension.IO;

namespace Utility.DataTable.Editor {
	public static class DataTableAssetGenerator {
		public static string DataTableAssetPath => Path.Combine(DataTableGenerator.AutomationFolderPath, $"DataTableAsset");

		public static void Generate(string nameSpace, string filename, Type value) {
#if UNITY_EDITOR
			string path = Path.Combine(DataTableAssetPath, nameSpace);

			DirectoryUtility.CreateOrNothing(DataTableGenerator.AutomationFolderPath);
			DirectoryUtility.CreateOrNothing(path);

			string filePath = Path.Combine(DataTableAssetPath, filename);
			string finalPath = Path.ChangeExtension(filePath, ".cs");

			CreateGenericCode($"cs", finalPath, $"{filename}_CS.exe", value, nameSpace, filename);

			AssetDatabase.Refresh();
#endif
		}

		private static void CreateGenericCode(string provider, string sourceFilename, string assemblyName, Type typeValue, string nameSpace, string filename) {
#if UNITY_EDITOR
			using var stream = new StreamWriter(sourceFilename, append: false);

			var textWriter = new IndentedTextWriter(stream);
			CodeDomProvider codeProvider = CodeDomProvider.CreateProvider(provider);
			CodeCompileUnit compileUnit = CreateCode(typeValue, nameSpace, filename);

			var options = new CodeGeneratorOptions();
			codeProvider.GenerateCodeFromCompileUnit(compileUnit, stream, options);
#endif
		}

		private static CodeCompileUnit CreateCode(Type typeData, string nameSpace, string filename) {
#if UNITY_EDITOR
			var compileUnit = new CodeCompileUnit();

			var codeNamespace = new CodeNamespace("");
			compileUnit.Namespaces.Add(codeNamespace);

			CodeTypeDeclaration generatedClass = new CodeTypeDeclaration();

			var assetType = typeof(DataTableAsset<>);
			var baseType = new CodeTypeReference(assetType);
			baseType.TypeArguments.Add(typeData);

			generatedClass.BaseTypes.Add(baseType);
			generatedClass.Name = $"{filename}";

			codeNamespace.Types.Add(generatedClass);
			return compileUnit;
#endif
		}
	}
}