﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LichSuThuPhiDTO
    {
        public int Id { get; set; }
        public int IdHocVien { get; set; }
        public decimal SoTien { get; set; }
        public DateTime NgayNop { get; set; }
        public string NoiDung { get; set; }
    }
}
