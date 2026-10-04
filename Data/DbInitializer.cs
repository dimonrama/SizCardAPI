using Microsoft.EntityFrameworkCore;

namespace SizCardApi.Data;

/// <summary>
/// EnsureCreated() создаёт таблицы только в ПУСТОЙ базе. Если sizcards.db уже существует
/// (с карточками), новые таблицы актов в ней сами не появятся. Этот метод добавляет их
/// через CREATE TABLE IF NOT EXISTS, не трогая существующие данные.
/// Схема совпадает с тем, что сгенерировал бы EF Core для моделей WriteOffAct / WriteOffActItem.
/// На свежей базе (где EnsureCreated уже всё создал) метод ничего не меняет.
/// </summary>
public static class DbInitializer
{
    public static void EnsureWriteOffTables(AppDbContext db)
    {
        db.Database.ExecuteSqlRaw(@"
CREATE TABLE IF NOT EXISTS ""WriteOffActs"" (
    ""Id"" INTEGER NOT NULL CONSTRAINT ""PK_WriteOffActs"" PRIMARY KEY AUTOINCREMENT,
    ""ActNumber"" TEXT NOT NULL,
    ""ActDate"" TEXT NOT NULL,
    ""CreatedAt"" TEXT NOT NULL,
    ""ChairmanName"" TEXT NULL,
    ""CommissionMembers"" TEXT NULL,
    ""Note"" TEXT NULL
);");

        db.Database.ExecuteSqlRaw(@"
CREATE TABLE IF NOT EXISTS ""WriteOffActItems"" (
    ""Id"" INTEGER NOT NULL CONSTRAINT ""PK_WriteOffActItems"" PRIMARY KEY AUTOINCREMENT,
    ""WriteOffActId"" INTEGER NOT NULL,
    ""SizCardId"" INTEGER NOT NULL,
    ""Reason"" INTEGER NOT NULL,
    ""Name"" TEXT NOT NULL,
    ""InventoryNumber"" TEXT NULL,
    ""BatchNumber"" TEXT NULL,
    ""SerialNumber"" TEXT NULL,
    ""Size"" TEXT NULL,
    ""StorageLocation"" TEXT NULL,
    ""ExpiryDate"" TEXT NULL,
    ""Owner"" TEXT NULL,
    CONSTRAINT ""FK_WriteOffActItems_WriteOffActs_WriteOffActId""
        FOREIGN KEY (""WriteOffActId"") REFERENCES ""WriteOffActs"" (""Id"") ON DELETE CASCADE
);");

        db.Database.ExecuteSqlRaw(
            @"CREATE UNIQUE INDEX IF NOT EXISTS ""IX_WriteOffActs_ActNumber"" ON ""WriteOffActs"" (""ActNumber"");");

        db.Database.ExecuteSqlRaw(
            @"CREATE INDEX IF NOT EXISTS ""IX_WriteOffActItems_WriteOffActId"" ON ""WriteOffActItems"" (""WriteOffActId"");");
    }
}
