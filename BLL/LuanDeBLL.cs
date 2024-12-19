using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LuanVanBLL
    {
        private NguoiSuDungDAL luanVanDAL = new NguoiSuDungDAL();
        public List<LuanVan> LayLuanVanTheoHocVien(int idHocVien)
        {
            return luanVanDAL.LayLuanVanTheoHocVien(idHocVien);
        }
        public decimal TinhTongTienLuanVan(int idHocVien)
        {
            return luanVanDAL.TinhTongTienLuanVan(idHocVien);
        }
        public bool XoaLuanVan(int idLuanVan)
        {
            return luanVanDAL.XoaLuanVan(idLuanVan);
        }
        public DataTable GetLuanVanByHocVienId(int idHocVien)
        {
            return luanVanDAL.GetLuanVanByHocVienId(idHocVien);
        }
        public bool XoaToanBoLuanVan(int idHocVien)
        {
            return luanVanDAL.XoaToanBoLuanVanByHocVienId(idHocVien);
        }
    }
}
