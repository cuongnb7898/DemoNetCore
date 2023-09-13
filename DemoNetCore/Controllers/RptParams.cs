using System.Collections.Generic;
using System.Data;

namespace DemoNetCore.Controllers
{
    public class RptParams
    {
        public string procName { get; set; }
        public Dictionary<string, object> objParams { get; set; }
        public Dictionary<string, object> objReplace { get; set; }
        public string[] optParams { get; set; }
        public string TemplatesExcel { get; set; }
        public int? ExcelStartCol { get; set; }
        public int? ExcelStartRow { get; set; }
        public int? DataStartCol { get; set; }
        public int? DataEndCol { get; set; }
    }
    public class RptTblBaoCao
    {
        public DataTable Table { get; set; }
        public Dictionary<string, object> RptData { get; set; }
    }
    public class RptDSBaoCao
    {
        public DataSet Data { get; set; }
        public Dictionary<string, object> RptData { get; set; }
    }

    public class RptCellValue
    {
        public int row { get; set; }
        public int col { get; set; }
        public object value { get; set; }
    }

    public class RptExcelData
    {
        public Dictionary<string, object> objReplace { get; set; }
        public IEnumerable<object> ObjData { get; set; }
        public IEnumerable<object> ListDatas { get; set; }
        public IEnumerable<RptCellValue> CellReplace { get; set; }
        public string[] ColmunName { get; set; }
        public bool? InsertSTT { get; set; }
        public DataTable TableData { get; set; }
        public int? DataStartCol { get; set; }
        public int? DataEndCol { get; set; }
        public string TemplatesExcel { get; set; }
        public string Border { get; set; }
        public int? ExcelStartCol { get; set; }
        public int? ExcelStartRow { get; set; }
    }

    public class RptExcelListData<T>
    {
        public Dictionary<string, object> objReplace { get; set; }
        public IEnumerable<T> ListDatas { get; set; }
        public IEnumerable<List<T>> ListDatas2 { get; set; }
        public string[] ColmunName { get; set; }
        public bool? InsertSTT { get; set; }
        public int? DataStartCol { get; set; }
        public int? DataEndCol { get; set; }
        public string TemplatesExcel { get; set; }
        public int? ExcelStartCol { get; set; }
        public int? ExcelStartRow { get; set; }
        public string Border { get; set; }
        public List<Dictionary<string, object>> listReplace { get; set; }

    }
    public class RptListMa
    {
        public string lstMa { get; set; }
    }
}