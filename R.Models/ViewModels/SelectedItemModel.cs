﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R.Models.ViewModels.BaseModels;

namespace R.Models.ViewModels
{
    public class SelectedItemModel:BaseInputModel
    {
        public long? NumberId { get; set; }
        public string? StringId { get; set; }
    }
}
