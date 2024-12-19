﻿using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BienLaiBLL
    {
        private NguoiSuDungDAL dal = new NguoiSuDungDAL();

        public bool ThemBienLai(BienLaiDTO bienLai)
        {
            return dal.InsertBienLai(bienLai);
        }
    }
}
