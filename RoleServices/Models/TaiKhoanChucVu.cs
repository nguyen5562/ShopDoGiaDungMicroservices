using System;
using System.Collections.Generic;

namespace RoleServices.Models;

public partial class TaiKhoanChucVu
{
    public int MaTaiKhoan { get; set; }

    public int MaChucVu { get; set; }

    public string? Ten { get; set; }

    public virtual ChucVu2 MaChucVuNavigation { get; set; } = null!;

    public virtual Taikhoan MaTaiKhoanNavigation { get; set; } = null!;
}
