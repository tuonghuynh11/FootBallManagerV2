using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Entities;

public partial class FootBallManagerV2Context : DbContext
{
    public FootBallManagerV2Context()
    {
    }

    public FootBallManagerV2Context(DbContextOptions<FootBallManagerV2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cauthu> Cauthus { get; set; }

    public virtual DbSet<Chuyennhuong> Chuyennhuongs { get; set; }

    public virtual DbSet<Diadiem> Diadiems { get; set; }

    public virtual DbSet<Diem> Diems { get; set; }

    public virtual DbSet<Doibong> Doibongs { get; set; }

    public virtual DbSet<Doihinhchinh> Doihinhchinhs { get; set; }

    public virtual DbSet<Field> Fields { get; set; }

    public virtual DbSet<Fieldservice> Fieldservices { get; set; }

    public virtual DbSet<Footballmatch> Footballmatches { get; set; }

    public virtual DbSet<Huanluyenvien> Huanluyenviens { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Itemtype> Itemtypes { get; set; }

    public virtual DbSet<League> Leagues { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }

    public virtual DbSet<Quoctich> Quoctiches { get; set; }

    public virtual DbSet<Round> Rounds { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Tapluyen> Tapluyens { get; set; }

    public virtual DbSet<Teamofleague> Teamofleagues { get; set; }

    public virtual DbSet<Thamgium> Thamgia { get; set; }

    public virtual DbSet<Thongtingiaidau> Thongtingiaidaus { get; set; }

    public virtual DbSet<Thongtintrandau> Thongtintrandaus { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userrole> Userroles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress;Initial Catalog=FootBallManagerV2;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cauthu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAUTHU__3214EC27069414EF");

            entity.ToTable("CAUTHU");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cannang)
                .HasMaxLength(30)
                .HasColumnName("CANNANG");
            entity.Property(e => e.Chanthuan)
                .HasMaxLength(30)
                .HasColumnName("CHANTHUAN");
            entity.Property(e => e.Chieucao)
                .HasMaxLength(30)
                .HasColumnName("CHIEUCAO");
            entity.Property(e => e.Giatricauthu)
                .HasDefaultValueSql("((0))")
                .HasColumnName("GIATRICAUTHU");
            entity.Property(e => e.Hinhanh)
                .HasColumnType("image")
                .HasColumnName("HINHANH");
            entity.Property(e => e.Hoten)
                .HasMaxLength(100)
                .HasColumnName("HOTEN");
            entity.Property(e => e.Iddoibong)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IDDOIBONG");
            entity.Property(e => e.Idquoctich).HasColumnName("IDQUOCTICH");
            entity.Property(e => e.Soao).HasColumnName("SOAO");
            entity.Property(e => e.Sobanthang).HasColumnName("SOBANTHANG");
            entity.Property(e => e.Sogiai).HasColumnName("SOGIAI");
            entity.Property(e => e.Thetrang)
                .HasMaxLength(30)
                .HasColumnName("THETRANG");
            entity.Property(e => e.Tuoi).HasColumnName("TUOI");
            entity.Property(e => e.Vitri)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("VITRI");

            entity.HasOne(d => d.IddoibongNavigation).WithMany(p => p.Cauthus)
                .HasForeignKey(d => d.Iddoibong)
                .HasConstraintName("FK_CT1");

            entity.HasOne(d => d.IdquoctichNavigation).WithMany(p => p.Cauthus)
                .HasForeignKey(d => d.Idquoctich)
                .HasConstraintName("FK_CT2");
        });

        modelBuilder.Entity<Chuyennhuong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CHUYENNH__3214EC27AC306685");

            entity.ToTable("CHUYENNHUONG");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idcauthu).HasColumnName("IDCAUTHU");
            entity.Property(e => e.Iddoimua)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IDDOIMUA");

            entity.HasOne(d => d.IdcauthuNavigation).WithMany(p => p.Chuyennhuongs)
                .HasForeignKey(d => d.Idcauthu)
                .HasConstraintName("FK_CN1");

            entity.HasOne(d => d.IddoimuaNavigation).WithMany(p => p.Chuyennhuongs)
                .HasForeignKey(d => d.Iddoimua)
                .HasConstraintName("FK_CN2");
        });

        modelBuilder.Entity<Diadiem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DIADIEM__3214EC2747645FC8");

            entity.ToTable("DIADIEM");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idquocgia).HasColumnName("IDQUOCGIA");
            entity.Property(e => e.Tendiadiem)
                .HasMaxLength(100)
                .HasColumnName("TENDIADIEM");

            entity.HasOne(d => d.IdquocgiaNavigation).WithMany(p => p.Diadiems)
                .HasForeignKey(d => d.Idquocgia)
                .HasConstraintName("FK_1");
        });

        modelBuilder.Entity<Diem>(entity =>
        {
            entity.HasKey(e => new { e.Iddoibong, e.Idgiaidau }).HasName("PK__DIEM__3EE06242DA2AF470");

            entity.ToTable("DIEM");

            entity.Property(e => e.Iddoibong)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IDDOIBONG");
            entity.Property(e => e.Idgiaidau).HasColumnName("IDGIAIDAU");
            entity.Property(e => e.Sodiem).HasColumnName("SODIEM");

            entity.HasOne(d => d.IddoibongNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.Iddoibong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_D1");
        });

        modelBuilder.Entity<Doibong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DOIBONG__3214EC278B84E431");

            entity.ToTable("DOIBONG");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Giatri)
                .HasDefaultValueSql("((0))")
                .HasColumnName("GIATRI");
            entity.Property(e => e.Hinhanh)
                .HasColumnType("image")
                .HasColumnName("HINHANH");
            entity.Property(e => e.Idquoctich).HasColumnName("IDQUOCTICH");
            entity.Property(e => e.Ngaythanhlap)
                .HasColumnType("date")
                .HasColumnName("NGAYTHANHLAP");
            entity.Property(e => e.Sannha)
                .HasMaxLength(100)
                .HasColumnName("SANNHA");
            entity.Property(e => e.Sodochienthuat)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('4-3-3')")
                .HasColumnName("SODOCHIENTHUAT");
            entity.Property(e => e.Soluongthanhvien).HasColumnName("SOLUONGTHANHVIEN");
            entity.Property(e => e.Ten)
                .HasMaxLength(100)
                .HasColumnName("TEN");
            entity.Property(e => e.Thanhpho).HasColumnName("THANHPHO");

            entity.HasOne(d => d.IdquoctichNavigation).WithMany(p => p.Doibongs)
                .HasForeignKey(d => d.Idquoctich)
                .HasConstraintName("FK_DB2");

            entity.HasOne(d => d.ThanhphoNavigation).WithMany(p => p.Doibongs)
                .HasForeignKey(d => d.Thanhpho)
                .HasConstraintName("FK_DB3");
        });

        modelBuilder.Entity<Doihinhchinh>(entity =>
        {
            entity.HasKey(e => new { e.Iddoibong, e.Idcauthu });

            entity.ToTable("DOIHINHCHINH");

            entity.Property(e => e.Iddoibong)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IDDOIBONG");
            entity.Property(e => e.Idcauthu).HasColumnName("IDCAUTHU");
            entity.Property(e => e.Vaitro)
                .HasMaxLength(50)
                .HasColumnName("VAITRO");
            entity.Property(e => e.Vitri)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("VITRI");

            entity.HasOne(d => d.IdcauthuNavigation).WithMany(p => p.Doihinhchinhs)
                .HasForeignKey(d => d.Idcauthu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DHC2");

            entity.HasOne(d => d.IddoibongNavigation).WithMany(p => p.Doihinhchinhs)
                .HasForeignKey(d => d.Iddoibong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DHC1");
        });

        modelBuilder.Entity<Field>(entity =>
        {
            entity.HasKey(e => e.IdField).HasName("PR_FIELD");

            entity.ToTable("FIELD");

            entity.Property(e => e.IdField).HasColumnName("idField");
            entity.Property(e => e.FieldName)
                .HasMaxLength(300)
                .HasColumnName("fieldName");
            entity.Property(e => e.IdDiaDiem).HasColumnName("idDiaDiem");
            entity.Property(e => e.Images)
                .IsUnicode(false)
                .HasColumnName("images");
            entity.Property(e => e.NumOfSeats).HasColumnName("numOfSeats");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TechnicalInformation)
                .HasColumnType("text")
                .HasColumnName("technicalInformation");

            entity.HasOne(d => d.IdDiaDiemNavigation).WithMany(p => p.Fields)
                .HasForeignKey(d => d.IdDiaDiem)
                .HasConstraintName("fk_field_01");
        });

        modelBuilder.Entity<Fieldservice>(entity =>
        {
            entity.HasKey(e => new { e.IdField, e.IdService }).HasName("PR_FIELDSERVICE");

            entity.ToTable("FIELDSERVICE");

            entity.Property(e => e.IdField).HasColumnName("idField");
            entity.Property(e => e.IdService).HasColumnName("idService");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.IdFieldNavigation).WithMany(p => p.Fieldservices)
                .HasForeignKey(d => d.IdField)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_fieldservice_01");

            entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.Fieldservices)
                .HasForeignKey(d => d.IdService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_fieldservice_02");
        });

        modelBuilder.Entity<Footballmatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FOOTBALL__3214EC271227EF18");

            entity.ToTable("FOOTBALLMATCH");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Diadiem).HasColumnName("DIADIEM");
            entity.Property(e => e.Idvong).HasColumnName("IDVONG");
            entity.Property(e => e.Tentrandau).HasColumnName("TENTRANDAU");
            entity.Property(e => e.Thoigian)
                .HasColumnType("smalldatetime")
                .HasColumnName("THOIGIAN");
            entity.Property(e => e.Vongbang).HasColumnName("VONGBANG");

            entity.HasOne(d => d.DiadiemNavigation).WithMany(p => p.Footballmatches)
                .HasForeignKey(d => d.Diadiem)
                .HasConstraintName("fk_fbm_02");

            entity.HasOne(d => d.IdvongNavigation).WithMany(p => p.Footballmatches)
                .HasForeignKey(d => d.Idvong)
                .HasConstraintName("fk_fbm_01");
        });

        modelBuilder.Entity<Huanluyenvien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HUANLUYE__3214EC2727CF6E7F");

            entity.ToTable("HUANLUYENVIEN");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Chucvu)
                .HasMaxLength(50)
                .HasColumnName("CHUCVU");
            entity.Property(e => e.Gmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("GMAIL");
            entity.Property(e => e.Hinhanh)
                .HasColumnType("image")
                .HasColumnName("HINHANH");
            entity.Property(e => e.Hoten)
                .HasMaxLength(100)
                .HasColumnName("HOTEN");
            entity.Property(e => e.Iddoibong)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IDDOIBONG");
            entity.Property(e => e.Idquoctich).HasColumnName("IDQUOCTICH");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("date")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Tuoi).HasColumnName("TUOI");

            entity.HasOne(d => d.IddoibongNavigation).WithMany(p => p.Huanluyenviens)
                .HasForeignKey(d => d.Iddoibong)
                .HasConstraintName("FK_DB1");

            entity.HasOne(d => d.IdquoctichNavigation).WithMany(p => p.Huanluyenviens)
                .HasForeignKey(d => d.Idquoctich)
                .HasConstraintName("FK_HLV1");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ITEM__3214EC2770AFC74B");

            entity.ToTable("ITEM");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idcauthu).HasColumnName("IDCAUTHU");
            entity.Property(e => e.Iddoibong)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IDDOIBONG");
            entity.Property(e => e.Iditemtype).HasColumnName("IDITEMTYPE");
            entity.Property(e => e.Idthongtintrandau).HasColumnName("IDTHONGTINTRANDAU");
            entity.Property(e => e.Thoigian).HasColumnName("THOIGIAN");

            entity.HasOne(d => d.IdcauthuNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.Idcauthu)
                .HasConstraintName("FK_ITEM_THEM_01");

            entity.HasOne(d => d.IddoibongNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.Iddoibong)
                .HasConstraintName("fk_item_02");

            entity.HasOne(d => d.IditemtypeNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.Iditemtype)
                .HasConstraintName("fk_item_03");

            entity.HasOne(d => d.IdthongtintrandauNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.Idthongtintrandau)
                .HasConstraintName("fk_item_01");
        });

        modelBuilder.Entity<Itemtype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ITEMTYPE__3214EC27AB4D8E06");

            entity.ToTable("ITEMTYPE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Tenitem).HasColumnName("TENITEM");
        });

        modelBuilder.Entity<League>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LEAGUE__3214EC275E61BAF4");

            entity.ToTable("LEAGUE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Hinhanh)
                .HasColumnType("image")
                .HasColumnName("HINHANH");
            entity.Property(e => e.Idquocgia).HasColumnName("IDQUOCGIA");
            entity.Property(e => e.Ngaybatdau)
                .HasColumnType("date")
                .HasColumnName("NGAYBATDAU");
            entity.Property(e => e.Ngayketthuc)
                .HasColumnType("date")
                .HasColumnName("NGAYKETTHUC");
            entity.Property(e => e.Tengiaidau).HasColumnName("TENGIAIDAU");

            entity.HasOne(d => d.IdquocgiaNavigation).WithMany(p => p.Leagues)
                .HasForeignKey(d => d.Idquocgia)
                .HasConstraintName("fk_league_01");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC27CA71BBCC");

            entity.ToTable("Notification");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Checked)
                .HasMaxLength(50)
                .HasColumnName("CHECKED");
            entity.Property(e => e.Idhlv).HasColumnName("IDHLV");
            entity.Property(e => e.Notify).HasColumnName("NOTIFY");

            entity.HasOne(d => d.IdhlvNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Idhlv)
                .HasConstraintName("FK_TB1");
        });

        modelBuilder.Entity<Otp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OTP__3214EC0792811241");

            entity.ToTable("OTP");

            entity.Property(e => e.Time)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Quoctich>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__QUOCTICH__3214EC278FF1125B");

            entity.ToTable("QUOCTICH");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Tenquocgia)
                .HasMaxLength(100)
                .HasColumnName("TENQUOCGIA");
        });

        modelBuilder.Entity<Round>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROUND__3214EC277E3F2B1E");

            entity.ToTable("ROUND");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Iddisplay)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IDDISPLAY");
            entity.Property(e => e.Idgiaidau).HasColumnName("IDGIAIDAU");
            entity.Property(e => e.Ngaybatdau)
                .HasColumnType("smalldatetime")
                .HasColumnName("NGAYBATDAU");
            entity.Property(e => e.Soluongdoi).HasColumnName("SOLUONGDOI");
            entity.Property(e => e.Tenvongdau).HasColumnName("TENVONGDAU");

            entity.HasOne(d => d.IdgiaidauNavigation).WithMany(p => p.Rounds)
                .HasForeignKey(d => d.Idgiaidau)
                .HasConstraintName("fk_round_01");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.IdService).HasName("PR_SERVICES");

            entity.ToTable("SERVICES");

            entity.Property(e => e.IdService).HasColumnName("idService");
            entity.Property(e => e.Detail)
                .HasColumnType("text")
                .HasColumnName("detail");
            entity.Property(e => e.Images)
                .IsUnicode(false)
                .HasColumnName("images");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(300)
                .HasColumnName("serviceName");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.IdSupplier).HasName("PR_SUPPLIER");

            entity.ToTable("SUPPLIER");

            entity.Property(e => e.IdSupplier).HasColumnName("idSupplier");
            entity.Property(e => e.Addresss)
                .HasMaxLength(300)
                .HasColumnName("addresss");
            entity.Property(e => e.EstablishDate)
                .HasColumnType("date")
                .HasColumnName("establishDate");
            entity.Property(e => e.Images)
                .IsUnicode(false)
                .HasColumnName("images");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.RepresentativeName)
                .HasMaxLength(100)
                .HasColumnName("representativeName");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(300)
                .HasColumnName("supplierName");

            entity.HasMany(d => d.IdServices).WithMany(p => p.IdSuppliers)
                .UsingEntity<Dictionary<string, object>>(
                    "Supplierservice",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("IdService")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_supplierservice_02"),
                    l => l.HasOne<Supplier>().WithMany()
                        .HasForeignKey("IdSupplier")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_supplierservice_01"),
                    j =>
                    {
                        j.HasKey("IdSupplier", "IdService").HasName("PR_SUPPLIERSERVICE");
                        j.ToTable("SUPPLIERSERVICE");
                        j.IndexerProperty<int>("IdSupplier").HasColumnName("idSupplier");
                        j.IndexerProperty<int>("IdService").HasColumnName("idService");
                    });
        });

        modelBuilder.Entity<Tapluyen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TAPLUYEN__3214EC2748EE31DB");

            entity.ToTable("TAPLUYEN");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Ghichu).HasColumnName("GHICHU");
            entity.Property(e => e.Hoatdong)
                .HasMaxLength(100)
                .HasColumnName("HOATDONG");
            entity.Property(e => e.Iddoibong)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IDDOIBONG");
            entity.Property(e => e.Idnguoiquanly).HasColumnName("IDNGUOIQUANLY");
            entity.Property(e => e.Thoigianbatdau)
                .HasColumnType("smalldatetime")
                .HasColumnName("THOIGIANBATDAU");
            entity.Property(e => e.Thoigianketthuc)
                .HasColumnType("smalldatetime")
                .HasColumnName("THOIGIANKETTHUC");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(20)
                .HasColumnName("TRANGTHAI");

            entity.HasOne(d => d.IddoibongNavigation).WithMany(p => p.Tapluyens)
                .HasForeignKey(d => d.Iddoibong)
                .HasConstraintName("FK_TL2");

            entity.HasOne(d => d.IdnguoiquanlyNavigation).WithMany(p => p.Tapluyens)
                .HasForeignKey(d => d.Idnguoiquanly)
                .HasConstraintName("FK_TL1");
        });

        modelBuilder.Entity<Teamofleague>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TEAMOFLE__3214EC276D00DC18");

            entity.ToTable("TEAMOFLEAGUE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Iddoibong)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IDDOIBONG");
            entity.Property(e => e.Idgiaidau).HasColumnName("IDGIAIDAU");

            entity.HasOne(d => d.IddoibongNavigation).WithMany(p => p.Teamofleagues)
                .HasForeignKey(d => d.Iddoibong)
                .HasConstraintName("FK_TEAMOFLEAGUE_02");

            entity.HasOne(d => d.IdgiaidauNavigation).WithMany(p => p.Teamofleagues)
                .HasForeignKey(d => d.Idgiaidau)
                .HasConstraintName("FK_TEAMOFLEAGUE_01");
        });

        modelBuilder.Entity<Thamgium>(entity =>
        {
            entity.HasKey(e => new { e.Idtran, e.Idcauthu }).HasName("PK__THAMGIA__ED8AA057AFEFB5D6");

            entity.ToTable("THAMGIA");

            entity.Property(e => e.Idtran).HasColumnName("IDTRAN");
            entity.Property(e => e.Idcauthu).HasColumnName("IDCAUTHU");
            entity.Property(e => e.Sobanthang).HasColumnName("SOBANTHANG");

            entity.HasOne(d => d.IdcauthuNavigation).WithMany(p => p.Thamgia)
                .HasForeignKey(d => d.Idcauthu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TG2");

            entity.HasOne(d => d.IdtranNavigation).WithMany(p => p.Thamgia)
                .HasForeignKey(d => d.Idtran)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TG1");
        });

        modelBuilder.Entity<Thongtingiaidau>(entity =>
        {
            entity.HasKey(e => new { e.Idgiaidau, e.Iddoibong }).HasName("PK__THONGTIN__44315CD7A4CCA015");

            entity.ToTable("THONGTINGIAIDAU");

            entity.Property(e => e.Idgiaidau).HasColumnName("IDGIAIDAU");
            entity.Property(e => e.Iddoibong)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IDDOIBONG");
            entity.Property(e => e.Draw)
                .HasDefaultValueSql("((0))")
                .HasColumnName("DRAW");
            entity.Property(e => e.Ga)
                .HasDefaultValueSql("((0))")
                .HasColumnName("GA");
            entity.Property(e => e.Gd)
                .HasDefaultValueSql("((0))")
                .HasColumnName("GD");
            entity.Property(e => e.Lose)
                .HasDefaultValueSql("((0))")
                .HasColumnName("LOSE");
            entity.Property(e => e.Points)
                .HasDefaultValueSql("((0))")
                .HasColumnName("POINTS");
            entity.Property(e => e.Win)
                .HasDefaultValueSql("((0))")
                .HasColumnName("WIN");

            entity.HasOne(d => d.IddoibongNavigation).WithMany(p => p.Thongtingiaidaus)
                .HasForeignKey(d => d.Iddoibong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TTGD2");

            entity.HasOne(d => d.IdgiaidauNavigation).WithMany(p => p.Thongtingiaidaus)
                .HasForeignKey(d => d.Idgiaidau)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TTGD1");
        });

        modelBuilder.Entity<Thongtintrandau>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__THONGTIN__3214EC27A0CF0655");

            entity.ToTable("THONGTINTRANDAU");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Diem).HasColumnName("DIEM");
            entity.Property(e => e.Iddoibong)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("IDDOIBONG");
            entity.Property(e => e.Idtrandau).HasColumnName("IDTRANDAU");
            entity.Property(e => e.Ketqua).HasColumnName("KETQUA");
            entity.Property(e => e.Thedo).HasColumnName("THEDO");
            entity.Property(e => e.Thevang).HasColumnName("THEVANG");

            entity.HasOne(d => d.IddoibongNavigation).WithMany(p => p.Thongtintrandaus)
                .HasForeignKey(d => d.Iddoibong)
                .HasConstraintName("fk_tttd_01");

            entity.HasOne(d => d.IdtrandauNavigation).WithMany(p => p.Thongtintrandaus)
                .HasForeignKey(d => d.Idtrandau)
                .HasConstraintName("fk_tttd_02");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERS__3214EC27B2A0926E");

            entity.ToTable("USERS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Avatar)
                .HasColumnType("image")
                .HasColumnName("AVATAR");
            entity.Property(e => e.Displayname).HasColumnName("DISPLAYNAME");
            entity.Property(e => e.Email)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Idavatar).HasColumnName("IDAVATAR");
            entity.Property(e => e.Idnhansu).HasColumnName("IDNHANSU");
            entity.Property(e => e.Idotp).HasColumnName("IDOTP");
            entity.Property(e => e.Iduserrole).HasColumnName("IDUSERROLE");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Username)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.IdotpNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Idotp)
                .HasConstraintName("FK_US_02");

            entity.HasOne(d => d.IduserroleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Iduserrole)
                .HasConstraintName("FK_US_01");
        });

        modelBuilder.Entity<Userrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERROLE__3214EC277E76CD4C");

            entity.ToTable("USERROLE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Role).HasColumnName("ROLE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
