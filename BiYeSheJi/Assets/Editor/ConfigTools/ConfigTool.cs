using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;
using Newtonsoft.Json;
using OfficeOpenXml;

public class ConfigTool : ScriptableObject
{

    private string m_TableItemCodeTemplete = @"public class __FileName__Item : TableItem
{
    __AttribCode__
}";

    private string m_ItemCodeTemplete = @"public class __FileName__Item : Item
{
    __AttribCode__
}";


    private string m_TableCodeTemplete = @"public class __FileName__Table : Table<__FileName__Item>
{
}";

    private string m_TableAttriTemplete;
    private string m_TableMgrTemplete = @" using System;using System.Collections.Generic;using UnityEngine;
class TableManager
{
__AttribCode__
}";


    public string excelPath;
    public string codePath;

    public void Generate()
    {
        m_TableAttriTemplete = File.ReadAllText("Assets/Editor/Database/TableMgrTemplete.templete");
        List<string> fileNameList = PathUtile.GetPathFileNames(Application.dataPath + "/" + excelPath);
        GenerateByExcel(fileNameList);
    }

    private void GenerateByExcel(List<string> fileNameList)
    {
        string configTableCode = "using System;\nusing System.Collections.Generic;\nusing UnityEngine;\n";
        string tableMgrAttris = "";


        for (int i = 0; i < fileNameList.Count; i++)
        {
            string fileName = fileNameList[i];
            string filePath = Application.dataPath + "/" + excelPath + "/" + fileName + ".xlsx";
            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                // 默认excel文件的第一个表格数据
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];

                // 生成json文件
                GenerateJsonFile(fileName, worksheet);

                // 生成一个Excel对应的ConfigTable代码
                string tableCode = GenerateTableCode(fileName, worksheet);
                configTableCode += tableCode;

                // 生成一个Excel对应的TableManager代码
                string tableMgrAttri = GenerateTableMgrCode(fileName);
                tableMgrAttris += tableMgrAttri;
            }
        }

        // 将ConfigTable的代码写入文件
        string codeFilePath = Application.dataPath + "/" + codePath + "/" + "ConfigsTable.cs";
        File.WriteAllText(codeFilePath, configTableCode);

        // 将TableManager的代码写入文件
        string tableMgrCodes = m_TableMgrTemplete.Replace("__AttribCode__", tableMgrAttris);

        string tableMgrPath = Application.dataPath + "/" + codePath + "/" + "TableManager.cs";
        File.WriteAllText(tableMgrPath, tableMgrCodes);
    }

    private void GenerateJsonFile(string fileName, ExcelWorksheet worksheet)
    {
        List<Dictionary<string, object>> jsonData = new List<Dictionary<string, object>>();
        int startRow = GetExcelStartRow(worksheet);
        // 获取类型行的信息
        List<string> typeList = GetRowInfo(worksheet, startRow);
        // 获取名字信息行
        List<string> nameList = GetRowInfo(worksheet, startRow + 1);
        startRow += 2;

        for (int i = startRow; i <= worksheet.Dimension.End.Row; i++)
        {
            Dictionary<string, object> rowInfo = new Dictionary<string, object>();
            for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
            {
                ExcelRange cell = worksheet.Cells[i, j];
                if (null == cell.Value)
                {
                    continue;
                }
                string cellValue = cell.Value.ToString();
                string typeStr = typeList[j - 1];
                string nameStr = nameList[j - 1];
                switch (typeStr)
                {
                    case "int":
                        rowInfo.Add(nameStr, int.Parse(cellValue));
                        break;
                    case "float":
                        rowInfo.Add(nameStr, float.Parse(cellValue));
                        break;
                    case "bool":
                        rowInfo.Add(nameStr, bool.Parse(cellValue));
                        break;
                    case "array":
                        List<int> list = JsonConvert.DeserializeObject<List<int>>(cellValue);
                        rowInfo.Add(nameStr, list);
                        break;
                    case "ItemType":
                        ItemType itemType = (ItemType)System.Enum.Parse(typeof(ItemType), cellValue);
                        rowInfo.Add(nameStr,itemType);
                        break;
                    case "BuffType":
                        BuffType buffType = (BuffType)System.Enum.Parse(typeof(BuffType), cellValue);
                        rowInfo.Add(nameStr, buffType);
                        break;
                    default:
                        rowInfo.Add(nameStr, cellValue);
                        break;
                }
            }
            jsonData.Add(rowInfo);
        }
        string content = JsonConvert.SerializeObject(jsonData);
        string path = "Assets/Resources/Configs/" + fileName + ".json";
        File.WriteAllText(path, content);
    }

    // 生成TableManager的代码
    private string GenerateTableMgrCode(string fileName)
    {
        string attribCode = m_TableAttriTemplete.Replace("__FileName__", fileName);
        attribCode = attribCode.Replace("__fileName__", fileName.ToLower());
        return attribCode;
    }

    /// <summary>
    /// 保存配置表
    /// </summary>
    public void Save()
    {
        // 将当前对象设置为Dirty
        EditorUtility.SetDirty(this);
        // 保存所有(被标记为Dirty)资源
        AssetDatabase.SaveAssets();
        // 刷新
        AssetDatabase.Refresh();
    }

    // 获取Excel表格信息的开始行,去除空行和#开头的注释行
    private int GetExcelStartRow(ExcelWorksheet worksheet)
    {
        for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
        {
            var cell = worksheet.Cells[i, 1];
            if (null == cell.Value)
                continue;

            string value = cell.Value.ToString();
            if (string.IsNullOrEmpty(value) || value.StartsWith("#"))
                continue;

            return i;
        }
        return -1;
    }

    // 生成一个文件对应的ConfigTable代码
    private string GenerateTableCode(string fileName, ExcelWorksheet worksheet)
    {
        int startRow = GetExcelStartRow(worksheet);

        // 获取类型行的信息
        List<string> typeList = GetRowInfo(worksheet, startRow);
        // 获取名字信息行
        List<string> nameList = GetRowInfo(worksheet, startRow + 1);
        //获取对应类型和属性行的消息
        List<string> infoList = GetRowInfo(worksheet, startRow + 2);
        //获取Item类的所有字段的名字
        List<string> itemFieldNameList = GetItemFieldInfo();

        // Item属性的定义
        StringBuilder sb = new StringBuilder();
        string attribFormat = "public {0} {1};\n\t\t";
        int fatherClassIndex = -1 ;
        for (int i = 1; i < typeList.Count; i++)
        {
            bool sameField=false;
            for(int j=0;j<itemFieldNameList.Count;j++)
            {
                if(itemFieldNameList[j]== nameList[i])
                {
                    sameField = true;
                    break;
                }
            }
            if(sameField)
            {
                continue;
            }
            if (nameList[i] == "fatherClass")
            {
                fatherClassIndex = i;
            }        
            string typeStr = typeList[i];
            string nameStr = nameList[i];
            typeStr = (typeStr == "array") ? "List<int>" : typeStr;
            string attrib = string.Format(attribFormat, typeStr, nameStr);
            sb.Append(attrib);
        }
        string itemCode;
        if(fatherClassIndex==-1 || infoList[fatherClassIndex] == "TableItem")
        {
            itemCode = m_TableItemCodeTemplete;
        }
        else
        {
            itemCode= m_ItemCodeTemplete;
        }
        itemCode = itemCode.Replace("__FileName__", fileName);
        itemCode = itemCode.Replace("__AttribCode__", sb.ToString());

        string tableCode = m_TableCodeTemplete;
        tableCode = tableCode.Replace("__FileName__", fileName);
        string codeStr = itemCode + "\n" + tableCode + "\n";

        return codeStr;
    }

    private List<string> GetRowInfo(ExcelWorksheet worksheet, int rowIndex)
    {
        List<string> rowInfoList = new List<string>();
        for (int i = 1; i <= worksheet.Dimension.End.Column; i++)
        {
            ExcelRange cell = worksheet.Cells[rowIndex, i];
            if (null == cell.Value)
            {
                Debug.LogError("该行该列为空！");
                return null;
            }
            string cellValue = cell.Value.ToString();
            rowInfoList.Add(cellValue);
        }
        return rowInfoList;
    }

    private List<string> GetItemFieldInfo()
    {
        List<string> fieldNameList = new List<string>();
        Type type = typeof(Item);
        var itemFields = type.GetFields();
        foreach (var field in itemFields)
        {
            fieldNameList.Add(field.Name);
        }
        return fieldNameList;
    }
}






