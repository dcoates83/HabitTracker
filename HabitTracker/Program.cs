﻿namespace HabitTracker
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
@"
---------------------------
What would you like to do?

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

                        if (String.IsNullOrWhiteSpace(habit) || String.IsNullOrWhiteSpace(quantityString))
                        {
                            Console.WriteLine("Invalid Response");
                            break;
                        }
                        var quantity = Convert.ToInt32(quantityString);
                        SQLConnection.InsertRecord(habit, quantity, _dataBaseString);
                        break;
                    case "3":
                        Console.WriteLine("What habit do you want to Delete");
                        var _deleteHabit = Console.ReadLine();

                        if (String.IsNullOrWhiteSpace(_deleteHabit))
                        {
                            Console.WriteLine("Invalid Response");
                            break;
                        }

                        SQLConnection.DeleteRecord(_deleteHabit, _dataBaseString);
                        break;
                    case "4":

                        Console.WriteLine("Please enter the ID of the habit you want to update");
                        var _updateID = Console.ReadLine();
                        int _id;

                        bool isId = int.TryParse(_updateID, out _id);


                        if (isId)
                        {
                            Console.WriteLine("Do you want to update the quantity or the habit?");
                            var _whatToUpdate = Console.ReadLine();

                            if (_whatToUpdate == "habit")
                            {
                                Console.WriteLine("Please enter the new habit");
                                var _reqHabit = Console.ReadLine();

                                if (_reqHabit != null)
                                {
                                    SQLConnection.UpdateRecord(_id, _dataBaseString, _reqHabit);
                                }
                                else
                                {
                                    Console.WriteLine("Please enter a valid response");
                                }
                            }
                            else if (_whatToUpdate == "quantity")
                            {
                                Console.WriteLine("Please enter the new Value");
                                var _reqQuantity = Console.ReadLine();
                                int _newQuantity;
                                var _canConvert = int.TryParse(_reqQuantity, out _newQuantity);
                                if (_canConvert)
                                {
                                    SQLConnection.UpdateRecord(_id, _dataBaseString, _newQuantity);
                                }
                                else
                                {
                                    Console.WriteLine("Error converting the number");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Type");
                            }


                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid ID");
                        }




                        break;
                    default:
                        Console.WriteLine("Invalid Response");
                        break;
                }
            }
        }


    }
}

