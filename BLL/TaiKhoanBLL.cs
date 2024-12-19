using System;
using DAL;
using DTO;

namespace BLL
{
    public class NguoiSuDungBLL
    {
        private NguoiSuDungDAL nguoiSuDungDAL = new NguoiSuDungDAL();

        // Phương thức xử lý đăng nhập
        public bool DangNhap(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                throw new ArgumentException("Tên đăng nhập và mật khẩu không được để trống");
            }

            return nguoiSuDungDAL.DangNhap(tenDangNhap, matKhau);
        }

        // Phương thức lấy quyền của người dùng
        public string LayQuyen(string tenDangNhap)
        {
            return nguoiSuDungDAL.LayQuyenNguoiSuDung(tenDangNhap);
        }
        public TaiKhoan LayThongTinNguoiDung(string tenDangNhap)
        {
            return nguoiSuDungDAL.LayThongTinNguoiDung(tenDangNhap);
        }

    }
}
