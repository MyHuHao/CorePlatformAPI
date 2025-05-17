using System.Text;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Core.Helpers;

public static class ExcelHelper
{
    public static MemoryStream Export<T>(List<T> data, List<string> names, List<Type> types,
        string? sheetName = "Sheet1",
        ICellStyle? cellStyle = null)
    {
        XSSFWorkbook workbook = new();
        var sheet = workbook.CreateSheet(sheetName);
        var headerRow = sheet.CreateRow(0);
        if (cellStyle == null)
        {
            cellStyle = workbook.CreateCellStyle();
            // 居中对齐
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            // 四边加框
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            // 填满綠色
            cellStyle.FillForegroundColor = HSSFColor.LightCornflowerBlue.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            // 粗体字形
            var font = workbook.CreateFont();
            font.IsBold = true;
            cellStyle.SetFont(font);
        }

        var type = typeof(T);
        var properties = type.GetProperties();
        for (var i = 0; i < names.Count; i++)
        {
            var headerCell = headerRow.CreateCell(i);
            headerCell.SetCellValue(names[i]);
            headerRow.GetCell(i).CellStyle = cellStyle;
        }

        for (var i = 0; i < data.Count; i++)
        {
            var dataRow = sheet.CreateRow(i + 1);
            var item = data[i];
            for (var j = 0; j < types.Count; j++)
            {
                var property = properties[j];
                var value = property.GetValue(item);
                var dataCell = dataRow.CreateCell(j);
                if (value != null)
                {
                    if (types[j] == typeof(string))
                        dataCell.SetCellValue(value.ToString());
                    else if (types[j] == typeof(int) || types[j] == typeof(float) || types[j] == typeof(double) ||
                             types[j] == typeof(decimal))
                        //这样的话 单元格类型就不是文本类型
                        dataCell.SetCellValue(double.Parse(value.ToString()!));
                    else if (types[j] == typeof(DateTime))
                        dataCell.SetCellValue(((DateTime)value).ToString("yyyy-MM-dd"));
                    else if (types[j] == typeof(bool))
                        dataCell.SetCellValue((bool)value ? "是" : "否");
                    else
                        dataCell.SetCellValue(value.ToString());
                }
            }
        }

        var headerAverageWidth = 0;
        for (var i = 0; i < headerRow.LastCellNum; i++)
        {
            var cell = headerRow.GetCell(i);
            if (cell == null) continue;
            var cellWidth = Encoding.UTF8.GetBytes(cell.ToString()!).Length;
            if (cellWidth > headerAverageWidth) headerAverageWidth = cellWidth;
        }

        var maxLength = new int[sheet.GetRow(0).LastCellNum + 1];
        for (var rowIndex = sheet.FirstRowNum + 1; rowIndex <= sheet.LastRowNum; rowIndex++) // 跳过表头行  
        {
            var row = sheet.GetRow(rowIndex);
            if (row == null) continue;
            for (var cellIndex = 0; cellIndex < row.LastCellNum; cellIndex++)
            {
                var cell = row.GetCell(cellIndex);
                if (cell == null) continue;
                var cellLength = Encoding.UTF8.GetBytes(cell.ToString()!).Length;
                if (cellLength > maxLength[cellIndex]) maxLength[cellIndex] = cellLength;
            }
        }

        const int maxAllowedWidth = 255 * 256;
        for (var i = 0; i < sheet.GetRow(0).LastCellNum; i++) // 使用表头行的单元格数量作为列数
        {
            var calculatedWidth = Math.Max(headerAverageWidth, maxLength[i] + 1) * 256;
            sheet.SetColumnWidth(i, Math.Min(calculatedWidth, maxAllowedWidth)); // 限制列宽
        }

        MemoryStream stream = new();
        workbook.Write(stream);
        return new MemoryStream(stream.ToArray());
    }
}