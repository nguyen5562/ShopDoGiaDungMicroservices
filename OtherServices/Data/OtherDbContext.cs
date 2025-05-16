﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OtherServices.Models;

namespace OtherServices.Data;

public partial class OtherDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public OtherDbContext()
    {
    }

    public OtherDbContext(DbContextOptions<OtherDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Chitietdonhang> Chitietdonhangs { get; set; }

    public virtual DbSet<Danhgiasanpham> Danhgiasanphams { get; set; }

    public virtual DbSet<Danhmucsanpham> Danhmucsanphams { get; set; }

    public virtual DbSet<DonVi> DonVis { get; set; }

    public virtual DbSet<Donhang> Donhangs { get; set; }

    public virtual DbSet<Hangsanxuat> Hangsanxuats { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<Sanpham> Sanphams { get; set; }

    public virtual DbSet<TaiKhoanChucVu> TaiKhoanChucVus { get; set; }

    public virtual DbSet<TaiKhoanPhanQuyen> TaiKhoanPhanQuyens { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Vanchuyen> Vanchuyens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
         => optionsBuilder.UseSqlServer("Data Source=MYLAB\\NGUYEN;Initial Catalog=OnlineShop;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
       // => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AwsDB"));
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chitietdonhang>(entity =>
        {
            entity.HasKey(e => new { e.MaDonHang, e.MaSp });

            entity.ToTable("CHITIETDONHANG");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.Chitietdonhangs)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK_CHITIETDONHANG_DONHANG");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.Chitietdonhangs)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK_CHITIETDONHANG_SANPHAM");
        });

        modelBuilder.Entity<ChucNang>(entity =>
        {
            entity.HasKey(e => e.MaChucNang).HasName("PK__ChucNang__B26DC257C32A6B52");

            entity.ToTable("ChucNang");

            entity.Property(e => e.TenChucNang)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Danhgiasanpham>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DANHGIASANPHAM");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.NoiDungBinhLuan).HasMaxLength(200);

            entity.HasOne(d => d.MaSpNavigation).WithMany()
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK_DANHGIASANPHAM_SANPHAM");

            entity.HasOne(d => d.MaTaiKhoanNavigation).WithMany()
                .HasForeignKey(d => d.MaTaiKhoan)
                .HasConstraintName("FK_DANHGIASANPHAM_TAIKHOAN");
        });

        modelBuilder.Entity<Danhmucsanpham>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc);

            entity.ToTable("DANHMUCSANPHAM");

            entity.Property(e => e.TenDanhMuc).HasMaxLength(30);
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

        modelBuilder.Entity<Donhang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang);

            entity.ToTable("DONHANG");

            entity.HasOne(d => d.MaTaiKhoanNavigation).WithMany(p => p.Donhangs)
                .HasForeignKey(d => d.MaTaiKhoan)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DONHANG_TAIKHOAN");
        });

        modelBuilder.Entity<Hangsanxuat>(entity =>
        {
            entity.HasKey(e => e.MaHang);

            entity.ToTable("HANGSANXUAT");

            entity.Property(e => e.TenHang).HasMaxLength(20);
        });

        modelBuilder.Entity<HanhDong>(entity =>
        {
            entity.HasKey(e => e.MaHanhDong).HasName("PK__HanhDong__F0C73927D8BE5BA2");

            entity.ToTable("HanhDong");

            entity.Property(e => e.TenHanhDong)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .HasConstraintName("FK_Message_TAIKHOAN1");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK_Message_TAIKHOAN");
        });

        modelBuilder.Entity<PhanQuyen>(entity =>
        {
            entity.HasKey(e => e.MaPhanQuyen).HasName("PK__PhanQuye__529AB12BAA6B1333");

            entity.ToTable("PhanQuyen");

            entity.HasOne(d => d.MaChucNangNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.MaChucNang)
                .HasConstraintName("FK__PhanQuyen__MaChu__236943A5");

            //entity.HasOne(d => d.MaChucVuNavigation).WithMany(p => p.PhanQuyens)
            //    .HasForeignKey(d => d.MaChucVu)
            //    .HasConstraintName("FK__PhanQuyen__MaChu__22751F6C");

            entity.HasOne(d => d.MaDonViNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.MaDonVi)
                .HasConstraintName("FK__PhanQuyen__MaDon__25518C17");

            entity.HasOne(d => d.MaHanhDongNavigation).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.MaHanhDong)
                .HasConstraintName("FK__PhanQuyen__MaHan__245D67DE");
        });

        modelBuilder.Entity<Sanpham>(entity =>
        {
            entity.HasKey(e => e.MaSp);

            entity.ToTable("SANPHAM");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.Anh1).HasMaxLength(100);
            entity.Property(e => e.Anh2).HasMaxLength(100);
            entity.Property(e => e.Anh3).HasMaxLength(100);
            entity.Property(e => e.Anh4).HasMaxLength(100);
            entity.Property(e => e.Anh5).HasMaxLength(100);
            entity.Property(e => e.Anh6).HasMaxLength(100);
            entity.Property(e => e.MoTa).HasMaxLength(1000);
            entity.Property(e => e.TenSp)
                .HasMaxLength(100)
                .HasColumnName("TenSP");

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.MaDanhMuc)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SANPHAM_DANHMUCSANPHAM");

            entity.HasOne(d => d.MaHangNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.MaHang)
                .HasConstraintName("FK_SANPHAM_HANGSANXUAT");
        });

        modelBuilder.Entity<TaiKhoanChucVu>(entity =>
        {
            entity.HasKey(e => new { e.MaTaiKhoan, e.MaChucVu }).HasName("PK__TaiKhoan__803A5C7AC05A7FF7");

            entity.ToTable("TaiKhoan_ChucVu");

            entity.Property(e => e.Ten)
                .HasMaxLength(10)
                .IsFixedLength();

            //entity.HasOne(d => d.MaChucVuNavigation).WithMany(p => p.TaiKhoanChucVus)
            //    .HasForeignKey(d => d.MaChucVu)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__TaiKhoan___MaChu__1BC821DD");

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

        modelBuilder.Entity<Vanchuyen>(entity =>
        {
            entity.HasKey(e => e.MaDonHang);

            entity.ToTable("VANCHUYEN");

            entity.Property(e => e.MaDonHang).ValueGeneratedNever();
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.HinhThucVanChuyen).HasMaxLength(20);
            entity.Property(e => e.NguoiNhan).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");

            entity.HasOne(d => d.MaDonHangNavigation).WithOne(p => p.Vanchuyen)
                .HasForeignKey<Vanchuyen>(d => d.MaDonHang)
                .HasConstraintName("FK_VANCHUYEN_DONHANG");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
