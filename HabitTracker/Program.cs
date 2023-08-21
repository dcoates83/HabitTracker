using System.Data.SQLite;

namespace HabitTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var running = true;
            string _dataBaseString = "Data Source=HabitTracker.db";

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
                    case "1":
                        SQLConnection.ViewAllRecords(_dataBaseString);
                        break;
                    case "2":
                        Console.WriteLine("What habit do you want to enter");
                        var habit = Console.ReadLine();
                        Console.WriteLine("What is the quantity");
                        var quantityString = Console.ReadLine();
                        var quantity = Convert.ToInt32(quantityString);
                        SQLConnection.InsertRecord(habit, quantity, _dataBaseString);
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine("Invalid Response");
                        break;
                }
            }
        }


    }
    public class SQLConnection
    {

        public static void CreateTable(string conn)
        {

            using (var sqlConn = new SQLiteConnection(conn))
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
        }
        public static void InsertRecord(string habit, int quantity, string conn)
        {
            using (var sqlConn = new SQLiteConnection(conn))
            {

                sqlConn.Open();
                var tblCommand = sqlConn.CreateCommand();
                tblCommand.CommandText =
                    $@"INSERT INTO habits
                       (name, quantity) VALUES('{habit}',{quantity})";
                tblCommand.ExecuteNonQuery();
                sqlConn.Close();
            }
        }
        public static void ViewAllRecords(string conn)
        {
            using (var sqlConn = new SQLiteConnection(conn))
            {
                sqlConn.Open();
                var tblCommand = sqlConn.CreateCommand();
                tblCommand.CommandText = "SELECT * FROM Habits";
                tblCommand.ExecuteNonQuery();
                var reader = tblCommand.ExecuteReader();
                List<Habit> tableData = new();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var _id = (long)reader.GetValue(0);
                        var _name = (string)reader.GetValue(1);
                        var _quantity = reader.IsDBNull(2) ? 0 : reader.GetFieldValue<long>(2);


                        Console.WriteLine(_id.ToString(), _name.ToString(), _quantity.ToString());

                        tableData.Add(new Habit
                        {
                            Id = (int)_id,
                            Name = String.IsNullOrWhiteSpace(_name) ? "" : _name,
                            Quantity = (int)_quantity
                        });
                    }
                }
                else
                {
                    Console.WriteLine("No Rows Found");
                }
                Console.WriteLine("-----------------------------------\n");
                foreach (var row in tableData)
                {
                    Console.WriteLine($"ID:{row.Id} - Name:{row.Name} - Quantity:{row.Quantity}");
                }
                Console.WriteLine("-----------------------------------\n");
                sqlConn.Close();
            }
        }
    }
}

