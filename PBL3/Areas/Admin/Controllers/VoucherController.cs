﻿using Models.BLL;
using Models.DTO;
using PBL3.Helper;
using PBL3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBL3.Areas.Admin.Controllers
{
    [HasLogin(Role = "ADMIN")]
    public class VoucherController : Controller
    {
        // GET: Admin/View
        public ActionResult Index(int page = 1, string keyword = "")
        {
            VoucherBLL voucherDAO = new VoucherBLL();
            int totalPage = 0;
            ViewBag.Vouchers = voucherDAO.getPage(page, 10, keyword, out totalPage);
            ViewBag.Total = voucherDAO.Count();
            ViewBag.pagingData = new PagingModel
            {
                CountPages = totalPage,
                CurrentPage = page,
                GenerateURL = (int pageNum) => $"?page={pageNum}&keyword={keyword}"
            };
            return View();
        }
        public ActionResult View(int? id, bool isEdit = false)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Voucher voucher = new VoucherBLL().find((int)id);
            if (voucher == null)
            {
                TempData["Message"] = "Voucher không tồn tại";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.isEdit = isEdit;
                return View(voucher);
            }
        }
        public ActionResult Add(Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                VoucherBLL voucherDAO = new VoucherBLL();
                voucherDAO.Add(voucher);
                TempData["Status"] = true;
            }
            else
            {
                TempData["Status"] = false;
                TempData["Message"] = "Thêm thất bại";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {

            VoucherBLL voucherDAO = new VoucherBLL();
            if (voucherDAO.Delete(id) == true)
            {
                return new JsonResult
                {
                    Data = new
                    {
                        status = true,
                        message = "Xóa thành công voucher"
                    }
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new
                    {
                        status = false,
                        message = "Xóa thất bại voucher"
                    }
                };
            }
        }
        public ActionResult Update(Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                VoucherBLL voucherDAO = new VoucherBLL();
                if (voucherDAO.Update(voucher) == true)
                {
                    TempData["Status"] = true;
                    TempData["Message"] = "Cập nhật voucher thành công";
                    return RedirectToAction("View", new { id = voucher.ID });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Status"] = false;
                TempData["Message"] = "Cập nhật voucher thất bại";
                return RedirectToAction("View", new { id = voucher.ID, isEdit = true });
            }
        }
    }
}