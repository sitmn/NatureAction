using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class InitialPlayerStatusData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/InitialPlayerStatusData.xls";
	private static readonly string exportPath = "Assets/Resources/InitialPlayerStatusData.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			InitialPlayerStatusData data = (InitialPlayerStatusData)AssetDatabase.LoadAssetAtPath (exportPath, typeof(InitialPlayerStatusData));
			if (data == null) {
				data = ScriptableObject.CreateInstance<InitialPlayerStatusData> ();
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

					InitialPlayerStatusData.Sheet s = new InitialPlayerStatusData.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						InitialPlayerStatusData.Param p = new InitialPlayerStatusData.Param ();
						
					cell = row.GetCell(0); p.対象プレイヤー = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p._initialHealth = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p._initialHp = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p._initialMp = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p._initialAttack = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p._initialDefense = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p._initialSpeed = (int)(cell == null ? 0 : cell.NumericCellValue);
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
