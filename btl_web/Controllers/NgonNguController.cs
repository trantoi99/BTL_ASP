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
    public class NgonNguController : Controller
    {
        private readonly BTL_Library db = new BTL_Library();
        // GET: NgonNgu
        public ActionResult NgonNguIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ListNgonNgu()
        {
            var listNgonNgu = new List<NgonNgu>();
            var list = db.NgonNgus.ToList();
            foreach (var item in list)
            {
                var model = new NgonNgu();
                model.Mangonngu = item.Mangonngu;
                model.Tenngonngu = item.Tenngonngu;
                listNgonNgu.Add(model);
            }
            return Json(listNgonNgu, JsonRequestBehavior.AllowGet);
        }
       

        [HttpPost]
        public JsonResult SaveChange(NgonNgu model,int id)
        {
            try
            {
                if (id > 0)
                {
                    var update = db.NgonNgus.FirstOrDefault(x => x.Mangonngu == model.Mangonngu);
                    update.Tenngonngu = model.Tenngonngu;
                    db.SaveChanges();
                    return Json(new JMessage() { Error = false, Title = "Cap nhat ban ghi thanh cong !" });
                }

                var checkNgonNgu = db.NgonNgus.FirstOrDefault(x => x.Mangonngu == model.Mangonngu);
                if (checkNgonNgu != null)
                {
                    return Json(new JMessage() { Error = true, Title = "Bản ghi đã tồn tại!" });
                }
                db.NgonNgus.Add(model);
                db.SaveChanges();
                return Json(new JMessage() { Error = false, Title = "Thêm mới thành công!" });
            }
            catch
            {
                return Json(new JMessage() { Error = true, Title = "Có lỗi xảy ra trong quá trình thực hiện!" });
            }
        }

        [HttpDelete]
        public JsonResult Delete(string Mangonngu)
        {
            try
            {
                var delete = db.NgonNgus.FirstOrDefault(x => x.Mangonngu == Mangonngu);
                if (delete != null)
                {

                    db.NgonNgus.Remove(delete);
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
        public JsonResult getInfoId(string Mangonngu)
        {
            if (Mangonngu != null)
            {
                var kho = db.NgonNgus.FirstOrDefault(x => x.Mangonngu == Mangonngu);
                return Json(new NgonNgu() { Mangonngu = kho.Mangonngu, Tenngonngu = kho.Tenngonngu }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult timkiem(string tuKhoa)
        {
            //Lấy danh sách kho
            IQueryable<NgonNgu> lst = db.NgonNgus;
            //Nếu có từ khóa cần tìm kiếm


            if (!String.IsNullOrEmpty(tuKhoa))
            {
                lst = lst.Where(p => p.Mangonngu.Contains(tuKhoa) || p.Tenngonngu.Contains(tuKhoa));
            }

            return View(lst.OrderByDescending(p => p.Mangonngu));
        }


        public void ToExcel()
        {
            var listNgonNgu = new List<NgonNgu>();
            var list = db.NgonNgus.ToList();
            foreach (var item in list)
            {
                var model = new NgonNgu();
                model.Mangonngu = item.Mangonngu;
                model.Tenngonngu = item.Tenngonngu;
                listNgonNgu.Add(model);
            }


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com2";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report2";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Mã Ngôn Ngữ ";
            ws.Cells["B6"].Value = "Tên Ngôn Ngữ";


            int rowStart = 7;
            foreach (var item in list)
            {
                if (item.Mangonngu.Count() < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Mangonngu;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Tenngonngu;

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