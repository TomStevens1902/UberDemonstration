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

// teams
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
            Colorful.Console.WriteLine($"Name: {customer.Name,-10} | CO2: {customer.CO2}");
            CustomerList.Add(customer);

            // Calculate an overall CO2 for each team
            TotalCO2 += customer.CO2;
        }
    }
}

// struct for customers (randomised)
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
        DisplayInfo();
        string input = "";
        while (input == "")
        {
            input = System.Console.ReadLine();
            if(input == "")
            {
                System.Console.Clear();
                DisplayInfo();
            }
        }
    }

    static void DisplayInfo()
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
            Colorful.Console.WriteLine($"Total C02: {team.TotalCO2}");
            Colorful.Console.WriteLine("");
        }

        // Group boroughs by CO2 total
        var groupedBoroughs = boroughCO2.GroupBy(x => x.Value);

        // Display leaderboard
        Colorful.Console.WriteLine("Leaderboard:");

        int rank = 1;
        foreach (var group in groupedBoroughs.OrderBy(x => x.Key))
        {
            Colorful.Console.WriteLine($"Rank {rank}:");
            var table = new ConsoleTable("Borough", "CO2 Total");

            foreach (var item in group)
            {
                table.AddRow(item.Key, item.Value);
            }

            // Display table
            table.Write(Format.Minimal);

            rank++;
        }
    }
}

#region oldCode
    //using System;
    //using System.Collections.Generic;
    //using System.Linq;
    //using System.Drawing;
    //using ConsoleTables;
    //using Colorful;

    ////enum of boroughs
    //public enum LondonBorough
    //{
    //    Barking_and_Dagenham,
    //    Barnet,
    //    Bexley,
    //    Brent,
    //    Bromley,
    //    Camden,
    //    Croydon,
    //    Ealing,
    //    Enfield,
    //    Greenwich,
    //    Hackney,
    //    Hammersmith_and_Fulham,
    //    Haringey,
    //    Harrow,
    //    Havering,
    //    Hillingdon,
    //    Hounslow,
    //    Islington,
    //    Kensington_and_Chelsea,
    //    Kingston_upon_Thames,
    //    Lambeth,
    //    Lewisham,
    //    Merton,
    //    Newham,
    //    Redbridge,
    //    Richmond_upon_Thames,
    //    Southwark,
    //    Sutton,
    //    Tower_Hamlets,
    //    Waltham_Forest,
    //    Wandsworth,
    //    Westminster
    //}

    ////teams
    //public struct Team
    //{
    //    public LondonBorough TeamBorough;
    //    List<Customer> CustomerList = new List<Customer>();
    //    public int TotalCO2 = 0;

    //    public Team(LondonBorough team)
    //    {
    //        TeamBorough = team;
    //        System.Console.WriteLine($"Borough: {TeamBorough}");

    //        for (int i = 0; i < 5; i++)
    //        {
    //            Customer customer = new Customer(TeamBorough);
    //            System.Console.WriteLine($"Name: {customer.Name} \t CO2: {customer.CO2}");
    //            CustomerList.Add(customer);

    //            //Calculate an overall CO2 for each team
    //            TotalCO2 += customer.CO2;
    //        }
    //    }
    //}

    ////Struct for customers (randomised)
    //public struct Customer
    //{
    //    public string Name;
    //    public LondonBorough Borough;
    //    public int CO2;

    //    private static string[] names = { "Liam", "Emma", "Noah", "Ava", "Evan", "Ella", "Owen", "Grace", "Luke", "Lily", "Eli", "Ruby", "Aiden", "Maya", "Caleb", "Chloe" };
    //    private static Random random = new Random();

    //    public Customer(LondonBorough borough)
    //    {
    //        Name = names[random.Next(names.Length)];
    //        Borough = borough;
    //        CO2 = random.Next(1, 100); ;
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        DisplayInfo();

    //    }

    //    static void DisplayInfo()
    //    {
    //        // Check for dupes
    //        List<LondonBorough> boroughs = new List<LondonBorough>();
    //        Random random = new Random();
    //        for (int i = 0; i < 3; i++)
    //        {
    //            LondonBorough randomBorough = (LondonBorough)random.Next(Enum.GetValues(typeof(LondonBorough)).Length);
    //            boroughs.Add(randomBorough);
    //        }

    //        // Create a dictionary to store CO2 totals for each borough
    //        Dictionary<LondonBorough, int> boroughCO2 = new Dictionary<LondonBorough, int>();

    //        // Go through each borough, create a team, and calculate total CO2
    //        foreach (LondonBorough borough in Enum.GetValues(typeof(LondonBorough)))
    //        {
    //            Team team = new Team(borough);
    //            boroughCO2.Add(borough, team.TotalCO2);
    //            System.Console.WriteLine($"Total C02: {team.TotalCO2}");
    //            System.Console.WriteLine("");
    //        }

    //        // Group boroughs by CO2 total
    //        var groupedBoroughs = boroughCO2.GroupBy(x => x.Value);

    //        // Display leaderboard
    //        System.Console.WriteLine("Leaderboard:");

    //        int rank = 1;
    //        foreach (var group in groupedBoroughs.OrderBy(x => x.Key))
    //        {
    //            System.Console.WriteLine($"Rank {rank}:");
    //            foreach (var item in group)
    //            {
    //                System.Console.WriteLine($"{item.Key} (CO2: {item.Value})");
    //            }
    //            rank++;
    //        }
    //    }
    //}
#endregion
