﻿// <auto-generated />
using System;
using CloudStorage.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CloudStorage.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(CloudStorageDbContext))]
    partial class CloudStorageDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CloudStorage.Domain.CloudStorages.Storage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<long?>("Length")
                        .HasColumnType("bigint");

                    b.Property<string>("Path")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("StorageId")
                        .HasColumnType("char(36)");

                    b.Property<string>("StoragePath")
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("UserInfoId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("StorageId");

                    b.HasIndex("Type");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Storage", (string)null);

                    b.HasComment("云盘列表");
                });

            modelBuilder.Entity("CloudStorage.Domain.Users.property.UserStorages", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<long>("TotalSize")
                        .HasColumnType("bigint");

                    b.Property<long>("UsedSize")
                        .HasColumnType("bigint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserStorages", (string)null);

                    b.HasComment("用户云盘可用大小");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4cfb19ba-37b5-4d8e-8413-5341070b145f"),
                            ConcurrencyStamp = "80d351e0a58e4dd29309d64a471cbb3b",
                            TotalSize = 107374182400L,
                            UsedSize = 0L,
                            UserId = new Guid("58996810-f9e9-434e-83de-fa47a548640e")
                        });
                });

            modelBuilder.Entity("CloudStorage.Domain.Users.UserInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Account")
                        .HasColumnType("longtext");

                    b.Property<string>("BriefIntroduction")
                        .HasColumnType("longtext");

                    b.Property<string>("CloudStorageRoot")
                        .HasColumnType("longtext");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("HeadPortraits")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("WeChatOpenId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("UserInfo", (string)null);

                    b.HasComment("用户信息");

                    b.HasData(
                        new
                        {
                            Id = new Guid("58996810-f9e9-434e-83de-fa47a548640e"),
                            Account = "admin",
                            CloudStorageRoot = "./wwwroot/CloudStorage\\80d740b137a54df395480068683a1ffa",
                            ConcurrencyStamp = "a8e9bbe753394996bedce4660ff24631",
                            IsDeleted = false,
                            Name = "admin",
                            Password = "admin",
                            Sex = 0,
                            Status = 0
                        });
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.AuditLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ApplicationName")
                        .HasMaxLength(96)
                        .HasColumnType("varchar(96)")
                        .HasColumnName("ApplicationName");

                    b.Property<string>("BrowserInfo")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)")
                        .HasColumnName("BrowserInfo");

                    b.Property<string>("ClientId")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("ClientId");

                    b.Property<string>("ClientIpAddress")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("ClientIpAddress");

                    b.Property<string>("ClientName")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("ClientName");

                    b.Property<string>("Comments")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("Comments");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<string>("CorrelationId")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("CorrelationId");

                    b.Property<string>("Exceptions")
                        .HasColumnType("longtext");

                    b.Property<int>("ExecutionDuration")
                        .HasColumnType("int")
                        .HasColumnName("ExecutionDuration");

                    b.Property<DateTime>("ExecutionTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("longtext")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("HttpMethod")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)")
                        .HasColumnName("HttpMethod");

                    b.Property<int?>("HttpStatusCode")
                        .HasColumnType("int")
                        .HasColumnName("HttpStatusCode");

                    b.Property<Guid?>("ImpersonatorTenantId")
                        .HasColumnType("char(36)")
                        .HasColumnName("ImpersonatorTenantId");

                    b.Property<string>("ImpersonatorTenantName")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("ImpersonatorTenantName");

                    b.Property<Guid?>("ImpersonatorUserId")
                        .HasColumnType("char(36)")
                        .HasColumnName("ImpersonatorUserId");

                    b.Property<string>("ImpersonatorUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("ImpersonatorUserName");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)")
                        .HasColumnName("TenantId");

                    b.Property<string>("TenantName")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("TenantName");

                    b.Property<string>("Url")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("Url");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("char(36)")
                        .HasColumnName("UserId");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("UserName");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "ExecutionTime");

                    b.HasIndex("TenantId", "UserId", "ExecutionTime");

                    b.ToTable("AbpAuditLogs", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.AuditLogAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuditLogId")
                        .HasColumnType("char(36)")
                        .HasColumnName("AuditLogId");

                    b.Property<int>("ExecutionDuration")
                        .HasColumnType("int")
                        .HasColumnName("ExecutionDuration");

                    b.Property<DateTime>("ExecutionTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("ExecutionTime");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("longtext")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("MethodName")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("MethodName");

                    b.Property<string>("Parameters")
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)")
                        .HasColumnName("Parameters");

                    b.Property<string>("ServiceName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("ServiceName");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)")
                        .HasColumnName("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("AuditLogId");

                    b.HasIndex("TenantId", "ServiceName", "MethodName", "ExecutionTime");

                    b.ToTable("AbpAuditLogActions", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.EntityChange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuditLogId")
                        .HasColumnType("char(36)")
                        .HasColumnName("AuditLogId");

                    b.Property<DateTime>("ChangeTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("ChangeTime");

                    b.Property<byte>("ChangeType")
                        .HasColumnType("tinyint unsigned")
                        .HasColumnName("ChangeType");

                    b.Property<string>("EntityId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("EntityId");

                    b.Property<Guid?>("EntityTenantId")
                        .HasColumnType("char(36)");

                    b.Property<string>("EntityTypeFullName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("EntityTypeFullName");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("longtext")
                        .HasColumnName("ExtraProperties");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)")
                        .HasColumnName("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("AuditLogId");

                    b.HasIndex("TenantId", "EntityTypeFullName", "EntityId");

                    b.ToTable("AbpEntityChanges", (string)null);
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.EntityPropertyChange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("EntityChangeId")
                        .HasColumnType("char(36)");

                    b.Property<string>("NewValue")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)")
                        .HasColumnName("NewValue");

                    b.Property<string>("OriginalValue")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)")
                        .HasColumnName("OriginalValue");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("PropertyName");

                    b.Property<string>("PropertyTypeFullName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("PropertyTypeFullName");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)")
                        .HasColumnName("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("EntityChangeId");

                    b.ToTable("AbpEntityPropertyChanges", (string)null);
                });

            modelBuilder.Entity("CloudStorage.Domain.CloudStorages.Storage", b =>
                {
                    b.HasOne("CloudStorage.Domain.Users.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.AuditLogAction", b =>
                {
                    b.HasOne("Volo.Abp.AuditLogging.AuditLog", null)
                        .WithMany("Actions")
                        .HasForeignKey("AuditLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.EntityChange", b =>
                {
                    b.HasOne("Volo.Abp.AuditLogging.AuditLog", null)
                        .WithMany("EntityChanges")
                        .HasForeignKey("AuditLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.EntityPropertyChange", b =>
                {
                    b.HasOne("Volo.Abp.AuditLogging.EntityChange", null)
                        .WithMany("PropertyChanges")
                        .HasForeignKey("EntityChangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.AuditLog", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("EntityChanges");
                });

            modelBuilder.Entity("Volo.Abp.AuditLogging.EntityChange", b =>
                {
                    b.Navigation("PropertyChanges");
                });
#pragma warning restore 612, 618
        }
    }
}
