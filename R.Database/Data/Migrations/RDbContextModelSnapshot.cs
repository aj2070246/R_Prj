﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using R.Database;

#nullable disable

namespace R.Database.Data.Migrations
{
    [DbContext(typeof(RDbContext))]
    partial class RDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("R.Database.Entities.Age", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ItemValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Age");
                });

            modelBuilder.Entity("R.Database.Entities.AppConfigs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("KeyDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeyValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppConfigs");
                });

            modelBuilder.Entity("R.Database.Entities.BlockedDataLog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BlockedUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RUsersId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SourceUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RUsersId");

                    b.ToTable("BlockedDataLog");
                });

            modelBuilder.Entity("R.Database.Entities.Captcha", b =>
                {
                    b.Property<string>("CaptchaId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CaptchaValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CaptchaId");

                    b.ToTable("Captchas");
                });

            modelBuilder.Entity("R.Database.Entities.CarValue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ItemValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CarValue");
                });

            modelBuilder.Entity("R.Database.Entities.CheckMeActivityLogs", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RUsersId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId_CheckedMe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RUsersId");

                    b.ToTable("CheckMeActivityLogs");
                });

            modelBuilder.Entity("R.Database.Entities.FavoriteDataLog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FavoritedUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RUsersId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SourceUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RUsersId");

                    b.ToTable("FavoriteDataLog");
                });

            modelBuilder.Entity("R.Database.Entities.Gender", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ItemValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gender");
                });

            modelBuilder.Entity("R.Database.Entities.HealthStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ItemValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HealthStatus");
                });

            modelBuilder.Entity("R.Database.Entities.HomeValue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ItemValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HomeValue");
                });

            modelBuilder.Entity("R.Database.Entities.IncomeAmount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ItemValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IncomeAmount");
                });

            modelBuilder.Entity("R.Database.Entities.LiveType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ItemValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LiveType");
                });

            modelBuilder.Entity("R.Database.Entities.MarriageStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ItemValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MarriageStatus");
                });

            modelBuilder.Entity("R.Database.Entities.Province", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ItemValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Province");
                });

            modelBuilder.Entity("R.Database.Entities.RUsers", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CarValueId")
                        .HasColumnType("bigint");

                    b.Property<int>("CheildCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateUserDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmailAddressStatusId")
                        .HasColumnType("int");

                    b.Property<string>("EmailVerifyCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EmailVerifyCodeExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FirstCheildAge")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("GenderId")
                        .HasColumnType("bigint");

                    b.Property<int>("Ghad")
                        .HasColumnType("int");

                    b.Property<long>("HealthStatusId")
                        .HasColumnType("bigint");

                    b.Property<long?>("HomeValueId")
                        .HasColumnType("bigint");

                    b.Property<long?>("IncomeAmountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastActivityDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("LiveTypeId")
                        .HasColumnType("bigint");

                    b.Property<long>("MarriageStatusId")
                        .HasColumnType("bigint");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MobileStatusId")
                        .HasColumnType("int");

                    b.Property<string>("MobileVerifyCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MobileVerifyCodeExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MyDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhoneVerifyWay")
                        .HasColumnType("int");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<long>("ProvinceId")
                        .HasColumnType("bigint");

                    b.Property<string>("RDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RangePoost")
                        .HasColumnType("int");

                    b.Property<long?>("RelationTypeId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TelegramChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("TelegramUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipNUmber")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TokenExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserStatus")
                        .HasColumnType("int");

                    b.Property<int>("Vazn")
                        .HasColumnType("int");

                    b.Property<int>("ZibaeeNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarValueId");

                    b.HasIndex("GenderId");

                    b.HasIndex("HealthStatusId");

                    b.HasIndex("HomeValueId");

                    b.HasIndex("IncomeAmountId");

                    b.HasIndex("LiveTypeId");

                    b.HasIndex("MarriageStatusId");

                    b.HasIndex("ProvinceId");

                    b.HasIndex("RelationTypeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("R.Database.Entities.RelationType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ItemValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RelationType");
                });

            modelBuilder.Entity("R.Database.Entities.UsersMessages", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MessageStatusId")
                        .HasColumnType("int");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SenderUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UsersMessages");
                });

            modelBuilder.Entity("R.Database.Entities.BlockedDataLog", b =>
                {
                    b.HasOne("R.Database.Entities.RUsers", null)
                        .WithMany("BlockedDataLog")
                        .HasForeignKey("RUsersId");
                });

            modelBuilder.Entity("R.Database.Entities.CheckMeActivityLogs", b =>
                {
                    b.HasOne("R.Database.Entities.RUsers", "RUsers")
                        .WithMany("CheckMeLogDataLog")
                        .HasForeignKey("RUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RUsers");
                });

            modelBuilder.Entity("R.Database.Entities.FavoriteDataLog", b =>
                {
                    b.HasOne("R.Database.Entities.RUsers", null)
                        .WithMany("FavoriteDataLog")
                        .HasForeignKey("RUsersId");
                });

            modelBuilder.Entity("R.Database.Entities.RUsers", b =>
                {
                    b.HasOne("R.Database.Entities.CarValue", "CarValue")
                        .WithMany()
                        .HasForeignKey("CarValueId");

                    b.HasOne("R.Database.Entities.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("R.Database.Entities.HealthStatus", "HealthStatus")
                        .WithMany()
                        .HasForeignKey("HealthStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("R.Database.Entities.HomeValue", "HomeValue")
                        .WithMany()
                        .HasForeignKey("HomeValueId");

                    b.HasOne("R.Database.Entities.IncomeAmount", "IncomeAmount")
                        .WithMany()
                        .HasForeignKey("IncomeAmountId");

                    b.HasOne("R.Database.Entities.LiveType", "LiveType")
                        .WithMany()
                        .HasForeignKey("LiveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("R.Database.Entities.MarriageStatus", "MarriageStatus")
                        .WithMany()
                        .HasForeignKey("MarriageStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("R.Database.Entities.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("R.Database.Entities.RelationType", "RelationType")
                        .WithMany()
                        .HasForeignKey("RelationTypeId");

                    b.Navigation("CarValue");

                    b.Navigation("Gender");

                    b.Navigation("HealthStatus");

                    b.Navigation("HomeValue");

                    b.Navigation("IncomeAmount");

                    b.Navigation("LiveType");

                    b.Navigation("MarriageStatus");

                    b.Navigation("Province");

                    b.Navigation("RelationType");
                });

            modelBuilder.Entity("R.Database.Entities.RUsers", b =>
                {
                    b.Navigation("BlockedDataLog");

                    b.Navigation("CheckMeLogDataLog");

                    b.Navigation("FavoriteDataLog");
                });
#pragma warning restore 612, 618
        }
    }
}
