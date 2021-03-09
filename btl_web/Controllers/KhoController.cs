using Framework.EF;
using Framework.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace btl_web.Controllers
{
    public class KhoController : Controller
    {
        // GET: Kho
        private readonly BTL_Library db = new BTL_Library();
        public ActionResult KhoIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ListKho()
        {
            var listKho = new List<Kho>();
            var list = db.Khoes.ToList();
            foreach (var item in list)
            {
                var model = new Kho();
                model.Makho = item.Makho;
                // 
                model.Tenkho = item.Tenkho;
                listKho.Add(model);
            }
            return Json(listKho, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveChange(Kho model, int id)
        {
            try
            {
                if (id > 0)
                {
                    var update = db.Khoes.FirstOrDefault(x => x.Makho == model.Makho);
                    update.Tenkho = model.Tenkho;
                    db.SaveChanges();
                    return Json(new JMessage() { Error = false, Title = "Cap nhat ban ghi thanh cong !" });
                }
                var checkkho = db.Khoes.FirstOrDefault(x => x.Makho == model.Makho);
                if (checkkho != null)
                {
                    return Json(new JMessage() { Error = true, Title = "Bản ghi đã tồn tại !" });
                }
                db.Khoes.Add(model);
                db.SaveChanges();
                return Json(new JMessage() { Error = false, Title = "Thêm mới thành công !" });
            }
            catch
            {
                return Json(new JMessage() { Error = true, Title = "Đã cõ lỗi xảy ra trong quá trình thực hiện !" });
            }
        }
        [HttpDelete]
        public JsonResult Delete(string Makho)
        {
            try
            {
                var delete = db.Khoes.FirstOrDefault(x => x.Makho == Makho);
                if (delete != null)
                {

                    db.Khoes.Remove(delete);
                    db.SaveChanges();
                    return Json(new JMessage { Error = false, Title = "Xoa thanh cong!" });
                }
                else
                {
                    return Json(new JMessage { Error = true, Title = "Khong tim thay ban ghi!" });
                }
            }
            catch
            {
                return Json(new JMessage { Error = true, Title = "Đã cõ lỗi xảy ra trong quá trình thực hiện!" });
            }
        }
        [HttpPost]
        public JsonResult getInfoId(string Makho)
        {
            if (Makho != null)
            {
                var kho = db.Khoes.FirstOrDefault(x => x.Makho == Makho);
                return Json(new Kho() { Makho = kho.Makho, Tenkho = kho.Tenkho }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult timkiem(string tuKhoa)
        {
            //Lấy danh sách kho
            IQueryable<Kho> lst = db.Khoes;
            //Nếu có từ khóa cần tìm kiếm


            if (!String.IsNullOrEmpty(tuKhoa))
            {
                lst = lst.Where(p => p.Makho.Contains(tuKhoa) || p.Tenkho.Contains(tuKhoa));
            }

            return View(lst.OrderByDescending(p => p.Makho));
        }


        public void ToExcel()
        {
            var listKho = new List<Kho>();
            var list = db.Khoes.ToList();
            foreach (var item in list)
            {
                var model = new Kho();
                model.Makho = item.Makho;
                model.Tenkho = item.Tenkho;
                listKho.Add(model);
            }


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com1";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report1";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "MaKho";
            ws.Cells["B6"].Value = "TenKho";


            int rowStart = 7;
            foreach (var item in list)
            {
                if (item.Makho.Count() < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Makho;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Tenkho;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }
    }
}