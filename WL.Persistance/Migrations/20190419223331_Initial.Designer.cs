﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using WL.Persistance;

namespace WL.Persistance.Migrations
{
    [DbContext(typeof(WLDbContext))]
    [Migration("20190419223331_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            modelBuilder.Entity("WL.Domain.AnnotationType", b =>
                {
                    b.Property<long>("AnnotationTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Root")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("AnnotationTypeId");

                    b.HasAlternateKey("Name");

                    b.HasAlternateKey("Root");

                    b.ToTable("AnnotationTypes");
                });

            modelBuilder.Entity("WL.Domain.Document", b =>
                {
                    b.Property<long>("DocumentId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("DocumentTypeId");

                    b.Property<long>("EntityId");

                    b.Property<long>("FileDocumentId");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("PublicationDate");

                    b.Property<long>("PublicationYear");

                    b.HasKey("DocumentId");

                    b.HasAlternateKey("DocumentTypeId", "EntityId", "Number", "PublicationYear");

                    b.HasIndex("EntityId");

                    b.HasIndex("FileDocumentId")
                        .IsUnique();

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("WL.Domain.DocumentType", b =>
                {
                    b.Property<long>("DocumentTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("DocumentTypeId");

                    b.HasAlternateKey("Name");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("WL.Domain.Entity", b =>
                {
                    b.Property<long>("EntityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<long>("EntityTypeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("EntityId");

                    b.HasAlternateKey("Name");

                    b.HasIndex("EntityTypeId");

                    b.ToTable("Entities");
                });

            modelBuilder.Entity("WL.Domain.EntityType", b =>
                {
                    b.Property<long>("EntityTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("EntityTypeId");

                    b.HasAlternateKey("Name");

                    b.ToTable("EntityTypes");
                });

            modelBuilder.Entity("WL.Domain.EntityTypeDocumentType", b =>
                {
                    b.Property<long>("EntityTypeId");

                    b.Property<long>("DocumentTypeId");

                    b.HasKey("EntityTypeId", "DocumentTypeId");

                    b.HasIndex("DocumentTypeId");

                    b.ToTable("EntityTypeDocumentType");
                });

            modelBuilder.Entity("WL.Domain.File", b =>
                {
                    b.Property<long>("DocumentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Issue");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("DocumentId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("WL.Domain.Document", b =>
                {
                    b.HasOne("WL.Domain.DocumentType", "DocumentType")
                        .WithMany()
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WL.Domain.Entity", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WL.Domain.File", "File")
                        .WithOne("Document")
                        .HasForeignKey("WL.Domain.Document", "FileDocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WL.Domain.Entity", b =>
                {
                    b.HasOne("WL.Domain.EntityType", "EntityType")
                        .WithMany()
                        .HasForeignKey("EntityTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WL.Domain.EntityTypeDocumentType", b =>
                {
                    b.HasOne("WL.Domain.DocumentType", "DocumentType")
                        .WithMany()
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WL.Domain.EntityType", "EntityType")
                        .WithMany("SupportedDocuments")
                        .HasForeignKey("EntityTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
