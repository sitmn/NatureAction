using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class EnemyStateData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Excel/EnemyStateData.xls";
	private static readonly string exportPath = "Assets/Excel/EnemyStateData.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			EnemyStatusData data = (EnemyStatusData)AssetDatabase.LoadAssetAtPath (exportPath, typeof(EnemyStatusData));
			if (data == null) {
				data = ScriptableObject.CreateInstance<EnemyStatusData> ();
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

					EnemyStatusData.Sheet s = new EnemyStatusData.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						EnemyStatusData.Param p = new EnemyStatusData.Param ();
						
					cell = row.GetCell(0); p.name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.max_hp = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.max_mp = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.origin_attack = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.origin_defense = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.origin_speed = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.technique = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.range = (int)(cell == null ? 0 : cell.NumericCellValue);
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
