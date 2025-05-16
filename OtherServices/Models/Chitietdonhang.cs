using System;
using System.Collections.Generic;

namespace OtherServices.Models;

public partial class Chitietdonhang
{
    public int MaDonHang { get; set; }

    public int MaSp { get; set; }

    public int? SoLuongMua { get; set; }

    public virtual Donhang MaDonHangNavigation { get; set; } = null!;

    public virtual Sanpham MaSpNavigation { get; set; } = null!;
}
