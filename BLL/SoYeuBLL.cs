using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class HocVienBLL
    {
        private NguoiSuDungDAL hocvienDAL = new NguoiSuDungDAL();
        public SoYeuLL laythongtinhocvien(int IdDangNhap)
        {
            return  hocvienDAL.laythongtinhocvien(IdDangNhap);
        }
    }
}
