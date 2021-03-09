using Framework.EF;
using Framework.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace btl_web.Controllers
{
    public class LoaiSachController : Controller
    {
        private readonly BTL_Library data = new BTL_Library();
        // GET: LoaiSach
        public ActionResult LoaiSachIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ListLoaiSach()
        {
            var listLoaiSach = new List<LoaiSach>();
            var list = data.LoaiSaches.ToList();
            foreach (var item in list)
            {
                var model = new LoaiSach();
                model.Maloai = item.Maloai;
                model.Tenloai = item.Tenloai;
                listLoaiSach.Add(model);
            }
            return Json(listLoaiSach, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveChange(LoaiSach model,int id)
        {
            try
            {
                if(id > 0)
                {
                    var update = data.LoaiSaches.FirstOrDefault(x => x.Maloai == model.Maloai);
                    update.Tenloai = model.Tenloai;
                    data.SaveChanges();
                    return Json(new JMessage() { Error = false, Title = "Cap nhat ban ghi thanh cong !" });
                }
                var checkLoaiSach = data.LoaiSaches.FirstOrDefault(x => x.Maloai == model.Maloai);
                if (checkLoaiSach != null)
                {
                    return Json(new JMessage() { Error = true, Title = "Bản ghi đã tồn tại !" });
                }
                data.LoaiSaches.Add(model);
                data.SaveChanges();
                return Json(new JMessage() { Error = false, Title = "Thêm mới thành công !" });
            }
            catch
            {
                return Json(new JMessage() { Error = true, Title = "Đã cõ lỗi xảy ra trong quá trình thực hiện !" });
            }
        }
        [HttpDelete]
        public JsonResult Delete(string MaLoai)
        {
            try
            {
                var delete = data.LoaiSaches.FirstOrDefault(x => x.Maloai == MaLoai);
                if (delete  != null)
                {

                    data.LoaiSaches.Remove(delete);
                    data.SaveChanges();
                    return Json(new JMessage { Error = false , Title = "Xoa thanh cong!"});
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
        public JsonResult getInfoId(string MaLoai)
        {
            if(MaLoai != null)
            {
                var sach = data.LoaiSaches.FirstOrDefault(x => x.Maloai == MaLoai);
                return Json(new LoaiSach() { Maloai = sach.Maloai, Tenloai = sach.Tenloai}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult timkiem(string tuKhoa)
        {
            
            IQueryable<LoaiSach> lst = data.LoaiSaches;
            //Nếu có từ khóa cần tìm kiếm


            if (!String.IsNullOrEmpty(tuKhoa))
            {
                lst = lst.Where(p => p.Maloai.Contains(tuKhoa) || p.Tenloai.Contains(tuKhoa));
            }

            return View(lst.OrderByDescending(p => p.Maloai));
        }


        public void ToExcel()
        {
            var listloai = new List<LoaiSach>();
            var list = data.LoaiSaches.ToList();
            foreach (var item in list)
            {
                var model = new LoaiSach();
                model.Maloai = item.Maloai;
                model.Tenloai = item.Tenloai;
                listloai.Add(model);
            }


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com2";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report2";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Maloai";
            ws.Cells["B6"].Value = "Tenloai";


            int rowStart = 7;
            foreach (var item in list)
            {
                if (item.Maloai.Count() < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Maloai;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Tenloai;

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