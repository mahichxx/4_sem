using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FitnessClub
{
    public static class DatabaseService
    {
        public static string ConnString =>
    ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString + ";Connect Timeout=5";
        public static void InitializeDatabase()
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                string sql = @"
            -- Таблица категорий
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
            CREATE TABLE Categories (Id INT PRIMARY KEY IDENTITY, Name NVARCHAR(100) NOT NULL);

            -- Таблица услуг
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Services')
            CREATE TABLE Services (
                Id INT PRIMARY KEY IDENTITY, 
                ShortName NVARCHAR(100) NOT NULL, 
                Price DECIMAL(18,2) NOT NULL, 
                ImagePath NVARCHAR(MAX), 
                Quantity INT DEFAULT 0, 
                Rating FLOAT DEFAULT 0
            );

            -- ТАБЛИЦА ДЛЯ ТРИГГЕРА (Лог изменений цен)
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PriceLog')
            CREATE TABLE PriceLog (Id INT PRIMARY KEY IDENTITY, ServiceId INT, OldPrice DECIMAL(18,2), NewPrice DECIMAL(18,2), ChangeDate DATETIME);
        ";
                using (var cmd = new SqlCommand(sql, conn)) cmd.ExecuteNonQuery();

                //  Пункт №2 лабы
                string triggerSql = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'TR' AND name = 'TR_Services_PriceUpdate')
            EXEC('CREATE TRIGGER TR_Services_PriceUpdate ON Services AFTER UPDATE AS 
                  IF UPDATE(Price)
                  INSERT INTO PriceLog (ServiceId, OldPrice, NewPrice, ChangeDate)
                  SELECT d.Id, d.Price, i.Price, GETDATE() FROM deleted d JOIN inserted i ON d.Id = i.Id')";
                using (var cmd = new SqlCommand(triggerSql, conn)) cmd.ExecuteNonQuery();

                // Хранимая процедура 
                string procSql = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'UpdateServiceRating')
            EXEC('CREATE PROCEDURE UpdateServiceRating @id INT, @rating INT AS 
                  UPDATE Services SET Rating = @rating WHERE Id = @id')";
                using (var cmd = new SqlCommand(procSql, conn)) cmd.ExecuteNonQuery();
            }
        }
        // 4c. Асинхронное получение данных (CRUD: Read)
        public static async Task<List<Service>> GetAllServicesAsync()
        {
            var list = new List<Service>();
            using (var conn = new SqlConnection(ConnString))
            {
                await conn.OpenAsync();
                string sql = "SELECT * FROM Services";
                using (var cmd = new SqlCommand(sql, conn))
                using (var rdr = await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        list.Add(new Service
                        {
                            Id = (int)rdr["Id"],
                            ShortName = rdr["ShortName"].ToString(),
                            Price = (decimal)rdr["Price"],
                            Quantity = (int)rdr["Quantity"],
                            Rating = Convert.ToDouble(rdr["Rating"]),
                            Images = new List<string> { rdr["ImagePath"]?.ToString() ?? "" }
                        });
                    }
                }
            }
            return list;
        }

        // !!!!!5. Сохранение с использованием ТРАНЗАКЦИИ (CRUD: Create/Update)
        public static void SaveServiceWithTransaction(Service s, bool isNew)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = isNew
                            ? "INSERT INTO Services (ShortName, Price, ImagePath, Quantity, Rating) VALUES (@n, @p, @i, @q, @r)"
                            : "UPDATE Services SET ShortName=@n, Price=@p, ImagePath=@i, Quantity=@q, Rating=@r WHERE Id=@id";

                        using (var cmd = new SqlCommand(sql, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@n", s.ShortName ?? "");
                            cmd.Parameters.AddWithValue("@p", s.Price);
                            cmd.Parameters.AddWithValue("@i", (s.Images != null && s.Images.Count > 0) ? s.Images[0] : "");
                            cmd.Parameters.AddWithValue("@q", s.Quantity);
                            cmd.Parameters.AddWithValue("@r", s.Rating);
                            if (!isNew) cmd.Parameters.AddWithValue("@id", s.Id);

                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    catch { trans.Rollback(); throw; }
                }
            }
        }

        // 4a. Удаление (CRUD: Delete)
        public static void DeleteService(int id)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM Services WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 4c. Вызов ХРАНИМОЙ ПРОЦЕДУРЫ асинхронно
        public static async Task UpdateRatingProcedureAsync(int id, int newRating)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("UpdateServiceRating", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@rating", newRating);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}