using Framework.EF;
using Framework.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace btl_web.Controllers
{
    public class VitriController : Controller
    {
        private readonly BTL_Library db = new BTL_Library();
        // GET: Vitri
        public ActionResult VitriIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ListVitri()
        {
            var listVitri = new List<ViTri>();
            var list = db.ViTris.ToList();
            foreach (var item in list)
            {
                var model = new ViTri();
                model.Mavitri = item.Mavitri;
                model.Khu = item.Khu;
                model.Ke = item.Ke;
                model.Ngan = item.Ngan;
                listVitri.Add(model);
            }
            return Json(listVitri, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveChange(ViTri model, int id)
        {
            try
            {
                if (id > 0)
                {
                    var update = db.ViTris.FirstOrDefault(x => x.Mavitri == model.Mavitri);
                    update.Khu = model.Khu;
                    update.Ke = model.Ke;
                    update.Ngan = model.Ngan;
                    db.SaveChanges();
                    return Json(new JMessage() { Error = false, Title = "Cap nhat ban ghi thanh cong !" });
                }
                var checkViTri = db.ViTris.FirstOrDefault(x => x.Mavitri == model.Mavitri);
                if (checkViTri != null)
                {
                    return Json(new JMessage() { Error = true, Title = "Bản ghi đã tồn tại !" });
                }
                db.ViTris.Add(model);
                db.SaveChanges();
                return Json(new JMessage() { Error = false, Title = "Thêm mới thành công !" });
            }
            catch
            {
                return Json(new JMessage() { Error = true, Title = "Đã cõ lỗi xảy ra trong quá trình thực hiện !" });
            }
        }
        [HttpDelete]
        public JsonResult Delete(string MaVitri)
        {
            try
            {
                var delete = db.ViTris.FirstOrDefault(x => x.Mavitri == MaVitri);
                if (delete != null)
                {

                    db.ViTris.Remove(delete);
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
        public JsonResult getInfoId(string MaVitri)
        {
            if (MaVitri != null)
            {
                var sach = db.ViTris.FirstOrDefault(x => x.Mavitri == MaVitri);
                return Json(new ViTri() { Mavitri = sach.Mavitri, Khu = sach.Khu, Ke = sach.Ke, Ngan = sach.Ngan }, JsonRequestBehavior.AllowGet);
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
            IQueryable<ViTri> lst = db.ViTris;
            //Nếu có từ khóa cần tìm kiếm


            if (!String.IsNullOrEmpty(tuKhoa))
            {
                lst = lst.Where(p => p.Mavitri.Contains(tuKhoa) || p.Khu.Contains(tuKhoa) || p.Ke.Contains(tuKhoa) || p.Ngan.Contains(tuKhoa));
            }

            return View(lst.OrderByDescending(p => p.Mavitri));
        }


        public void ToExcel()
        {
            var listVitri = new List<ViTri>();
            var list = db.ViTris.ToList();
            foreach (var item in list)
            {
                var model = new ViTri();
                model.Mavitri = item.Mavitri;
                model.Khu = item.Khu;
                model.Ke = item.Ke;
                model.Ngan = item.Ngan;
                listVitri.Add(model);
            }


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com2";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report2";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Mã vị trí";
            ws.Cells["B6"].Value = "Khu";
            ws.Cells["C6"].Value = "Kệ";
            ws.Cells["D6"].Value = "Ngăn";


            int rowStart = 7;
            foreach (var item in list)
            {
                if (item.Mavitri.Count() < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Mavitri;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Khu;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Ke;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Ngan;

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