using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class TrainingPointData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/TrainingPointData.xls";
	private static readonly string exportPath = "Assets/Resources/TrainingPointData.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			TrainingPointData data = (TrainingPointData)AssetDatabase.LoadAssetAtPath (exportPath, typeof(TrainingPointData));
			if (data == null) {
				data = ScriptableObject.CreateInstance<TrainingPointData> ();
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

					TrainingPointData.Sheet s = new TrainingPointData.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						TrainingPointData.Param p = new TrainingPointData.Param ();
						
					cell = row.GetCell(0); p.trainingNo = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p._trainingName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p._tired = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p._hpBase = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p._mpBase = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p._attackBase = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p._defenseBase = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p._speedBase = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p._purpleStoneConsumeRaito = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p._redStoneConsumeRaito = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(10); p._blueStoneConsumeRaito = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(11); p._greenStoneConsumeRaito = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(12); p._yellowStoneConsumeRaito = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(13); p._magicStoneRaito = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(14); p._sameConsumeStoneCoefficient = (float)(cell == null ? 0 : cell.NumericCellValue);
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
