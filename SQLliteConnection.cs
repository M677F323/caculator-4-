using Calculator.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class  Constants
    {
        public const string DatabaseFilename = "MyHistorySQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }

    public class SQLliteConnection
    {
        SQLiteAsyncConnection Database;
        
        public void DatabaseConnection()
        {
            Init();
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<CalculationHistory>();
        }

        public async Task<List<CalculationHistory>> GetHistoryAsync()
        {
            await Init();
            return await Database.Table<CalculationHistory>().ToListAsync();
        }

        public async Task<int> SaveItemAsync(CalculationHistory item)
        {
            await Init();
            return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteAllAsync()
        {
            await Init();
            return await Database.DeleteAllAsync<CalculationHistory>();
        }
    
    
    }
}
