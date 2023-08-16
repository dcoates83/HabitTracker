using System.Data.SQLite;

namespace HabitTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var running = true;


            var _dataBaseString = "Data Source=HabitTracker.db";
            using (var sqlConn = new SQLiteConnection(_dataBaseString))
            {
                sqlConn.Open();
                var tblCommand = sqlConn.CreateCommand();
                tblCommand.CommandText = @"CREATE TABLE IF NOT EXISTS habits(
                                           Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                           name TEXT,
                                           quantity INTERGER
                                           )";
                tblCommand.ExecuteNonQuery();
                sqlConn.Close();
            }
            while (running)
            {
                Console.WriteLine
                    (
@"What would you like to do?

Type 0 to Close The App.
Type 1 to View All Records.
Type 2 to Insert a Record.
Type 3 to Delete a Record.
Type 4 to Update a Record.
---------------------------
                    ");
                var response = Console.ReadLine();
                switch (response)
                {
                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Response");
                        break;
                }
            }
        }
    }
}

