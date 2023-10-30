using OfficeOpenXml;
using Cafe.Domain.Entities;
using Cafe.Application.Interfaces;

namespace Cafe.Application.Extenssions;

public static class WorksheetExtenssion
{
    private static readonly string[] billHeaderLine = {"Id/Name", "DateTime", "Sale", "Total", "Dsihes"};
    private static readonly string[] ingridientHeaderLine = {"Id/Name", "Count"};

    private static void billHeader(this ExcelWorksheet sheet, ref int i, ref int j)
    {
        sheet.Cells[i, j + billHeaderLine.Length - 1, i, j + billHeaderLine.Length].Merge = true;
        sheet.Cells[i, j, i, j + billHeaderLine.Length].LoadFromArrays( new object[][] {billHeaderLine});
        sheet.Cells[i, j, i, j + billHeaderLine.Length].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; 
        i += 1;
    }

    public static void ingHeader(this ExcelWorksheet sheet, ref int i, ref int j)
    {
        sheet.Cells[i, j, i, j + ingridientHeaderLine.Length].LoadFromArrays( new object[][] {ingridientHeaderLine});
        sheet.Cells[i, j, i, j + ingridientHeaderLine.Length].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; 
        i += 1;
    }

    public static async Task<List<Pair<Guid, double>>> WriteBills(this ExcelWorksheet sheet, 
                                                    List<Bill> bills, 
                                                    IDishRepository dishRepository, 
                                                    int i, 
                                                    int j)
    {
        var ans = new List<Pair<Guid, double>>();

        sheet.billHeader(ref i, ref j);
    
        foreach(var bill in bills)
        {
            decimal total = 0;
            for(int k = 0; k < bill.Dishes.Count; k++)
            {
                var dish = await dishRepository.GetByIdAsync(bill.Dishes[k].First);
                foreach(var pair in dish.Ingridients)
                {
                    var index = ans.FindIndex(ing => ing.First == pair.First);
                    if(index == -1)
                    {
                        ans.Add(new Pair<Guid, double>(pair.First, pair.Second * bill.Dishes[k].Second));
                    }
                    else 
                    {
                        ans[index] = new Pair<Guid, double>(pair.First, ans[index].Second + pair.Second * bill.Dishes[k].Second);
                    }
                }
                total += (decimal)bill.Dishes[k].Second * dish.Price;

                sheet.Cells[i + k + 1, j + 4].Value = dish.Name;
                sheet.Cells[i + k + 1, j + 5].Value = bill.Dishes[k].Second;
            }

            sheet.Cells[i, j + 0].Value = bill.Id;
            sheet.Cells[i, j + 1].Value = bill.DateTime.ToString("hh:mm:ss");
            sheet.Cells[i, j + 2].Value = bill.Sale;
            sheet.Cells[i, j + 3].Value = total - (decimal)bill.Sale * total;
            sheet.Cells[i, j + 4].Value = "Name";
            sheet.Cells[i, j + 5].Value = "Count";

            i += 1 + bill.Dishes.Count;
        }

        return ans;
    }

     public static void WriteIngridients(this ExcelWorksheet sheet, 
                                        List<Pair<Guid, double>> ingridients, 
                                        int i, 
                                        int j)
    {
        sheet.ingHeader(ref i, ref j);
        foreach(var a in ingridients)
        {
            sheet.Cells[i, j + 0].Value = a.First;
            sheet.Cells[i, j + 1].Value = a.Second;
            i += 1;
        }
    }
}
