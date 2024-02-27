using System;
using System.Collections.Generic;
using System.Drawing;
using ConsoleTables;
using Colorful;

// enum of boroughs
public enum LondonBorough
{
    Barking_and_Dagenham,
    Barnet,
    Bexley,
    Brent,
    Bromley,
    Camden,
    Croydon,
    Ealing,
    Enfield,
    Greenwich,
    Hackney,
    Hammersmith_and_Fulham,
    Haringey,
    Harrow,
    Havering,
    Hillingdon,
    Hounslow,
    Islington,
    Kensington_and_Chelsea,
    Kingston_upon_Thames,
    Lambeth,
    Lewisham,
    Merton,
    Newham,
    Redbridge,
    Richmond_upon_Thames,
    Southwark,
    Sutton,
    Tower_Hamlets,
    Waltham_Forest,
    Wandsworth,
    Westminster


}

// Struct for teams
public struct Team
{
    public LondonBorough TeamBorough;
    public List<Customer> CustomerList;
    public int TotalCO2;

    public Team(LondonBorough team)
    {
        TeamBorough = team;
        CustomerList = new List<Customer>();
        TotalCO2 = 0;

        Colorful.Console.WriteLine($"Borough: {TeamBorough}");

        for (int i = 0; i < 5; i++)
        {
            Customer customer = new Customer(TeamBorough);
            Colorful.Console.WriteLine($"Name: {customer.Name,-10} | Miles: {customer.Miles,-5} | CO2: {customer.CO2}");
            CustomerList.Add(customer);

            // Calculate an overall CO2 for each team
            TotalCO2 += customer.CO2;
        }
    }
}

// Struct for customers (randomized)
public struct Customer
{
    public string Name;
    public LondonBorough Borough;
    public int Miles;
    public int CO2;

    private static string[] names = { "John", "Alice", "David", "Emily", "Michael", "Sophia", "William", "Olivia" };
    private static Random random = new Random();

    // Emission factor: grams per mile
    private const int EmissionFactor = 120; // Example value

    public Customer(LondonBorough borough)
    {
        Name = names[random.Next(names.Length)];
        Borough = borough;
        Miles = random.Next(1, 100); // Random miles traveled
        CO2 = Miles * EmissionFactor; // Calculate CO2 emissions based on miles and emission factor
    }
}

class Program
{
    static void Main(string[] args)
    {
        DisplayInfo();
        string input = "";
        while (input == "")
        {
            input = System.Console.ReadLine();
            if (input == "")
            {
                System.Console.Clear();
                DisplayInfo();
            }
        }
    }
    static void DisplayInfo()
    {
        // Check for duplicates
        List<LondonBorough> boroughs = new List<LondonBorough>();
        Random random = new Random();
        for (int i = 0; i < 3; i++)
        {
            LondonBorough randomBorough = (LondonBorough)random.Next(Enum.GetValues(typeof(LondonBorough)).Length);
            System.Console.WriteLine($"Borough: {randomBorough}", Color.Green);
            boroughs.Add(randomBorough);
        }
        System.Console.WriteLine("");

        // Create a dictionary to store CO2 totals for each borough
        Dictionary<LondonBorough, int> boroughCO2 = new Dictionary<LondonBorough, int>();

        // Go through each borough, create a team, and calculate total CO2
        foreach (LondonBorough borough in Enum.GetValues(typeof(LondonBorough)))
        {
            Team team = new Team(borough);
            boroughCO2.Add(borough, team.TotalCO2);
            System.Console.WriteLine($"Total C02: {team.TotalCO2}");
            System.Console.WriteLine("");
        }

        // Sort the dictionary by CO2 total
        var sortedBoroughCO2 = boroughCO2.OrderBy(x => x.Value);

        // Display leaderboard
        System.Console.WriteLine("Leaderboard:");
        System.Console.WriteLine("|-----|---------------------------|-------------|");
        System.Console.WriteLine("|  #  | Borough                   | CO2 Total   |");
        System.Console.WriteLine("|-----|---------------------------|-------------|");

        int rank = 1;
        foreach (var item in sortedBoroughCO2)
        {
            System.Console.WriteLine($"|  {rank,-2} | {item.Key,-25} | {item.Value,-11} |", Color.White);
            rank++;
        }

        System.Console.WriteLine("|-----|---------------------------|-------------|");
    }
}