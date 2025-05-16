using System;
using System.Collections.Generic;

namespace ProductServices.Models;

public partial class Danhmucsanpham
{
    public int MaDanhMuc { get; set; }

    public string? TenDanhMuc { get; set; }

    public virtual ICollection<Sanpham> Sanphams { get; set; } = new List<Sanpham>();
}
