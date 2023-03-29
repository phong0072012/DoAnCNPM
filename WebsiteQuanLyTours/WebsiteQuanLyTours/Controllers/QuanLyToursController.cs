﻿
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;
using WebsiteQuanLyTours.Models;
using System.Data;


using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System;

namespace WebsiteQuanLyTours.Controllers
{
    public class QuanLyToursController : Controller
    {
        // GET: QuanLyTours

        quanlytoursDBContext db = new quanlytoursDBContext();





        public ActionResult Index(string search="", string SortColumn ="MaTour",string IconClass = "fa-sort-asc", int page = 1)
        {

            
           // List<Tour> tours = db.Tours.ToList();
            List<Tour> tours = db.tours.Where(row => row.TenTour.Contains(search)).ToList();
            ViewBag.Search = search;


            //short
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;

            if (SortColumn == "MaTour")
            {
                if( IconClass == "fa-sort-asc")
                {
                    tours = tours.OrderBy(row => row.MaTour).ToList();
                }    
                else
                {
                    tours = tours.OrderByDescending(row => row.MaTour).ToList();    
                }    
                
            }    
            else if (SortColumn =="TenTour")
            {
                if (IconClass == "fa-sort-asc")
                {
                    tours = tours.OrderBy(row => row.TenTour).ToList();
                }
                else
                {
                    tours = tours.OrderByDescending(row => row.TenTour).ToList();
                }
            }
            else if (SortColumn == "ThoiGianTour")
            {
                if (IconClass == "fa-sort-asc")
                {
                    tours = tours.OrderBy(row => row.ThoiGianTour).ToList();
                }
                else
                {
                    tours = tours.OrderByDescending(row => row.ThoiGianTour).ToList();
                }
            }
            else if(SortColumn =="Gia")
            {
                if (IconClass == "fa-sort-asc")
                {
                    tours = tours.OrderBy(row => row.Gia).ToList();
                }
                else
                {
                    tours = tours.OrderByDescending(row => row.Gia ).ToList();
                }
            }


            //paging
            int NoOfRecordPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling
                (Convert.ToDouble(tours.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            tours = tours.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();

            return View(tours);
             
          
        }

        public ActionResult HienThi(int id)
        {
           
            Tour pro = db.tours.Where(row => row.MaTour == id).FirstOrDefault();
            return View( pro );
        }


        public ActionResult ThemMoi()
        {
            
            ViewBag.LoaiTours = db.LoaiTours.ToList();
            ViewBag.TourTheoChuDe = db.TourTheoChuDes.ToList();
            ViewBag.TourNuocNgoai = db.TourNuocNgoais.ToList();
            ViewBag.TourTrongNuoc = db.TourTrongNuocs.ToList();
            ViewBag.DiaDiemHotNuocNgoai = db.DiaDiemHotNuocNgoais.ToList();
            ViewBag.DiaDiemHotTrongNuoc = db.DiaDiemHotTrongNuocs.ToList();
         

            return View();
        }

        [HttpPost]
        public ActionResult ThemMoi(Tour  p)
        {
            if (ModelState.IsValid)
            {

            db.tours.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
            }    
            else
            {
                return RedirectToAction("ThemMoi");
            }
            
        }
       
        public ActionResult ChinhSua(int id)
        {
            Tour tour = db.tours.Where(row => row.MaTour == id).FirstOrDefault();
            ViewBag.LoaiTours = db.LoaiTours.ToList();
            ViewBag.TourTheoChuDe = db.TourTheoChuDes.ToList();
            ViewBag.TourNuocNgoai = db.TourNuocNgoais.ToList();
            ViewBag.TourTrongNuoc = db.TourTrongNuocs.ToList();
            ViewBag.DiaDiemHotNuocNgoai = db.DiaDiemHotNuocNgoais.ToList();
            ViewBag.DiaDiemHotTrongNuoc = db.DiaDiemHotTrongNuocs.ToList();
            return View( tour );
        }
        [HttpPost]
        public ActionResult ChinhSua(Tour pro)
        {
        
            Tour tour = db.tours.Where(row => row.MaTour == pro.MaTour).FirstOrDefault();
           


            //update
            tour.TenTour = pro.TenTour;
            tour.ThoiGianTour = pro.ThoiGianTour;
            tour.Gia = pro.Gia;
            tour.ChuongTrinhTour = pro.ChuongTrinhTour;
            tour.MaLoaiTour = pro.MaLoaiTour;
            tour.MaChuDe = pro.MaChuDe;
            tour.MaTourTrongNuoc = pro.MaTourTrongNuoc;
            tour.MaTourNuocNgoai = pro.MaTourNuocNgoai;
            tour.MaDiaDiemHotTrongNuoc = pro.MaDiaDiemHotTrongNuoc;
            tour.MaDiaDiemHotNuocNgoai = pro.MaDiaDiemHotNuocNgoai;
            db.SaveChanges();

                 return RedirectToAction("Index");
        }
        public ActionResult Xoa (int id)
        {
            Tour tour = db.tours.Where(row => row.MaTour == id).FirstOrDefault();
            return View(tour);
        }

        [HttpPost]
        public ActionResult Xoa(int id, Tour p)
        {
            Tour tour = db.tours.Where(row => row.MaTour == id).FirstOrDefault();
            db.tours.Remove(tour);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}