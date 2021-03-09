using Framework.EF;
using Framework.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace btl_web.Controllers
{
    public class MuonController : Controller
    {
        private readonly BTL_Library data = new BTL_Library();

        public class combo
        {
            public string sinhvien { get; set; }
        }

        // GET: Muon
        public ActionResult MuonIndex()
        {
            ViewBag.sinhvien = data.SinhViens.ToList();
            ViewBag.sach = data.Saches.ToList();
            return View();
        }
        [HttpGet]
        public ActionResult ListMuon()
        {
            var listMuon = new List<Muon>();
            var list = data.Muons.ToList();
            foreach ( var item in list)
            {
                var model = new Muon();
                model.Masinhvien = item.Masinhvien;
                model.Masach = item.Masach;
                model.Hinhthucmuon = item.Hinhthucmuon;
                model.Ngaymuon = item.Ngaymuon;
                model.Ngaytra = item.Ngaytra;
                model.Songaymuon = item.Songaymuon;
                listMuon.Add(model);
            }
            return Json(listMuon, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public JsonResult SaveChange(Muon model,int id)
        {
            try
            {

                if (id > 0)
                {
                    var update = data.Muons.FirstOrDefault(x => x.Masinhvien == model.Masinhvien);
                    update.Masach = model.Masach;
                    update.Ngaymuon = model.Ngaymuon;
                    update.Ngaytra = model.Ngaytra;
                    update.Songaymuon = model.Songaymuon;
                    update.Hinhthucmuon = model.Hinhthucmuon;

                    data.SaveChanges();
                    return Json(new JMessage() { Error = false, Title = "Cap nhat ban ghi thanh cong !" });
                }

                var checkmuon = data.Muons.FirstOrDefault(x => x.Masinhvien == model.Masinhvien);
                if (checkmuon != null)
                {
                    return Json(new JMessage() { Error = true, Title = "Bản ghi đã tồn tại!" });
                }
                data.Muons.Add(model);
                data.SaveChanges();
                return Json(new JMessage() { Error = false, Title = "Thêm mới thành công!" });
            }
            catch
            {
                return Json(new JMessage() { Error = true, Title = "Có lỗi xảy ra trong quá trình thực hiện!" });
            }
        }
        [HttpGet]
        public async Task<ActionResult> getList()
        {
            var listsv = await data.SinhViens.ToListAsync();
            return Json(listsv, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult Delete(string Masinhvien)
        {
            try
            {
                var delete = data.Muons.FirstOrDefault(x => x.Masinhvien == Masinhvien);
                if (delete != null)
                {

                    data.Muons.Remove(delete);
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
        public JsonResult getInfoId(string Masinhvien)
        {
            if (Masinhvien != null)
            {
                var muon = data.Muons.FirstOrDefault(x => x.Masinhvien == Masinhvien);
                return Json(new Muon() { Masinhvien = muon.Masinhvien, Masach = muon.Masach, Hinhthucmuon=muon.Hinhthucmuon,Ngaymuon=muon.Ngaymuon,Ngaytra=muon.Ngaytra,Songaymuon=muon.Songaymuon }, JsonRequestBehavior.AllowGet);
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
            IQueryable<Muon> lst = data.Muons;
            //Nếu có từ khóa cần tìm kiếm


            if (!String.IsNullOrEmpty(tuKhoa))
            {
                lst = lst.Where(p => p.Masach.Contains(tuKhoa) || p.Masinhvien.Contains(tuKhoa));
            }

            return View(lst.OrderByDescending(p => p.Masach));
        }


        public void ToExcel()
        {
            var listMuon = new List<Muon>();
            var list = data.Muons.ToList();
            foreach (var item in list)
            {
                var model = new Muon();
                model.Masinhvien = item.Masinhvien;
                model.Masach = item.Masach;
                model.Hinhthucmuon = item.Hinhthucmuon;
                model.Ngaymuon = item.Ngaymuon;
                model.Ngaytra = item.Ngaytra;
                model.Songaymuon = item.Songaymuon;
                listMuon.Add(model);
            }


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Bảng :";
            ws.Cells["B1"].Value = "Muon";

            ws.Cells["A2"].Value = "Báo Cáo";
            ws.Cells["B2"].Value = "Báo cáo ";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Mã Sinh Viên";
            ws.Cells["B6"].Value = "Mã Sách";
            ws.Cells["C6"].Value = "Hình Thức Mượn";
            ws.Cells["D6"].Value = "Ngày Mượn";
            ws.Cells["E6"].Value = "Ngày Trả";
            ws.Cells["F6"].Value = "Số Ngày Mượn";


            int rowStart = 7;
            foreach (var item in list)
            {
                if (item.Masinhvien.Count() < 5)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("red")));

                }

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Masinhvien;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Masach;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Hinhthucmuon;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Ngaymuon;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Ngaytra;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Songaymuon;

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