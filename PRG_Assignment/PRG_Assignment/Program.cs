﻿//==========================================================
// Student ID   : S10241624
// Student Name : Hendrik Yong
// Partner ID   : S10257971
// Partner Name : Caden Toh
//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using System.Xml.Schema;

namespace PRG_Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Dictionary<int, Customer> customerDict = InitData();
            while (true)
            {
                MenuInterface();
                string option = Console.ReadLine();
                if (option == "1")
                {
                    Option1(customerDict);
                    Console.WriteLine();
                }
                else if (option == "2")
                {
                    Option2(customerDict);
                    Console.WriteLine();
                }
                else if (option == "3")
                {
                    Option3 (customerDict);
                    Console.WriteLine();
                }
                else if (option == "4")
                {
                    Option4 (customerDict);
                    Console.WriteLine();
                }
                else if (option == "5")
                {
                    Option5(customerDict);
                    Console.WriteLine();
                }
                else if (option == "6")
                {
                    Option6(customerDict);
                    Console.WriteLine();
                }
                else if (option == "0")
                {
                    Console.WriteLine();
                    Console.WriteLine("Thank you! Goodbye :D");
                    break;  
                }
                else
                {
                    Console.WriteLine("Please return a valid input. An integer between 0-6 inclusive!");
                }
            }
        }

        static void MenuInterface()
        {
            Console.Write("================= MENU INTERFACE =================\n[1] List all customers\n[2] List all current orders\n[3] Register" +
                " a new customer\n[4] Create a customer's order\n[5] Display order details of a customer\n[6] Modify order details\n" +
                "[0] Exit\n==================================================\nEnter option: ");
        }

        static Dictionary<int, Customer> InitData()
        {
            Dictionary<int, Customer> customerDict = new Dictionary<int, Customer>();
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                string header = sr.ReadLine();
                //Console.WriteLine(header);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] contents = line.Split(',');
                        if (int.TryParse(contents[1], out int memId) &&
                            DateTime.TryParse(contents[2], out DateTime dob))
                        {
                            Customer customer = new Customer(contents[0], memId, dob);
                            customer.Rewards.Tier = contents[3];
                            customer.Rewards.Points = int.Parse(contents[4]);
                            customer.Rewards.PunchCard = int.Parse(contents[5]);
                            customerDict.Add(memId, customer);
                        }
                    }
            }

            return customerDict;
        }


        static void Option1(Dictionary<int, Customer> customerDict)
        {
            Console.WriteLine("{0,-11}{1,-11}{2}","Name","Member Id","DOB");
            foreach (var customer in customerDict)
            {
                Console.WriteLine(customer.Value);
            }
        }

        static void Option2(Dictionary<int, Customer> customerDict)
        {
            Queue<string> goldQueue = new Queue<string>();
            Queue<string> regularQueue = new Queue<string>();

            using (StreamReader sr = new StreamReader("orders.csv"))
            {
                string header = sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (int.TryParse(parts[1], out int memberId))
                    {
                        if (customerDict.ContainsKey(memberId))
                        {
                            Customer customer = customerDict[memberId];
                            if (customer.Rewards.Tier == "Gold")
                            {
                                goldQueue.Enqueue(line);
                            }
                            else
                            {
                                regularQueue.Enqueue(line);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Gold Member's Queue");
            Console.WriteLine("{0,-3}{1,-10}{2,-18}{3,-18}{4,-8}{5,-8}{6,-8}{7,-15}{8,-12}{9,-12}{10,-12}{11,-12}{12,-12}{13,-12}{14}"
                    , "Id", "MemberId", "Time received", "Time fulfilled", "Option", "Scoops", "Dipped",
                    "WaffleFlavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");
            foreach (var order in goldQueue)
            {
                string[] contents = order.Split(',');
                Console.WriteLine("{0,-3}{1,-10}{2,-18}{3,-18}{4,-8}{5,-8}{6,-8}{7,-15}{8,-12}{9,-12}{10,-12}{11,-12}{12,-12}{13,-12}{14}",
                    contents[0], contents[1], contents[2], contents[3], contents[4], contents[5], contents[6], contents[7], contents[8], contents[9],
                    contents[10], contents[11], contents[12], contents[13], contents[14]);
            }

            Console.WriteLine();
            Console.WriteLine("Regular Member's Queue");
            Console.WriteLine("{0,-3}{1,-10}{2,-18}{3,-18}{4,-8}{5,-8}{6,-8}{7,-15}{8,-12}{9,-12}{10,-12}{11,-12}{12,-12}{13,-12}{14}"
                , "Id", "MemberId", "Time received", "Time fulfilled", "Option", "Scoops", "Dipped",
                "WaffleFlavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");
            foreach (var order in regularQueue)
            {
                string[] contents = order.Split(',');
                Console.WriteLine("{0,-3}{1,-10}{2,-18}{3,-18}{4,-8}{5,-8}{6,-8}{7,-15}{8,-12}{9,-12}{10,-12}{11,-12}{12,-12}{13,-12}{14}",
                    contents[0], contents[1], contents[2], contents[3], contents[4], contents[5], contents[6], contents[7], contents[8], contents[9],
                    contents[10], contents[11], contents[12], contents[13], contents[14]);
            }
        }

        static void Option3(Dictionary<int, Customer> customerDict)
        {
            Console.Write("Enter customer name: ");
            var name = Console.ReadLine().Trim();
            Console.Write("Enter customer Id: ");
            var id = Convert.ToInt32(Console.ReadLine().Trim());
            Console.Write("Enter customer DOB: ");
            var dob = Convert.ToDateTime(Console.ReadLine().Trim());
            Customer customer = new Customer(name, id, dob);
            //customer.Rewards.Tier = "Ordinary"; 
            customerDict.Add(id, customer);


            using (StreamWriter sw = new StreamWriter("customers.csv", true, Encoding.UTF8))
            {
                sw.Write($"{customer.Name},{customer.MemberID},{customer.Dob.ToShortDateString()},{customer.Rewards.Tier},{customer.Rewards.Points},{customer.Rewards.PunchCard}\n");
                Console.WriteLine("Customer successfully appended to customers.csv");
            }
        }

        static void Option4(Dictionary<int, Customer> customerDict)
        {
            //list customers from csv
            Console.WriteLine("List of customers");
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                var header = sr.ReadLine();
                int i = 1;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] customerdata = line.Split(',');
                    Console.WriteLine($"[{i++}] {customerdata[0]}");
                }
                Option4Order(customerDict);
            }
            
        }

        static void Option5(Dictionary<int, Customer> customerDict)
        {
            Console.WriteLine("List of customers");
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                var header = sr.ReadLine();

                int i = 1;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] customerdata = line.Split(',');
                    Console.WriteLine($"{i++}. {customerdata[0]}");
                }
                Console.WriteLine();
                Console.Write("Enter selected customer: ");
                var selectedCustomer = Console.ReadLine().Trim().ToLower();

                foreach (var customer in customerDict)
                {
                    if (customer.Value.Name.ToLower() == selectedCustomer)
                    {
                        //need to retrieve order obj and past and current order 
                        //for each order display details of the order incl datetime received, fulfilled and all ice cream details associated with that order 

                    }
                }

            }
        }

        static void Option6(Dictionary<int, Customer> customerDict)
        {
            Console.WriteLine("List of customers");
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                var header = sr.ReadLine();

                int i = 1;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] customerdata = line.Split(',');
                    Console.WriteLine($"{i++}. {customerdata[0]}");
                }
            }
        }

        static void Option4Order(Dictionary<int, Customer> customerDict)
        {
            List<string> options = new List<string>() { "cup", "cone", "waffle" };
            List<string> flavours = new List<string>() { "Vanilla", "Chocolate", "Strawberry", "Durian", "Ube", "Sea salt" };
            List<string> toppings = new List<string>() { "Sprinkles", "Mochi", "Sago", "Oreos" };
            List<string> specialFlavour = new List<string>() { "Red velvet", "Charcoal", "Pandan Waffle" };
            List<string> customerOptions = new List<string>();

            Console.Write("Select a customer: ");
            var selectedCustomer = Console.ReadLine().Trim().ToLower();

            foreach (var customer in customerDict)
            {
                //Check if selected customer exist 
                if (customer.Value.Name.Trim().ToLower().Contains(selectedCustomer))
                {
                    Order order = new Order(customer.Value.MemberID, DateTime.Now);

                    Console.WriteLine();
                    Console.WriteLine("Select Ice Cream Option");
                    int i = 1;
                    foreach (var option in options)   
                    {
                        Console.WriteLine($"[{i++}] {option}");
                    }
                    //choose waffle, cone or cup
                    var selectedOption = 0;

                    // Validation for IceCream option
                    while (true)
                    {
                        try
                        {
                            Console.Write("Ice Cream Option: ");
                            selectedOption = Convert.ToInt32(Console.ReadLine());
                            if (selectedOption > 0 && selectedOption < 4)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Please ensure an integer between 1-3");
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Ensure that an integer between 1-3 is entered");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Error occurred.");
                        }
                    }

                    //Scoops, flavour and topping

                    //Initialise List

                    List<Flavour> userFlavourList = new List<Flavour>();
                    List<Topping> userToppingList = new List<Topping>();

                    //Choose no. of scoops 
                    var scoops = 0;
                    while (true)
                    {
                        try
                        {
                            Console.Write("Enter number of scoops (1-3): ");
                            scoops = Convert.ToInt32(Console.ReadLine());
                            if (scoops > 0 && scoops < 4)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Please enter an integer between 1-3.");
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Please enter an integer between 1-3.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                    //List containing flavours chosen by user 
                    int inputtedFlavours = 0;
                    while (inputtedFlavours != scoops)
                    {
                        int counter = 1;
                        Console.WriteLine();
                        Console.WriteLine("Flavour Options");
                        foreach (var flavour in flavours)
                        {
                            Console.WriteLine($"[{counter++}] {flavour}");
                        }
                        //Choose flavour 
                        var userFlavour = 0;
                        while (true)
                        {
                            try
                            {
                                Console.Write("Select Flavour: ");
                                userFlavour = Convert.ToInt32(Console.ReadLine().Trim());
                                if (userFlavour > 0 && userFlavour < 7)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an integer between 1-6.");
                                }
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("Please enter an integer between 1-6.");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("Please enter an integer between 1-6.");
                            }
                        }

                        //Number of selected flavour 
                        var userFlavourNo = 0;

                        //Check that inputted flavours do not exceed number of scoops 
                        while (true)
                        {

                            //Validate input type
                            while (true)
                            {
                                try
                                {
                                    Console.Write($"No. of {flavours[userFlavour - 1]}:");
                                    userFlavourNo = Convert.ToInt32(Console.ReadLine().Trim());
                                    break;
                                }
                                catch (FormatException e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Enter a valid integer");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Enter a valid integer");
                                }
                            }

                            //Validate if inputted number is within range
                            if (userFlavourNo == 0)
                            {
                                Console.WriteLine("Number of flavour cannot be 0.");
                            }
                            else if ((inputtedFlavours + userFlavourNo) > scoops)
                            {
                                Console.WriteLine($"Exceeded number of flavours, please make sure its less than {scoops}");
                            }
                            else
                            {
                                inputtedFlavours += userFlavourNo;
                                //Apending flavour to flavourList 
                                if (userFlavour > 3)
                                {
                                    userFlavourList.Add(new Flavour(flavours[userFlavour - 1], false, userFlavourNo));
                                }
                                else
                                {
                                    userFlavourList.Add(new Flavour(flavours[userFlavour - 1], true, userFlavourNo));
                                }
                                break;
                            }
                        }
                    }
                    Console.WriteLine();
                    //Ask if user want topping 
                    var wantTopping = "";
                    while (true)
                    {
                        Console.Write("Do you want toppings[y/n]: ");
                        wantTopping = Console.ReadLine().Trim().ToLower();
                        if (wantTopping == "y" || wantTopping == "n")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter either a 'y' or 'n'");
                        }
                    }
                    if (wantTopping == "y")
                    {
                        for (int m = 1; m <= 4; m++)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Choose Topping");
                            Console.WriteLine($"[1] {toppings[0]}");
                            Console.WriteLine($"[2] {toppings[1]}");
                            Console.WriteLine($"[3] {toppings[2]}");
                            Console.WriteLine($"[4] {toppings[3]}");
                            Console.Write("Select Topping: ");
                            var userTopping = Convert.ToInt32(Console.ReadLine().Trim());
                            userToppingList.Add(new Topping(toppings[userTopping - 1]));

                            Console.Write("Do you want another topping?[y/n]: ");
                            var anotherTopping = Console.ReadLine().Trim().ToLower();
                            if (anotherTopping == "n")
                            {
                                break;
                            }
                        }
                    }

                    //If Cup is chosen 
                    if (selectedOption == 1)
                    {
                        IceCream cup = new Cup("Cup", scoops, userFlavourList, userToppingList);
                        Console.WriteLine(cup.ToString());
                    }
                    //If Cone is chosen
                    else if (selectedOption == 2)
                    {
                        Console.Write("Would you like your cone to be dipped in chocolate [y/n]: ");
                        string option = Console.ReadLine();
                        if (option == "y")
                        {
                            IceCream cone = new Cone("Cone", scoops, userFlavourList, userToppingList);
                        }
                    }
                    else if (selectedOption == 3)
                    {
                        Console.Write("Would you like your waffle to be a special flavour [y/n]: ");
                        string option = Console.ReadLine(); 
                        if (option == "y")
                        {
                            foreach (string flavour in specialFlavour)
                            {
                                int counter = 1;
                                Console.WriteLine($"[{counter}] {flavour}");
                            }
                            int specialFlavourOption = Convert.ToInt32(Console.ReadLine().Trim());
                            IceCream waffle = new Waffle("Waffle", scoops, userFlavourList, userToppingList, specialFlavour[specialFlavourOption-1]);
                        }
                        else
                        {
                            IceCream waffle = new Waffle("Waffle", scoops, userFlavourList, userToppingList, "Original");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Option entered Invalid.");
                    }
                }
            }
        }

    }
}
