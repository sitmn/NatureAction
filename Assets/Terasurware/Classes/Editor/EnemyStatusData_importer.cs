using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class EnemyStatusData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/EnemyStatusData.xls";
	private static readonly string exportPath = "Assets/Resources/EnemyStatusData.asset";
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
						
					cell = row.GetCell(0); p._enemyName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p._enemyMaxHp = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p._enemyAttack = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p._enemyDefense = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p._enemySpeed = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p._enemyDistance = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p._enemyAttackDurationTime = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p._enemyAttackEndTime = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p._enemyAttackSpeed = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p._enemyAttackRange = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(10); p._enemySearchRange = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(11); p._dropPurpleStone = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(12); p._dropRedStone = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(13); p._dropBlueStone = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(14); p._dropGreenStone = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(15); p._dropYellowStone = (int)(cell == null ? 0 : cell.NumericCellValue);
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
