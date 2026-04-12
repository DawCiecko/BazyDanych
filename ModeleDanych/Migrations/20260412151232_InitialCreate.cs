using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModeleDanych.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Badania",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badania", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DaneLogowania",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Salt = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    TypKonta = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaneLogowania", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Produkty",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkty", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Szpital",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Adres = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Szpital", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pacjent",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imie = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Nazwisko = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PESEL = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    NrTel = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Adres = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    DaneLogowaniaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacjent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pacjent_DaneLogowania_DaneLogowaniaId",
                        column: x => x.DaneLogowaniaId,
                        principalTable: "DaneLogowania",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zamowienia",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProduktId = table.Column<int>(type: "INTEGER", nullable: false),
                    Ilosc = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamowienia", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zamowienia_Produkty_ProduktId",
                        column: x => x.ProduktId,
                        principalTable: "Produkty",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gabinet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numer = table.Column<int>(type: "INTEGER", nullable: false),
                    SzpitalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gabinet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Gabinet_Szpital_SzpitalId",
                        column: x => x.SzpitalId,
                        principalTable: "Szpital",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Magazyn",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SzpitalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magazyn", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Magazyn_Szpital_SzpitalId",
                        column: x => x.SzpitalId,
                        principalTable: "Szpital",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poradnia",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SzpitalId = table.Column<int>(type: "INTEGER", nullable: false),
                    Adres = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poradnia", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Poradnia_Szpital_SzpitalId",
                        column: x => x.SzpitalId,
                        principalTable: "Szpital",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imie = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Nazwisko = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PESEL = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    NrTel = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Adres = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Wypłata = table.Column<double>(type: "REAL", nullable: false),
                    Stanowisko = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    SzpitalId = table.Column<int>(type: "INTEGER", nullable: false),
                    DaneLogowaniaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pracownicy_DaneLogowania_DaneLogowaniaId",
                        column: x => x.DaneLogowaniaId,
                        principalTable: "DaneLogowania",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pracownicy_Szpital_SzpitalId",
                        column: x => x.SzpitalId,
                        principalTable: "Szpital",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dostawy",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ZamowienieId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataDostawy = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MagazynId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dostawy", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Dostawy_Magazyn_MagazynId",
                        column: x => x.MagazynId,
                        principalTable: "Magazyn",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dostawy_Zamowienia_ZamowienieId",
                        column: x => x.ZamowienieId,
                        principalTable: "Zamowienia",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StanMagazynowy",
                columns: table => new
                {
                    MagazynId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProduktId = table.Column<int>(type: "INTEGER", nullable: false),
                    Ilosc = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanMagazynowy", x => new { x.MagazynId, x.ProduktId });
                    table.ForeignKey(
                        name: "FK_StanMagazynowy_Magazyn_MagazynId",
                        column: x => x.MagazynId,
                        principalTable: "Magazyn",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StanMagazynowy_Produkty_ProduktId",
                        column: x => x.ProduktId,
                        principalTable: "Produkty",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grafik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PracownikId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataKoniec = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GabinetId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grafik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Grafik_Gabinet_GabinetId",
                        column: x => x.GabinetId,
                        principalTable: "Gabinet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grafik_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lekarz",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdPracownika = table.Column<int>(type: "INTEGER", nullable: false),
                    Specjalizacja = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lekarz", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lekarz_Pracownicy_IdPracownika",
                        column: x => x.IdPracownika,
                        principalTable: "Pracownicy",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recepta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kod = table.Column<int>(type: "INTEGER", nullable: false),
                    LekarzWystawiajacyId = table.Column<int>(type: "INTEGER", nullable: false),
                    PacjentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProduktId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepta", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Recepta_Lekarz_LekarzWystawiajacyId",
                        column: x => x.LekarzWystawiajacyId,
                        principalTable: "Lekarz",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recepta_Pacjent_PacjentId",
                        column: x => x.PacjentId,
                        principalTable: "Pacjent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recepta_Produkty_ProduktId",
                        column: x => x.ProduktId,
                        principalTable: "Produkty",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skierowania",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kod = table.Column<int>(type: "INTEGER", nullable: false),
                    LekarzWystawiajacyId = table.Column<int>(type: "INTEGER", nullable: false),
                    PacjentId = table.Column<int>(type: "INTEGER", nullable: false),
                    BadanieId = table.Column<int>(type: "INTEGER", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skierowania", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Skierowania_Badania_BadanieId",
                        column: x => x.BadanieId,
                        principalTable: "Badania",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skierowania_Lekarz_LekarzWystawiajacyId",
                        column: x => x.LekarzWystawiajacyId,
                        principalTable: "Lekarz",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skierowania_Pacjent_PacjentId",
                        column: x => x.PacjentId,
                        principalTable: "Pacjent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wizyty",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PacjentId = table.Column<int>(type: "INTEGER", nullable: false),
                    LekarzId = table.Column<int>(type: "INTEGER", nullable: false),
                    GabinetId = table.Column<int>(type: "INTEGER", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InformacjeDodatkowe = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wizyty", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Wizyty_Gabinet_GabinetId",
                        column: x => x.GabinetId,
                        principalTable: "Gabinet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wizyty_Lekarz_LekarzId",
                        column: x => x.LekarzId,
                        principalTable: "Lekarz",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wizyty_Pacjent_PacjentId",
                        column: x => x.PacjentId,
                        principalTable: "Pacjent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Procedury",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypBadaniaId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataWykonania = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SkierowanieId = table.Column<int>(type: "INTEGER", nullable: true),
                    LekarzWykonujacyId = table.Column<int>(type: "INTEGER", nullable: false),
                    PacjentId = table.Column<int>(type: "INTEGER", nullable: false),
                    InfoDodatkowe = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedury", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Procedury_Badania_TypBadaniaId",
                        column: x => x.TypBadaniaId,
                        principalTable: "Badania",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedury_Lekarz_LekarzWykonujacyId",
                        column: x => x.LekarzWykonujacyId,
                        principalTable: "Lekarz",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedury_Pacjent_PacjentId",
                        column: x => x.PacjentId,
                        principalTable: "Pacjent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedury_Skierowania_SkierowanieId",
                        column: x => x.SkierowanieId,
                        principalTable: "Skierowania",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Terminarz",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProceduraId = table.Column<int>(type: "INTEGER", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminarz", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Terminarz_Gabinet_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Gabinet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Terminarz_Procedury_ProceduraId",
                        column: x => x.ProceduraId,
                        principalTable: "Procedury",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wyniki",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProceduraId = table.Column<int>(type: "INTEGER", nullable: false),
                    OpisBadania = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wyniki", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Wyniki_Procedury_ProceduraId",
                        column: x => x.ProceduraId,
                        principalTable: "Procedury",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dostawy_MagazynId",
                table: "Dostawy",
                column: "MagazynId");

            migrationBuilder.CreateIndex(
                name: "IX_Dostawy_ZamowienieId",
                table: "Dostawy",
                column: "ZamowienieId");

            migrationBuilder.CreateIndex(
                name: "IX_Gabinet_SzpitalId",
                table: "Gabinet",
                column: "SzpitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Grafik_GabinetId",
                table: "Grafik",
                column: "GabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_Grafik_PracownikId",
                table: "Grafik",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Lekarz_IdPracownika",
                table: "Lekarz",
                column: "IdPracownika",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Magazyn_SzpitalId",
                table: "Magazyn",
                column: "SzpitalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacjent_DaneLogowaniaId",
                table: "Pacjent",
                column: "DaneLogowaniaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Poradnia_SzpitalId",
                table: "Poradnia",
                column: "SzpitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_DaneLogowaniaId",
                table: "Pracownicy",
                column: "DaneLogowaniaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_SzpitalId",
                table: "Pracownicy",
                column: "SzpitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedury_LekarzWykonujacyId",
                table: "Procedury",
                column: "LekarzWykonujacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedury_PacjentId",
                table: "Procedury",
                column: "PacjentId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedury_SkierowanieId",
                table: "Procedury",
                column: "SkierowanieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedury_TypBadaniaId",
                table: "Procedury",
                column: "TypBadaniaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recepta_LekarzWystawiajacyId",
                table: "Recepta",
                column: "LekarzWystawiajacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Recepta_PacjentId",
                table: "Recepta",
                column: "PacjentId");

            migrationBuilder.CreateIndex(
                name: "IX_Recepta_ProduktId",
                table: "Recepta",
                column: "ProduktId");

            migrationBuilder.CreateIndex(
                name: "IX_Skierowania_BadanieId",
                table: "Skierowania",
                column: "BadanieId");

            migrationBuilder.CreateIndex(
                name: "IX_Skierowania_LekarzWystawiajacyId",
                table: "Skierowania",
                column: "LekarzWystawiajacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Skierowania_PacjentId",
                table: "Skierowania",
                column: "PacjentId");

            migrationBuilder.CreateIndex(
                name: "IX_StanMagazynowy_ProduktId",
                table: "StanMagazynowy",
                column: "ProduktId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminarz_ProceduraId",
                table: "Terminarz",
                column: "ProceduraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Terminarz_SalaId",
                table: "Terminarz",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Wizyty_GabinetId",
                table: "Wizyty",
                column: "GabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_Wizyty_LekarzId",
                table: "Wizyty",
                column: "LekarzId");

            migrationBuilder.CreateIndex(
                name: "IX_Wizyty_PacjentId",
                table: "Wizyty",
                column: "PacjentId");

            migrationBuilder.CreateIndex(
                name: "IX_Wyniki_ProceduraId",
                table: "Wyniki",
                column: "ProceduraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienia_ProduktId",
                table: "Zamowienia",
                column: "ProduktId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dostawy");

            migrationBuilder.DropTable(
                name: "Grafik");

            migrationBuilder.DropTable(
                name: "Poradnia");

            migrationBuilder.DropTable(
                name: "Recepta");

            migrationBuilder.DropTable(
                name: "StanMagazynowy");

            migrationBuilder.DropTable(
                name: "Terminarz");

            migrationBuilder.DropTable(
                name: "Wizyty");

            migrationBuilder.DropTable(
                name: "Wyniki");

            migrationBuilder.DropTable(
                name: "Zamowienia");

            migrationBuilder.DropTable(
                name: "Magazyn");

            migrationBuilder.DropTable(
                name: "Gabinet");

            migrationBuilder.DropTable(
                name: "Procedury");

            migrationBuilder.DropTable(
                name: "Produkty");

            migrationBuilder.DropTable(
                name: "Skierowania");

            migrationBuilder.DropTable(
                name: "Badania");

            migrationBuilder.DropTable(
                name: "Lekarz");

            migrationBuilder.DropTable(
                name: "Pacjent");

            migrationBuilder.DropTable(
                name: "Pracownicy");

            migrationBuilder.DropTable(
                name: "DaneLogowania");

            migrationBuilder.DropTable(
                name: "Szpital");
        }
    }
}
