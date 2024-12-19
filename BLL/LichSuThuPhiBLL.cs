﻿using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LichSuThuPhiBLL
    {
        private NguoiSuDungDAL dal = new NguoiSuDungDAL();
       

        public bool ThemLichSuThuPhi(int idHocVien, decimal soTien, DateTime ngayNop, string noiDung = "Đã thanh toán thành công")
        {
            return dal.ThemLichSuThuPhi(idHocVien, soTien, ngayNop, noiDung);
        }
        public List<LichSuThuPhiDTO> LayLichSuThuPhi(int idHocVien)
        {
            return dal.GetLichSuThuPhiByIdHocVien(idHocVien);
        }
    }
}
