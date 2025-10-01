using EasyCarsMaintenance.Models;
using SQLite;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyCarsMaintenance.Services
{
    internal interface IDatabaseService
    {
        SQLiteAsyncConnection GetConnection();
        Task<bool> InitializeDatabaseAsync();
    }

    internal class DatabaseService : IDatabaseService
    {
        private const string DatabaseName = "CarsMaintenance.db";
        private readonly string _databasePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EasyCarsMaintenance", DatabaseName);

        internal SQLiteAsyncConnection GetConnection()
        {
            return new SQLiteAsyncConnection(_databasePath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
        }

        internal async Task<bool> InitializeDatabaseAsync()
        {
            try
            {
                // check if directory of db exists
                var directory = Path.GetDirectoryName(_databasePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory!);
                }

                // check if db exists
                if (!File.Exists(_databasePath))
                {
                    Debug.WriteLine("Database file does not exist. Creating a new one.");
                    File.Create(_databasePath).Dispose();

                    using (var connection = GetConnection())
                    {
                        Debug.WriteLine("Creating tables...");
                        await connection.CreateTableAsync<Car>();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing database: {ex.Message}");
                return false;
            }
        }
    }
}
