using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class HighwayName_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/HighwayName.xls";
	private static readonly string exportPath = "Assets/Resources/HighwayName.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			HighWayNameData data = (HighWayNameData)AssetDatabase.LoadAssetAtPath (exportPath, typeof(HighWayNameData));
			if (data == null) {
				data = ScriptableObject.CreateInstance<HighWayNameData> ();
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

					HighWayNameData.Sheet s = new HighWayNameData.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						HighWayNameData.Param p = new HighWayNameData.Param ();
						
					cell = row.GetCell(1); p._sceneName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p._townName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p._townGateName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(4); p._startPosX = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p._startPosZ = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p._startPosY = (float)(cell == null ? 0 : cell.NumericCellValue);
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
