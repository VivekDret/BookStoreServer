﻿// <auto-generated />
using System;
using BookStoreServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookStoreServer.Migrations
{
    [DbContext(typeof(BookStoreContext))]
    [Migration("20240605171105_BookStore")]
    partial class BookStore
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookStoreServer.Models.Author", b =>
                {
                    b.Property<int>("AuthorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorID"));

                    b.Property<string>("AuthorAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("AuthorContact")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("AuthorEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthorGender")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("AuthorID");

                    b.ToTable("Author", (string)null);
                });

            modelBuilder.Entity("BookStoreServer.Models.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookID"));

                    b.Property<int?>("AuthorID")
                        .HasColumnType("int");

                    b.Property<string>("BookImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BookPrice")
                        .HasColumnType("int");

                    b.Property<int>("BookQuantity")
                        .HasColumnType("int");

                    b.Property<string>("BookSerialNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BookTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("BookYear")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.HasKey("BookID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Books", (string)null);
                });

            modelBuilder.Entity("BookStoreServer.Models.Cart", b =>
                {
                    b.Property<int>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartID"));

                    b.Property<int>("CartTotal")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CartID");

                    b.HasIndex("UserId");

                    b.ToTable("Cart", (string)null);
                });

            modelBuilder.Entity("BookStoreServer.Models.CartDetail", b =>
                {
                    b.Property<int>("CartDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartDetailID"));

                    b.Property<int?>("BookId")
                        .HasColumnType("int");

                    b.Property<int?>("CartID")
                        .HasColumnType("int");

                    b.Property<int>("CartQuantity")
                        .HasColumnType("int");

                    b.Property<int>("SubTotalCart")
                        .HasColumnType("int");

                    b.HasKey("CartDetailID");

                    b.HasIndex("BookId");

                    b.HasIndex("CartID");

                    b.ToTable("CartDetail", (string)null);
                });

            modelBuilder.Entity("BookStoreServer.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryID");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("BookStoreServer.Models.OrderDetail", b =>
                {
                    b.Property<int?>("OrderID")
                        .HasColumnType("int");

                    b.Property<int?>("BookID")
                        .HasColumnType("int");

                    b.Property<int>("OrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailID"));

                    b.Property<int>("OrderQuantity")
                        .HasColumnType("int");

                    b.Property<int>("SubTotalOrder")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("BookID");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("BookStoreServer.Models.OrderTbl", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderTotal")
                        .HasColumnType("int");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("UserId");

                    b.ToTable("OrderTbl", (string)null);
                });

            modelBuilder.Entity("BookStoreServer.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("RefundId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("UserId");

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("BookStoreServer.Models.Refund", b =>
                {
                    b.Property<int>("RefundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RefundId"));

                    b.Property<int?>("PaymentId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal>("RefundAmount")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<DateTime>("RefundDate")
                        .HasColumnType("datetime");

                    b.Property<string>("RefundStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("Not Approved");

                    b.HasKey("RefundId");

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.ToTable("Refund", (string)null);
                });

            modelBuilder.Entity("BookStoreServer.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewID"));

                    b.Property<int?>("BookID")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewComment")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("ReviewID");

                    b.HasIndex("BookID");

                    b.HasIndex("UserId");

                    b.ToTable("Review", (string)null);
                });

            modelBuilder.Entity("BookStoreServer.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("UserAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserContact")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserFirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserGender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("UserLastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserProfilePic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("BookStoreServer.Models.Book", b =>
                {
                    b.HasOne("BookStoreServer.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Book_Author");

                    b.HasOne("BookStoreServer.Models.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_Book_Category");

                    b.Navigation("Author");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BookStoreServer.Models.Cart", b =>
                {
                    b.HasOne("BookStoreServer.Models.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_User_Cart");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookStoreServer.Models.CartDetail", b =>
                {
                    b.HasOne("BookStoreServer.Models.Book", "Book")
                        .WithMany("CartDetails")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_Book_CartDetail");

                    b.HasOne("BookStoreServer.Models.Cart", "Cart")
                        .WithMany("CartDetails")
                        .HasForeignKey("CartID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Book");

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("BookStoreServer.Models.OrderDetail", b =>
                {
                    b.HasOne("BookStoreServer.Models.Book", "Book")
                        .WithMany("OrdertDetails")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("BookStoreServer.Models.OrderTbl", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BookStoreServer.Models.OrderTbl", b =>
                {
                    b.HasOne("BookStoreServer.Models.Payment", "Payment")
                        .WithOne("Order")
                        .HasForeignKey("BookStoreServer.Models.OrderTbl", "OrderID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BookStoreServer.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Payment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookStoreServer.Models.Payment", b =>
                {
                    b.HasOne("BookStoreServer.Models.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookStoreServer.Models.Refund", b =>
                {
                    b.HasOne("BookStoreServer.Models.Payment", "Payment")
                        .WithOne("Refund")
                        .HasForeignKey("BookStoreServer.Models.Refund", "PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Refund_Payment");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("BookStoreServer.Models.Review", b =>
                {
                    b.HasOne("BookStoreServer.Models.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_Review_Book");

                    b.HasOne("BookStoreServer.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Review_User");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookStoreServer.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookStoreServer.Models.Book", b =>
                {
                    b.Navigation("CartDetails");

                    b.Navigation("OrdertDetails");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("BookStoreServer.Models.Cart", b =>
                {
                    b.Navigation("CartDetails");
                });

            modelBuilder.Entity("BookStoreServer.Models.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookStoreServer.Models.OrderTbl", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BookStoreServer.Models.Payment", b =>
                {
                    b.Navigation("Order");

                    b.Navigation("Refund");
                });

            modelBuilder.Entity("BookStoreServer.Models.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");

                    b.Navigation("Payments");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
