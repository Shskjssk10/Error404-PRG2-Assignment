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

namespace PRG_Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, Customer> customerDict = InitData();
            //Option1(customerDict);
            //Option2(customerDict);
            //Option3(customerDict);
            Option4(customerDict);
            //Option5(customerDict);
            //Option6(customerDict);
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

            Console.Write("Select a customer: ");
            var selectedCustomer = Console.ReadLine().Trim().ToLower();
            foreach (var customer in customerDict)
            {
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
                    //Choose Waffle, Cone or Cup 
                    Console.Write("Ice Cream Option: ");
                    var selectedOption = Convert.ToInt32(Console.ReadLine());

                    //If Cup is chosen 
                    if (selectedOption == 1)
                    {
                        //Choose no. of scoops 
                        Console.Write("Enter number of scoops (1-3): ");
                        var scoops = Convert.ToInt32(Console.ReadLine());
                        if (scoops < 1 || scoops > 3)
                        {
                            Console.WriteLine("Invalid scoop number.");
                        }
                        else
                        {
                            //List containing flavours chosen by user 
                            List<Flavour> userFlavourList = new List<Flavour>();
                            int inputtedFlavours = 0;
                            while(inputtedFlavours != scoops)
                            {
                                int counter = 1;
                                Console.WriteLine();
                                Console.WriteLine("Flavour Options");
                                foreach (var flavour in flavours)
                                {
                                    Console.WriteLine($"[{counter++}] {flavour}");
                                }
                                Console.Write("Select Flavour: ");
                                //Choose flavour 
                                var userFlavour = Convert.ToInt32(Console.ReadLine().Trim());
                                //Number of selected flavour 
                                var userFlavourNo = 0;

                                //Check that inputted flavours do not exceed number of scoops 
                                while (true)
                                {
                                    Console.Write($"No. of {flavours[userFlavour - 1]}:");
                                    userFlavourNo = Convert.ToInt32(Console.ReadLine().Trim());

                                    //To validate
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
                            Console.Write("Do you want toppings[y/n]: ");
                            var wantTopping = Console.ReadLine().Trim().ToLower();
                            List<Topping> userToppingList = new List<Topping>();
                            if (wantTopping == "y")
                            {
                                int l = 1;
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
                            Cup cup = new Cup("Cup", scoops, userFlavourList, userToppingList);
                            Console.WriteLine(cup.ToString());

                        }
                    }
                    else if (selectedOption == 2)
                    {

                    }

                    else if (selectedOption == 3)
                    {

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
