﻿// <auto-generated />
using DealFinder.Data.Deals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DealFinder.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20180220131230_RemovingLocationsTable")]
    partial class RemovingLocationsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("DealFinder.Data.Deals.DealRecord", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Summary");

                    b.Property<string>("Title");

                    b.HasKey("Identifier");

                    b.ToTable("Deals");
                });
#pragma warning restore 612, 618
        }
    }
}
