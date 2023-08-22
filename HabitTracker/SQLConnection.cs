using System.Data.SQLite;

namespace HabitTracker
{
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
        public static void DeleteRecord(string habit, string conn)
        {
            using (var sqlConn = new SQLiteConnection(conn))
            {
                sqlConn.Open();
                var tblCommand = sqlConn.CreateCommand();
                tblCommand.CommandText =
$@"
DELETE FROM habits
WHERE habits.name = '{habit}';
";
                tblCommand.ExecuteNonQuery();
                sqlConn.Close();
            }
        }
        public static void UpdateRecord(int id, string conn, string? habit)
        {
            using (var sqlConn = new SQLiteConnection(conn))
            {

                sqlConn.Open();
                var tblCommand = sqlConn.CreateCommand();


                if (!String.IsNullOrWhiteSpace(habit))
                {
                    tblCommand.CommandText =
                                $@"
UPDATE habits
SET name = '{habit}'
WHERE habits.Id  = {id};
";
                    tblCommand.ExecuteNonQuery();
                }

                sqlConn.Close();
            }
        }
        public static void UpdateRecord(int id, string conn, int? quantity)
        {
            using (var sqlConn = new SQLiteConnection(conn))
            {

                sqlConn.Open();
                var tblCommand = sqlConn.CreateCommand();

                if (quantity != null)
                {
                    tblCommand.CommandText =
                 $@"
UPDATE habits
SET quantity = {quantity}
WHERE habits.Id  = {id};
";
                    tblCommand.ExecuteNonQuery();
                }


                sqlConn.Close();
            }
        }
    }
}

