using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RoleServices.Models;

namespace RoleServices.Data;

public partial class RoleDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public RoleDbContext()
    {
    }

    public RoleDbContext(DbContextOptions<RoleDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<ChucNang> ChucNangs { get; set; }

    public virtual DbSet<ChucVu2> ChucVu2s { get; set; }

    public virtual DbSet<DonVi> DonVis { get; set; }

    public virtual DbSet<HanhDong> HanhDongs { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TaiKhoanChucVu> TaiKhoanChucVus { get; set; }

    public virtual DbSet<TaiKhoanPhanQuyen> TaiKhoanPhanQuyens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
         => optionsBuilder.UseSqlServer("Data Source=MYLAB\\NGUYEN;Initial Catalog=OnlineShop;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
       // => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AwsDB"));
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChucNang>(entity =>
        {
            entity.HasKey(e => e.MaChucNang).HasName("PK__ChucNang__B26DC257C32A6B52");

            entity.ToTable("ChucNang");

            entity.Property(e => e.TenChucNang)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ChucVu2>(entity =>
        {
            entity.HasKey(e => e.MaChucVu).HasName("PK__ChucVu2__D4639533AC1CD1B0");

            entity.ToTable("ChucVu2");

            entity.Property(e => e.TenChucVu)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DonVi>(entity =>
        {
            entity.HasKey(e => e.MaDonVi).HasName("PK__DonVi__DDA5A6CF896155EF");

            entity.ToTable("DonVi");

            entity.Property(e => e.TenDonVi)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.MaDonViChaNavigation).WithMany(p => p.InverseMaDonViChaNavigation)
                .HasForeignKey(d => d.MaDonViCha)
                .HasConstraintName("FK__DonVi__MaDonViCh__160F4887");
        });

        modelBuilder.Entity<HanhDong>(entity =>
        {
            entity.HasKey(e => e.MaHanhDong).HasName("PK__HanhDong__F0C73927D8BE5BA2");

            entity.ToTable("HanhDong");

            entity.Property(e => e.TenHanhDong)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PhanQuyen>(entity =>
        {
            entity.HasKey(e => e.MaPhanQuyen).HasName("PK__PhanQuye__529AB12BAA6B1333");

            entity.ToTable("PhanQuyen");

            entity.HasOne(d => d.MaChucNangNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.MaChucNang)
                .HasConstraintName("FK__PhanQuyen__MaChu__236943A5");

            entity.HasOne(d => d.MaChucVuNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.MaChucVu)
                .HasConstraintName("FK__PhanQuyen__MaChu__22751F6C");

            entity.HasOne(d => d.MaDonViNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.MaDonVi)
                .HasConstraintName("FK__PhanQuyen__MaDon__25518C17");

            entity.HasOne(d => d.MaHanhDongNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.MaHanhDong)
                .HasConstraintName("FK__PhanQuyen__MaHan__245D67DE");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AE26BA8FE");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160B39AB150").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<TaiKhoanChucVu>(entity =>
        {
            entity.HasKey(e => new { e.MaTaiKhoan, e.MaChucVu }).HasName("PK__TaiKhoan__803A5C7AC05A7FF7");

            entity.ToTable("TaiKhoan_ChucVu");

            entity.Property(e => e.Ten)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.MaChucVuNavigation).WithMany(p => p.TaiKhoanChucVus)
                .HasForeignKey(d => d.MaChucVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoan___MaChu__1BC821DD");

            entity.HasOne(d => d.MaTaiKhoanNavigation).WithMany(p => p.TaiKhoanChucVus)
                .HasForeignKey(d => d.MaTaiKhoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaiKhoan_ChucVu_TAIKHOAN1");
        });

        modelBuilder.Entity<TaiKhoanPhanQuyen>(entity =>
        {
            entity.HasKey(e => new { e.MaTaiKhoan, e.MaChucNang, e.MaHanhDong, e.MaDonVi }).HasName("PK__TaiKhoan__8C47A46FAC8E91FA");

            entity.ToTable("TaiKhoan_PhanQuyen");

            entity.HasOne(d => d.MaChucNangNavigation).WithMany(p => p.TaiKhoanPhanQuyens)
                .HasForeignKey(d => d.MaChucNang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoan___MaChu__29221CFB");

            entity.HasOne(d => d.MaDonViNavigation).WithMany(p => p.TaiKhoanPhanQuyens)
                .HasForeignKey(d => d.MaDonVi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoan___MaDon__2B0A656D");

            entity.HasOne(d => d.MaHanhDongNavigation).WithMany(p => p.TaiKhoanPhanQuyens)
                .HasForeignKey(d => d.MaHanhDong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoan___MaHan__2A164134");

            entity.HasOne(d => d.MaTaiKhoanNavigation).WithMany(p => p.TaiKhoanPhanQuyens)
                .HasForeignKey(d => d.MaTaiKhoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaiKhoan_PhanQuyen_TAIKHOAN");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.MaTaiKhoan);

            entity.ToTable("TAIKHOAN");

            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(20);
            entity.Property(e => e.MaCv).HasColumnName("MaCV");
            entity.Property(e => e.MatKhau).HasMaxLength(100);
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("SDT");
            entity.Property(e => e.Ten).HasMaxLength(30);

            //entity.HasOne(d => d.MaCvNavigation).WithMany(p => p.Taikhoans)
            //    .HasForeignKey(d => d.MaCv)
            //    .HasConstraintName("FK_TAIKHOAN_ChucVu");

            entity.HasOne(d => d.MaDonViNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.MaDonVi)
                .HasConstraintName("FK_TAIKHOAN_DonVi");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("PK__UserRole__AF2760ADC2DA84DC");

            entity.Property(e => e.Ten)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserRoles__RoleI__06CD04F7");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRoles__UserI__05D8E0BE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
