using System;
using MySql.Data.MySqlClient;
using DTO;
using System.Data;


namespace DAL
{
    public class NguoiSuDungDAL
    {
        private string connectionString = "server=localhost;user=root;database=quan_ly_hoc_phi;port=3306;password=";

        // Phương thức kiểm tra đăng nhập
        public bool DangNhap(string tenDangNhap, string matKhau)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM nguoi_su_dung WHERE ten_dang_nhap = @TenDangNhap AND mat_khau = @MatKhau";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                MySqlDataReader reader = cmd.ExecuteReader();

                // Nếu tìm thấy bản ghi, tức là đăng nhập thành công
                return reader.HasRows;
            }
        }

        // Phương thức lấy quyền của người dùng
        public string LayQuyenNguoiSuDung(string tenDangNhap)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT quyen FROM phan_quyen pq
                                 JOIN nguoi_su_dung nsd ON pq.id_nguoi_su_dung = nsd.id
                                 WHERE nsd.ten_dang_nhap = @TenDangNhap";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                return (string)cmd.ExecuteScalar();
            }
        }
        public TaiKhoan LayThongTinNguoiDung(string tenDangNhap)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, ten_dang_nhap,mat_khau, email, vai_tro FROM nguoi_su_dung WHERE ten_dang_nhap = @TenDangNhap";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TaiKhoan
                        {
                            Id = reader.GetInt32("id"),
                            TenDangNhap = reader.GetString("ten_dang_nhap"),
                            MatKhau = reader.GetString("mat_khau"),
                            Email = reader.GetString("email"),
                            VaiTro = reader.GetString("vai_tro"),
                        };
                    }
                }
            }
            return null; // Trả về null nếu không tìm thấy người dùng
        }
        public SoYeuLL laythongtinhocvien(int mhv)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM hoc_vien WHERE id = @mhv";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@mhv", mhv);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new DTO.SoYeuLL
                        {
                            Id = reader.GetInt32("id"),
                            HoTen = reader.GetString("ho_ten"),
                            NgaySinh = reader.GetDateTime("ngay_sinh"),
                            GioiTinh = reader.GetString("gioi_tinh"),
                            DiaChi = reader.GetString("dia_chi"),
                            DienThoai = reader.GetString("so_dien_thoai"),
                            Email = reader.GetString("email"),
                            NgayNhapHoc = reader.GetDateTime("ngay_nhap_hoc"),
                            MaChuyenNganh = reader.GetInt32("ma_chuyen_nganh"),
                            MaHeDaoTao = reader.GetInt32("ma_he_dao_tao"),
                        };
                    }
                }
            }
            return null;
        }
        public List<LuanVan> LayLuanVanTheoHocVien(int idHocVien)
        {
            List<LuanVan> luanVanList = new List<LuanVan>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM luan_van_luan_an WHERE id_hoc_vien = @IdHocVien";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdHocVien", idHocVien);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LuanVan luanVan = new LuanVan
                        {
                            Id = reader.GetInt32("id"),
                            Idhv = reader.GetInt32("id_hoc_vien"),
                            TenLuanDe = reader.GetString("ten_luan_van"),
                            Tien = reader.GetDecimal("so_tien"),
                            NgayNop = reader.GetDateTime("ngay_thanh_toan")
                        };
                        luanVanList.Add(luanVan);
                    }
                }
            }
            return luanVanList;
        }
        public decimal TinhTongTienLuanVan(int idHocVien)
        {
            decimal tongTien = 0;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT SUM(so_tien) FROM luan_van_luan_an WHERE id_hoc_vien = @IdHocVien";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdHocVien", idHocVien);

                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    tongTien = Convert.ToDecimal(result);
                }
            }
            return tongTien;
        }
        public bool ThemLichSuThuPhi(int idHocVien, decimal soTien, DateTime ngayNop, string noiDung = "Đã thanh toán thành công")
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO lich_su_thu_phi (id_hoc_vien, so_tien, ngay_nop, noi_dung) VALUES (@IdHocVien, @SoTien, @NgayNop, @NoiDung)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdHocVien", idHocVien);
                cmd.Parameters.AddWithValue("@SoTien", soTien);
                cmd.Parameters.AddWithValue("@NgayNop", ngayNop);
                cmd.Parameters.AddWithValue("@NoiDung", noiDung);

                return cmd.ExecuteNonQuery() > 0; // Trả về true nếu thêm thành công
            }
        }
        public bool XoaLuanVan(int idLuanVan)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM luan_van_luan_an WHERE id = @IdLuanVan";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdLuanVan", idLuanVan);

                return cmd.ExecuteNonQuery() > 0; // Trả về true nếu xóa thành công
            }
        }
        public DataTable GetLuanVanByHocVienId(int idHocVien)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM luan_van_luan_an WHERE id_hoc_vien = @IdHocVien";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdHocVien", idHocVien);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public bool XoaToanBoLuanVanByHocVienId(int idHocVien)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM luan_van_luan_an WHERE id_hoc_vien = @IdHocVien";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdHocVien", idHocVien);

                return cmd.ExecuteNonQuery() > 0; // Trả về true nếu có ít nhất một dòng bị xóa
            }
        }
        public List<LichSuThuPhiDTO> GetLichSuThuPhiByIdHocVien(int idHocVien)
        {
            List<LichSuThuPhiDTO> lichSuList = new List<LichSuThuPhiDTO>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM lich_su_thu_phi WHERE id_hoc_vien = @IdHocVien";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdHocVien", idHocVien);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LichSuThuPhiDTO lichSu = new LichSuThuPhiDTO
                        {
                            Id = reader.GetInt32("id"),
                            IdHocVien = reader.GetInt32("id_hoc_vien"),
                            SoTien = reader.GetDecimal("so_tien"),
                            NgayNop = reader.GetDateTime("ngay_nop"),
                            NoiDung = reader.GetString("noi_dung")
                        };
                        lichSuList.Add(lichSu);
                    }
                }
            }
            return lichSuList;
        }
        public bool InsertBienLai(BienLaiDTO bienLai)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO bien_lai (id_hoc_vien, so_bien_lai, ngay_phat_hanh, so_tien) VALUES (@IdHocVien, @SoBienLai, @NgayPhatHanh, @SoTien)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdHocVien", bienLai.IdHocVien);
                cmd.Parameters.AddWithValue("@SoBienLai", bienLai.SoBienLai);
                cmd.Parameters.AddWithValue("@NgayPhatHanh", bienLai.NgayPhatHanh);
                cmd.Parameters.AddWithValue("@SoTien", bienLai.SoTien);

                return cmd.ExecuteNonQuery() > 0; // Trả về true nếu thêm thành công
            }
        }
    }
}
