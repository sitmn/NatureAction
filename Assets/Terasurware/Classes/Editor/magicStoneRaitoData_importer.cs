using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class MagicStoneRaitoData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/MagicStoneRaitoData.xls";
	private static readonly string exportPath = "Assets/Resources/MagicStoneRaitoData.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			MagicStoneRaitoData data = (MagicStoneRaitoData)AssetDatabase.LoadAssetAtPath (exportPath, typeof(MagicStoneRaitoData));
			if (data == null) {
				data = ScriptableObject.CreateInstance<MagicStoneRaitoData> ();
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

					MagicStoneRaitoData.Sheet s = new MagicStoneRaitoData.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						MagicStoneRaitoData.Param p = new MagicStoneRaitoData.Param ();
						
					cell = row.GetCell(0); p.stone_name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.health_training_stone_raito = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.attack_training_stone_raito = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.defense_training_stone_raito = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.speed_training_stone_raito = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.search_training_stone_raito = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.magic_stone_raito = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.same_consume_stone_coefficient = (float)(cell == null ? 0 : cell.NumericCellValue);
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
