using BusinessLogic;
using BusinessLogic.Excel;
using BusinessLogic.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaoCaoController : ControllerBase
    {
        [HttpPost]
        [Route("getData")]
        public RptDSBaoCao getDuLieuBaoCao(RptParams value)
        {
            if (value != null)
            {
                var result = new RptDSBaoCao();
                //Dictionary<string, object> lstOpt;
                //result.Data = BaoCaoManager.Instance.getDataSetProc(value, out lstOpt);
                //result.RptData = lstOpt;
                return result;
            }
            return null;
        }

        [HttpPost]
        [Route("getExcel")]
        public IActionResult getBaoCaoExce(RptParams value)
        {
            if (value != null)
            {
                Dictionary<string, object> lstOpt;
                var data = new DataSet(); // BaoCaoManager.Instance.getDataSetProc(value, out lstOpt);
                if (data != null)
                {
                    #region xuất excel
                    ReportExcelDataSet cReport = new ReportExcelDataSet(value.TemplatesExcel);
                    var objParams = value.objParams;
                    if (objParams != null)
                    {
                        DateTime from, to, timefrom, timeto;
                        if (objParams.ContainsKey("dateFrom") && DateTime.TryParse(objParams["dateFrom"].ToString(), out from))
                        {
                            cReport.AddFindAndReplaceItem("<DATE_FROM>", from.ToString("dd/MM/yyyy"));
                        }
                        if (objParams.ContainsKey("dateTo") && DateTime.TryParse(objParams["dateTo"].ToString(), out to))
                        {
                            cReport.AddFindAndReplaceItem("<DATE_TO>", to.ToString("dd/MM/yyyy"));
                        }
                        if (objParams.ContainsKey("dateTimeFrom") && DateTime.TryParse(objParams["dateTimeFrom"].ToString(), out timefrom))
                        {
                            cReport.AddFindAndReplaceItem("<DATETIME_FROM>", timefrom.ToString("HH:mm dd/MM/yyyy"));
                        }
                        if (objParams.ContainsKey("dateTimeTo") && DateTime.TryParse(objParams["dateTimeTo"].ToString(), out timeto))
                        {
                            cReport.AddFindAndReplaceItem("<DATETIME_TO>", timeto.ToString("HH:mm dd/MM/yyyy"));
                        }
                    }
                    if (value.objReplace != null)
                    {
                        foreach (string item in value.objReplace.Keys)
                        {
                            object obj2 = value.objReplace[item];
                            cReport.AddFindAndReplaceItem(string.Format("<{0}>", item), obj2);
                        }
                    }

                    cReport.FindAndReplace();
                    cReport.Export2ExcelBold(data.Tables[0], value.ExcelStartRow ?? 1, value.ExcelStartCol ?? 1, value.DataEndCol ?? -1);
                    var result = cReport.SaveStream();
                    return File(result, "application/octet-stream", string.Format("{0}_{1}.xlsx", value.TemplatesExcel, DateTime.Now.ToString("yyyyMMddHHmmss")));
                    #endregion
                }
                return null;
            }
            return null;
        }

        [HttpPost]
        [Route("exportExcel")]
        public IActionResult exportExce(RptExcelData value)
        {
            if (value != null)
            {
                ReportExcelDataSet cReport = new ReportExcelDataSet(value.TemplatesExcel);
                if (value.objReplace != null)
                {
                    foreach (string item in value.objReplace.Keys)
                    {
                        object obj2 = value.objReplace[item];
                        cReport.AddFindAndReplaceItem(string.Format("<{0}>", item), obj2);
                    }
                    cReport.FindAndReplace();
                }
                if (value.CellReplace != null)
                {
                    foreach (var item in value.CellReplace)
                    {
                        cReport.SetValueToCell(item.row, item.col, item.value);
                    }
                }
                if (value.ObjData != null)
                {
                    cReport.Export2ExcelByJson(value.ObjData, value.ExcelStartRow ?? 1, value.ExcelStartCol ?? 1, value.ColmunName, value.InsertSTT ?? false);
                }
                if (value.TableData != null)
                {
                    cReport.Export2ExcelBold(value.TableData, value.ExcelStartRow ?? 1, value.ExcelStartCol ?? 1, value.DataEndCol ?? -1);
                }
                var result = cReport.SaveStream();
                return File(result, "application/octet-stream", string.Format("{0}_{1}.xlsx", value.TemplatesExcel, DateTime.Now.ToString("yyyyMMddHHmmss")));
            }
            return null;
        }

        [HttpPost]
        [Route("testExcel")]
        public IActionResult getBaoCaoExce(string test)
        {
            if (test != null)
            {
                Dictionary<string, object> lstOpt;
                ReportExcelDataSet cReport = new ReportExcelDataSet("test");
                var data = new List<TestModel>();
                data.Add(new TestModel
                {
                    Code = "1",
                    Name = "name1",
                });
                string[] arr = { "Code", "Name" };
                cReport.Export2ExcelByList(data, 1, 1, arr, true);
                var result = cReport.SaveStream();
                return File(result, "application/octet-stream", string.Format("{0}_{1}.xlsx", "test.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss")));
            }
            return null;
        }
    }
}