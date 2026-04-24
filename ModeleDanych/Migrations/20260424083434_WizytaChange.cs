using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModeleDanych.Migrations
{
    /// <inheritdoc />
    public partial class WizytaChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // SQLite requires PRAGMA foreign_keys = 0 outside of a transaction
            // when doing table rebuilds, so we use raw SQL with suppressTransaction.
            migrationBuilder.Sql(
                @"
                PRAGMA foreign_keys = 0;

                CREATE TABLE ""ef_temp_Wizyty"" (
                    ""ID"" INTEGER NOT NULL CONSTRAINT ""PK_Wizyty"" PRIMARY KEY AUTOINCREMENT,
                    ""PacjentId"" INTEGER NULL,
                    ""LekarzId"" INTEGER NOT NULL,
                    ""GabinetId"" INTEGER NOT NULL,
                    ""Data"" TEXT NOT NULL,
                    ""InformacjeDodatkowe"" TEXT NULL,
                    CONSTRAINT ""FK_Wizyty_Pacjent_PacjentId"" FOREIGN KEY (""PacjentId"") REFERENCES ""Pacjent"" (""ID""),
                    CONSTRAINT ""FK_Wizyty_Lekarz_LekarzId"" FOREIGN KEY (""LekarzId"") REFERENCES ""Lekarz"" (""ID"") ON DELETE CASCADE,
                    CONSTRAINT ""FK_Wizyty_Gabinet_GabinetId"" FOREIGN KEY (""GabinetId"") REFERENCES ""Gabinet"" (""ID"") ON DELETE CASCADE
                );

                INSERT INTO ""ef_temp_Wizyty"" (""ID"", ""PacjentId"", ""LekarzId"", ""GabinetId"", ""Data"", ""InformacjeDodatkowe"")
                    SELECT ""ID"", ""PacjentId"", ""LekarzId"", ""GabinetId"", ""Data"", ""InformacjeDodatkowe""
                    FROM ""Wizyty"";

                DROP TABLE ""Wizyty"";

                ALTER TABLE ""ef_temp_Wizyty"" RENAME TO ""Wizyty"";

                CREATE INDEX ""IX_Wizyty_GabinetId"" ON ""Wizyty"" (""GabinetId"");
                CREATE INDEX ""IX_Wizyty_LekarzId"" ON ""Wizyty"" (""LekarzId"");
                CREATE INDEX ""IX_Wizyty_PacjentId"" ON ""Wizyty"" (""PacjentId"");

                PRAGMA foreign_keys = 1;
                ", suppressTransaction: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                PRAGMA foreign_keys = 0;

                CREATE TABLE ""ef_temp_Wizyty"" (
                    ""ID"" INTEGER NOT NULL CONSTRAINT ""PK_Wizyty"" PRIMARY KEY AUTOINCREMENT,
                    ""PacjentId"" INTEGER NOT NULL DEFAULT 0,
                    ""LekarzId"" INTEGER NOT NULL,
                    ""GabinetId"" INTEGER NOT NULL,
                    ""Data"" TEXT NOT NULL,
                    ""InformacjeDodatkowe"" TEXT NULL,
                    CONSTRAINT ""FK_Wizyty_Pacjent_PacjentId"" FOREIGN KEY (""PacjentId"") REFERENCES ""Pacjent"" (""ID"") ON DELETE CASCADE,
                    CONSTRAINT ""FK_Wizyty_Lekarz_LekarzId"" FOREIGN KEY (""LekarzId"") REFERENCES ""Lekarz"" (""ID"") ON DELETE CASCADE,
                    CONSTRAINT ""FK_Wizyty_Gabinet_GabinetId"" FOREIGN KEY (""GabinetId"") REFERENCES ""Gabinet"" (""ID"") ON DELETE CASCADE
                );

                INSERT INTO ""ef_temp_Wizyty"" (""ID"", ""PacjentId"", ""LekarzId"", ""GabinetId"", ""Data"", ""InformacjeDodatkowe"")
                    SELECT ""ID"", COALESCE(""PacjentId"", 0), ""LekarzId"", ""GabinetId"", ""Data"", ""InformacjeDodatkowe""
                    FROM ""Wizyty"";

                DROP TABLE ""Wizyty"";

                ALTER TABLE ""ef_temp_Wizyty"" RENAME TO ""Wizyty"";

                CREATE INDEX ""IX_Wizyty_GabinetId"" ON ""Wizyty"" (""GabinetId"");
                CREATE INDEX ""IX_Wizyty_LekarzId"" ON ""Wizyty"" (""LekarzId"");
                CREATE INDEX ""IX_Wizyty_PacjentId"" ON ""Wizyty"" (""PacjentId"");

                PRAGMA foreign_keys = 1;
                ", suppressTransaction: true);
        }
    }
}
