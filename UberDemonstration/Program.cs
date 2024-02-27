using System;
using System.Collections.Generic;
using System.Linq;

//enum of boroughs
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

//teams
public struct Team
{
    public LondonBorough TeamBorough;
    List<Customer> CustomerList = new List<Customer>();
    public int TotalCO2 = 0;

    public Team(LondonBorough team)
    {
        TeamBorough = team;
        Console.WriteLine($"Borough: {TeamBorough}");

        for (int i = 0; i < 5; i++)
        {
            Customer customer = new Customer(TeamBorough);
            Console.WriteLine($"Name: {customer.Name} \t CO2: {customer.CO2}");
            CustomerList.Add(customer);

            //Calculate an overall CO2 for each team
            TotalCO2 += customer.CO2;
        }
    }
}

//Struct for customers (randomised)
public struct Customer
{
    public string Name;
    public LondonBorough Borough;
    public int CO2;

    private static string[] names = { "Liam", "Emma", "Noah", "Ava", "Evan", "Ella", "Owen", "Grace", "Luke", "Lily", "Eli", "Ruby", "Aiden", "Maya", "Caleb", "Chloe" };
    private static Random random = new Random();

    public Customer(LondonBorough borough)
    {
        Name = names[random.Next(names.Length)];
        Borough = borough;
        CO2 = random.Next(1, 100); ;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Check for dupes
        List<LondonBorough> boroughs = new List<LondonBorough>();
        Random random = new Random();
        for (int i = 0; i < 3; i++)
        {
            LondonBorough randomBorough = (LondonBorough)random.Next(Enum.GetValues(typeof(LondonBorough)).Length);
            boroughs.Add(randomBorough);
        }

        // Create a dictionary to store CO2 totals for each borough
        Dictionary<LondonBorough, int> boroughCO2 = new Dictionary<LondonBorough, int>();

        // Go through each borough, create a team, and calculate total CO2
        foreach (LondonBorough borough in Enum.GetValues(typeof(LondonBorough)))
        {
            Team team = new Team(borough);
            boroughCO2.Add(borough, team.TotalCO2);
            Console.WriteLine($"Total C02: {team.TotalCO2}");
            Console.WriteLine("");
        }

        // Group boroughs by CO2 total
        var groupedBoroughs = boroughCO2.GroupBy(x => x.Value);

        // Display leaderboard
        Console.WriteLine("Leaderboard:");

        int rank = 1;
        foreach (var group in groupedBoroughs.OrderBy(x => x.Key))
        {
            Console.WriteLine($"Rank {rank}:");
            foreach (var item in group)
            {
                Console.WriteLine($"{item.Key} (CO2: {item.Value})");
            }
            rank++;
        }
    }
}