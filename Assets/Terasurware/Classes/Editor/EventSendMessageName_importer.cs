using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class EventSendMessageName_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/EventSendMessageName.xls";
	private static readonly string exportPath = "Assets/Resources/EventSendMessageName.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			EventSendMessageNameData data = (EventSendMessageNameData)AssetDatabase.LoadAssetAtPath (exportPath, typeof(EventSendMessageNameData));
			if (data == null) {
				data = ScriptableObject.CreateInstance<EventSendMessageNameData> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					EventSendMessageNameData.Sheet s = new EventSendMessageNameData.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						EventSendMessageNameData.Param p = new EventSendMessageNameData.Param ();
						
					cell = row.GetCell(0); p._sendMessageName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p._sceneName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p._screenName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p._dungeonClearFlag = (cell == null ? false : cell.BooleanCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
