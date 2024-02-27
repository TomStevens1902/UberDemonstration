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

    private static string[] names = { "John", "Alice", "David", "Emily", "Michael", "Sophia", "William", "Olivia" };
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
        // Shuffle the boroughs to avoid duplicates
        List<LondonBorough> boroughs = Enum.GetValues(typeof(LondonBorough)).Cast<LondonBorough>().ToList();
        Random random = new Random();
        for (int i = boroughs.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            LondonBorough temp = boroughs[i];
            boroughs[i] = boroughs[j];
            boroughs[j] = temp;
        }
        // Select how many boroughs you want displayed
        boroughs = boroughs.Take(3).ToList();

        //minimum C02
        List<LondonBorough> minBorough = new List<LondonBorough>();
        int minCO2 = 501; //C02 Is between 0 - 100 and so will always be smaller
        //Go through each borough create a team //Create a set of 5 customers for each enum team
        List<Team> teams = new List<Team>();
        foreach (LondonBorough borough in boroughs)
        {
            Team team = new Team(borough);
            teams.Add(team);
            Console.WriteLine($"Total C02: {team.TotalCO2}");
            Console.WriteLine("");

            //Compare each team lowest = winner
            if (team.TotalCO2 < minCO2)
            {
                minCO2 = team.TotalCO2;

                minBorough.Clear();
                minBorough.Add(team.TeamBorough);
            } else if (team.TotalCO2 <= minCO2)
            {
                minBorough.Add(team.TeamBorough);
            }
        }
        
        //display winner 
        Console.Write($"The Winner(s) Is: ");
        for (int i = 0; i < minBorough.Count; i++)
        {
            if (i != 0)
            {
                Console.Write(", ");
            }
            Console.Write($"{minBorough[i]}"); 
        }
    }
}