using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Utility.Google.Sheet {
    public static class GoogleSheetConverter {
        public static string SheetToCSV(List<List<string>> sheet) {
			var stringBuilder = new StringBuilder();

			for (int row = 0; row < sheet.Count; row++) {
				List<string> data = sheet[row];

				for (int col = 0; col < data.Count; col++) {
					string cell = data[col].ToString();
					if (cell.Contains(",")) {
						cell = $"\"{cell}\"";
					}

					if (cell.Contains("\n")) {
						cell = cell.Replace("\n","");
					}

					stringBuilder.Append(cell);

					if (col + 1 < data.Count) {
						stringBuilder.Append(",");
					}
				}

				if (row + 1 < sheet.Count) {
					stringBuilder.Append("\n");
				}
			}

			string result = stringBuilder.ToString();

			return result;
		}
	}
}