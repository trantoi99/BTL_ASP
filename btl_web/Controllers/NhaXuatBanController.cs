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
    public class NhaXuatBanController : Controller
    {
        private readonly BTL_Library db = new BTL_Library();
        // GET: NhaXuatBan
        public ActionResult NhaXuatBanIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult listNhaXuatBan()
        {
            var listNhaXuatBan = new List<NXB>();
            var list = db.NXBs.ToList();
            foreach(var item in list)
            {
                var model = new NXB();
                model.Manhaxuatban = item.Manhaxuatban;
                model.Tennhaxuatban = item.Tennhaxuatban;
                model.Diachi = item.Diachi;
                listNhaXuatBan.Add(model);
            }
            return Json(listNhaXuatBan, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveChange(NXB model,int id)
        {
            try
            {
                if (id > 0)
                {
                    var update = db.NXBs.FirstOrDefault(x => x.Manhaxuatban == model.Manhaxuatban);
                    update.Tennhaxuatban = model.Tennhaxuatban;
                    db.SaveChanges();
                    return Json(new JMessage() { Error = false, Title = "Cap nhat ban ghi thanh cong !" });
                }

                var checkkho = db.NXBs.FirstOrDefault(x => x.Manhaxuatban == model.Manhaxuatban);
                if (checkkho != null)
                {
                    return Json(new JMessage() { Error = true, Title = "Bản ghi đã tồn tại!" });
                }
                db.NXBs.Add(model);
                db.SaveChanges();
                return Json(new JMessage() { Error = false, Title = "Thêm mới thành công!" });
            }
            catch
            {
                return Json(new JMessage() { Error = true, Title = "Có lỗi xảy ra trong quá trình thực hiện!" });
            }
        }

        [HttpDelete]
        public JsonResult Delete(string MaNXB)
        {
            try
            {
                var delete = db.NXBs.FirstOrDefault(x => x.Manhaxuatban == MaNXB);
                if (delete != null)
                {

                    db.NXBs.Remove(delete);
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
        public JsonResult getInfoId(string MaNXB)
        {
            if (MaNXB != null)
            {
                var nxb = db.NXBs.FirstOrDefault(x => x.Manhaxuatban == MaNXB);
                return Json(new NXB() { Manhaxuatban = nxb.Manhaxuatban, Tennhaxuatban = nxb.Tennhaxuatban, Diachi = nxb.Diachi }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult timkiem(string tuKhoa)
        {
            //Lấy danh sách kho
            IQueryable<NXB> lst = db.NXBs;
            //Nếu có từ khóa cần tìm kiếm


            if (!String.IsNullOrEmpty(tuKhoa))
            {
                lst = lst.Where(p => p.Manhaxuatban.Contains(tuKhoa) || p.Tennhaxuatban.Contains(tuKhoa));
            }

            return View(lst.OrderByDescending(p => p.Manhaxuatban));
        }


        public void ToExcel()
        {
            var listNhaXuatBan = new List<NXB>();
            var list = db.NXBs.ToList();
            foreach (var item in list)
            {
                var model = new NXB();
                model.Manhaxuatban = item.Manhaxuatban;
                model.Tennhaxuatban = item.Tennhaxuatban;
                model.Diachi = item.Diachi;
                listNhaXuatBan.Add(model);
            }


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com2";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report2";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Ma Nha Xuat Ban";
            ws.Cells["B6"].Value = "Ten Nha Xuat Ban";
            ws.Cells["C6"].Value = "Dia Chi";


            int rowStart = 7;
            foreach (var item in list)
            {
                if (item.Manhaxuatban.Count() < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Manhaxuatban;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Tennhaxuatban;
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