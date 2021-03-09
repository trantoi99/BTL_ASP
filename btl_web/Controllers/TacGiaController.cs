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
    public class TacGiaController : Controller
    {
        private readonly BTL_Library data = new BTL_Library();
        // GET: TacGia
        public ActionResult TacGiaIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ListTacGia()
        {
            var listTacGia = new List<TacGia>();
            var list = data.TacGias.ToList();
            foreach (var item in list)
            {
                var model = new TacGia();
                model.Matacgia = item.Matacgia;
                model.Tentacgia = item.Tentacgia;
                model.Diachi = item.Diachi;
                listTacGia.Add(model);
            }
            return Json(listTacGia, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveChange(TacGia model, int id)
        {
            try
            {
                if (id > 0)
                {
                    var update = data.TacGias.FirstOrDefault(x => x.Matacgia == model.Matacgia);
                    update.Tentacgia = model.Tentacgia;
                    update.Diachi = model.Diachi;

                    data.SaveChanges();
                    return Json(new JMessage() { Error = false, Title = "Cap nhat ban ghi thanh cong !" });
                }
                var checkTacGia = data.TacGias.FirstOrDefault(x => x.Matacgia == model.Matacgia);
                if (checkTacGia != null)
                {
                    return Json(new JMessage() { Error = true, Title = "Bản ghi đã tồn tại !" });
                }
                data.TacGias.Add(model);
                data.SaveChanges();
                return Json(new JMessage() { Error = false, Title = "Thêm mới thành công !" });
            }
            catch
            {
                return Json(new JMessage() { Error = true, Title = "Đã cõ lỗi xảy ra trong quá trình thực hiện !" });
            }
        }
        [HttpDelete]
        public JsonResult Delete(string Matacgia)
        {
            try
            {
                var delete = data.TacGias.FirstOrDefault(x => x.Matacgia == Matacgia);
                if (delete != null)
                {

                    data.TacGias.Remove(delete);
                    data.SaveChanges();
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
        public JsonResult getInfoId(string Matacgia)
        {
            if (Matacgia != null)
            {
                var sach = data.TacGias.FirstOrDefault(x => x.Matacgia == Matacgia);
                return Json(new TacGia() { Matacgia = sach.Matacgia, Tentacgia = sach.Tentacgia, Diachi = sach.Diachi, }, JsonRequestBehavior.AllowGet);
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
            IQueryable<TacGia> lst = data.TacGias;
            //Nếu có từ khóa cần tìm kiếm


            if (!String.IsNullOrEmpty(tuKhoa))
            {
                lst = lst.Where(p => p.Matacgia.Contains(tuKhoa) || p.Tentacgia.Contains(tuKhoa) || p.Diachi.Contains(tuKhoa));
            }

            return View(lst.OrderByDescending(p => p.Matacgia));
        }


        public void ToExcel()
        {
            var listTacGia = new List<TacGia>();
            var list = data.TacGias.ToList();
            foreach (var item in list)
            {
                var model = new TacGia();
                model.Matacgia = item.Matacgia;
                model.Tentacgia = item.Tentacgia;
                model.Diachi = item.Diachi;
                listTacGia.Add(model);
            }


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com2";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report2";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Mã tác giả";
            ws.Cells["B6"].Value = "Tên tác giả";
            ws.Cells["C6"].Value = "Địa Chỉ";


            int rowStart = 7;
            foreach (var item in list)
            {
                if (item.Matacgia.Count() < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Matacgia;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Tentacgia;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Diachi;

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