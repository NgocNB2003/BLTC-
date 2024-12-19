using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LuanVan
    {
        public int Id { get; set; }
        public int Idhv { get; set; }
        public string TenLuanDe { get; set; }
        public decimal Tien { get; set; }
        public DateTime NgayNop { get; set; }
    }
}
