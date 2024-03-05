using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using Utility.ProjectSetting;

namespace Utility.Google.Sheet {
	public static class GoogleSheetDownloader {
		public struct SpreadSheetGetResult {
			public String range { get; set; }
			public String majorDimensions { get; set; }
			public List<List<String>> values { get; set; }
		}

		public static List<List<string>> DownloadSheet(string sheetName) {
			ProjectSettings settings = ProjectSettingManager.Settings;
			var geturi = $"https://sheets.googleapis.com/v4/spreadsheets/{settings.GoogleSpreadSheetID}/values/{sheetName}?key={settings.GoogleClientAPI}";
			var client = new HttpClient();

			Task<HttpResponseMessage> response = client.GetAsync(geturi);

			while (!response.IsCompleted) {
				Thread.Sleep(1);
			}

			if (!response.IsCompletedSuccessfully) {
				client.Dispose();
				throw new Exception("HTTP Get Failed");
			}

			var responseMessage = response.Result;
			Task<String> content = responseMessage.Content.ReadAsStringAsync();

			while (!content.IsCompleted) {
				Thread.Sleep(1);
			}

			if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK) {
				client.Dispose();
				throw new Exception(content.Result);
			}

			client.Dispose();

			var getResult = JsonConvert.DeserializeObject<SpreadSheetGetResult>(content.Result);

			return getResult.values;

			//var secret = new ClientSecrets {
			//	ClientId = settings.GoogleClientID,
			//	ClientSecret = settings.GoogleClientSecret
			//};

			//var scope = new string[] { SheetsService.Scope.SpreadsheetsReadonly };
			//Task<UserCredential> task = GoogleWebAuthorizationBroker.AuthorizeAsync(secret, scope, "user", CancellationToken.None);
			//UserCredential user = task.Result;

			//var clientService = new BaseClientService.Initializer() {
			//	HttpClientInitializer = user,
			//	ApplicationName = "GoogleSpreadSheetDownload"
			//};
			//var service = new SheetsService(clientService);

			//SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(settings.GoogleSpreadSheetID, sheetName);
			//IList<IList<object>> result = request.Execute().Values;

			//return result;
		}
	}
}