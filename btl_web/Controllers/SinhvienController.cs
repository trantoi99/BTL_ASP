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
    public class SinhvienController : Controller
    {
        private readonly BTL_Library db = new BTL_Library();
        // GET: Sinhvien
        public ActionResult SinhvienIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult listSinhvien()
        {
            var listSinhvien = new List<SinhVien>();
            var list = db.SinhViens.ToList();
            foreach (var item in list)
            {
                var model = new SinhVien();
                model.Masinhvien = item.Masinhvien;
                model.Tensinhvien = item.Tensinhvien;
                model.Lop = item.Lop;
                listSinhvien.Add(model);

            }
            return Json(listSinhvien, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveChange(SinhVien model, int id)
        {
            try
            {
                if (id > 0)
                {
                    var update = db.SinhViens.FirstOrDefault(x => x.Masinhvien == model.Masinhvien);
                    update.Tensinhvien = model.Tensinhvien;
                    update.Lop = model.Lop;

                    db.SaveChanges();
                    return Json(new JMessage() { Error = false, Title = "Cap nhat ban ghi thanh cong !" });
                }
                var checkSinhVien = db.SinhViens.FirstOrDefault(x => x.Masinhvien == model.Masinhvien);
                if (checkSinhVien != null)
                {
                    return Json(new JMessage() { Error = true, Title = "Bản ghi đã tồn tại !" });
                }
                db.SinhViens.Add(model);
                db.SaveChanges();
                return Json(new JMessage() { Error = false, Title = "Thêm mới thành công !" });
            }
            catch
            {
                return Json(new JMessage() { Error = true, Title = "Đã cõ lỗi xảy ra trong quá trình thực hiện !" });
            }
        }
        [HttpDelete]
        public JsonResult Delete(string Masinhvien)
        {
            try
            {
                var delete = db.SinhViens.FirstOrDefault(x => x.Masinhvien == Masinhvien);
                if (delete != null)
                {

                    db.SinhViens.Remove(delete);
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
        public JsonResult getInfoId(string Masinhvien)
        {
            if (Masinhvien != null)
            {
                var sach = db.SinhViens.FirstOrDefault(x => x.Masinhvien == Masinhvien);
                return Json(new SinhVien() { Masinhvien = sach.Masinhvien, Tensinhvien = sach.Tensinhvien, Lop = sach.Lop, }, JsonRequestBehavior.AllowGet);
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
            IQueryable<SinhVien> lst = db.SinhViens;
            //Nếu có từ khóa cần tìm kiếm


            if (!String.IsNullOrEmpty(tuKhoa))
            {
                lst = lst.Where(p => p.Masinhvien.Contains(tuKhoa) || p.Lop.Contains(tuKhoa) || p.Tensinhvien.Contains(tuKhoa));
            }

            return View(lst.OrderByDescending(p => p.Masinhvien));
        }


        public void ToExcel()
        {
            var listSinhvien = new List<SinhVien>();
            var list = db.SinhViens.ToList();
            foreach (var item in list)
            {
                var model = new SinhVien();
                model.Masinhvien = item.Masinhvien;
                model.Tensinhvien = item.Tensinhvien;
                model.Lop = item.Lop;
                listSinhvien.Add(model);

            }


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com2";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report2";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Mã sinh viên";
            ws.Cells["B6"].Value = "Tên sinh viên";
            ws.Cells["C6"].Value = "Lớp";


            int rowStart = 7;
            foreach (var item in list)
            {
                if (item.Masinhvien.Count() < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Masinhvien;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Tensinhvien;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Lop;

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