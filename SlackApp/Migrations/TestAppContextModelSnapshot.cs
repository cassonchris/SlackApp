﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SlackApp.Repositories;
using System;

namespace SlackApp.Migrations
{
    [DbContext(typeof(SlackAppContext))]
    partial class TestAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SlackApp.Models.AppInstall", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<bool>("Ok");

                    b.Property<string>("Scope")
                        .HasMaxLength(250);

                    b.Property<string>("TeamId")
                        .HasMaxLength(50);

                    b.Property<string>("TeamName")
                        .HasMaxLength(250);

                    b.HasKey("UserId");

                    b.ToTable("AppInstalls");
                });
#pragma warning restore 612, 618
        }
    }
}
