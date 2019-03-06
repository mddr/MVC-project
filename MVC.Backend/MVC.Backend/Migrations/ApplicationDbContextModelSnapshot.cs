﻿// <auto-generated />
using System;
using MVC.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MVC.Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("MVC.Backend.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("HouseNumber");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Street");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("MVC.Backend.Models.CartItem", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ProductId");

                    b.Property<bool>("IsValid");

                    b.Property<int?>("OrderId");

                    b.Property<int>("ProductAmount");

                    b.Property<int>("UserId");

                    b.HasKey("Id", "ProductId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("MVC.Backend.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsHidden");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("SuperiorCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("SuperiorCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MVC.Backend.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AddressId");

                    b.Property<string>("CartId")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<double>("TotalPrice");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MVC.Backend.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmountAvailable");

                    b.Property<int>("BoughtTimes");

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<int>("Discount");

                    b.Property<string>("ExpertEmail");

                    b.Property<string>("FullImagePath");

                    b.Property<bool>("IsHidden");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("PricePln");

                    b.Property<int>("TaxRate");

                    b.Property<string>("ThumbnailPath");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MVC.Backend.Models.ProductFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("FilePath")
                        .IsRequired();

                    b.Property<string>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductFiles");
                });

            modelBuilder.Entity("MVC.Backend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AcceptsNewsletters");

                    b.Property<int?>("AddressId");

                    b.Property<string>("CartId");

                    b.Property<int>("Currency");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired();

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired();

                    b.Property<bool>("PrefersNetPrice");

                    b.Property<int>("ProductsPerPage");

                    b.Property<string>("RefreshToken");

                    b.Property<int>("Role");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MVC.Backend.Models.CartItem", b =>
                {
                    b.HasOne("MVC.Backend.Models.Order")
                        .WithMany("ShoppingCart")
                        .HasForeignKey("OrderId");

                    b.HasOne("MVC.Backend.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MVC.Backend.Models.User", "User")
                        .WithMany("ShoppingCart")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MVC.Backend.Models.Category", b =>
                {
                    b.HasOne("MVC.Backend.Models.Category", "SuperiorCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("SuperiorCategoryId");
                });

            modelBuilder.Entity("MVC.Backend.Models.Order", b =>
                {
                    b.HasOne("MVC.Backend.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("MVC.Backend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MVC.Backend.Models.Product", b =>
                {
                    b.HasOne("MVC.Backend.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MVC.Backend.Models.ProductFile", b =>
                {
                    b.HasOne("MVC.Backend.Models.Product", "Product")
                        .WithMany("Files")
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
