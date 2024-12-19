﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BienLaiDTO
    {
        public int Id { get; set; }
        public int IdHocVien { get; set; }
        public string SoBienLai { get; set; }
        public DateTime NgayPhatHanh { get; set; }
        public decimal SoTien { get; set; }
    }
}
