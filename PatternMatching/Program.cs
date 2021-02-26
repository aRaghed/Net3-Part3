using CommercialRegistration;

using ConsumerVehicleRegistration;

using LiveryRegistration;

using System;

namespace PatternMatching
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var car = new Car(); //Try adding Passengers
            var taxi = new Taxi(); //Try adding Fares
            var bus = new Bus(); //Try adding Capacity and Riders
            var truck = new DeliveryTruck(); //Try adding GrossWeightClass

            Console.WriteLine($"The toll for a car is {TollCalculator.CalculateToll(car)}");
            Console.WriteLine($"The toll for a taxi is {TollCalculator.CalculateToll(taxi)}");
            Console.WriteLine($"The toll for a bus is {TollCalculator.CalculateToll(bus)}");
            Console.WriteLine($"The toll for a truck is {TollCalculator.CalculateToll(truck)}");

            Console.WriteLine("Are you inbound to or outbound from the city? Press I or O: ");
            ConsoleKey key = default;
            while (key != ConsoleKey.I && key != ConsoleKey.O)
                key = Console.ReadKey(true).Key;
            DateTime now = DateTime.Now;

            Console.WriteLine($"Riding " +
                $"{(key == ConsoleKey.I ? "inbound" : "outbound")} " +
                $"on a " +
                $"{now.DayOfWeek} " +
                $"at " +
                $"{now.TimeOfDay:hh\\:mm\\:ss} " +
                $"will increase your toll by " +
                $"{TollCalculator.PeakTimePremium(DateTime.Now, key == ConsoleKey.I)}x");

            //try
            //{
            //    TollCalculator.CalculateToll("this will fail");
            //}
            //catch (ArgumentException e)
            //{
            //    Console.WriteLine("Caught an argument exception when using the wrong type");
            //}
            //try
            //{
            //    TollCalculator.CalculateToll(null!);
            //}
            //catch (ArgumentNullException e)
            //{
            //    Console.WriteLine("Caught an argument exception when using null");
            //}

            var ages = TollCalculator.GetAge();
            var name = ages.Item2;
            Console.WriteLine(name);
        }
    }

    public class TollCalculator
    {
        public static decimal CalculateToll(object vehicle) =>
            vehicle switch
            {
                Car { Passengers: 0 } => 2.00m + 0.50m,
                Car { Passengers: 1 } => 2.0m,
                Car { Passengers: 2 } => 2.0m - 0.50m,
                Car => 2.00m - 1.0m,
                Taxi { Fares: 0 } => 3.50m + 1.00m,
                Taxi { Fares: 1 } => 3.50m,
                Taxi { Fares: 2 } => 3.50m - 0.50m,
                Taxi => 3.50m - 1.00m,
                Bus b when ((double)b.Riders / (double)b.Capacity) < 0.50 => 5.00m + 2.00m,
                Bus b when ((double)b.Riders / (double)b.Capacity) > 0.90 => 5.00m - 1.00m,
                Bus => 5.00m,
                DeliveryTruck t when (t.GrossWeightClass > 5000) => 10.00m + 5.00m,
                DeliveryTruck t when (t.GrossWeightClass < 3000) => 10.00m - 2.00m,
                DeliveryTruck => 10.00m,
                { } => throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicle)),
                null => throw new ArgumentNullException(nameof(vehicle))
            };

        public static decimal CalculateToll2(object vehicle) =>
            vehicle switch
            {
                Car c => c.Passengers switch
                {
                    0 => 2.00m + 0.5m,
                    1 => 2.0m,
                    2 => 2.0m - 0.5m,
                    _ => 2.00m - 1.0m
                },

                Taxi t => t.Fares switch
                {
                    0 => 3.50m + 1.00m,
                    1 => 3.50m,
                    2 => 3.50m - 0.50m,
                    _ => 3.50m - 1.00m
                },

                Bus b when ((double)b.Riders / (double)b.Capacity) < 0.50 => 5.00m + 2.00m,
                Bus b when ((double)b.Riders / (double)b.Capacity) > 0.90 => 5.00m - 1.00m,
                Bus => 5.00m,

                DeliveryTruck t when (t.GrossWeightClass > 5000) => 10.00m + 5.00m,
                DeliveryTruck t when (t.GrossWeightClass < 3000) => 10.00m - 2.00m,
                DeliveryTruck => 10.00m,

                { } => throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicle)),
                null => throw new ArgumentNullException(nameof(vehicle))
            };

        public static decimal PeakTimePremium(DateTime timeOfToll, bool inbound) =>

            //Use the Weekday, TimeBand and if we are heading outbound from or inbound to the city to calculate a toll factor
            //Use a tuple for the values
            (IsWeekDay(timeOfToll), GetTimeBand(timeOfToll), inbound) switch
            {
                (true, TimeBand.MorningRush, true) => 2.00m,
                (true, TimeBand.MorningRush, false) => 1.00m,
                (true, TimeBand.Daytime, _) => 1.50m,
                (true, TimeBand.EveningRush, true) => 1.00m,
                (true, TimeBand.EveningRush, false) => 2.00m,
                (true, TimeBand.Overnight, _) => 0.75m,
                (false, _, _) => 1.00m,
                (_, _, _) => throw new ArgumentException(message: "Not a valid calculation") //This is the default!
            };

        private static bool IsWeekDay(DateTime timeOfToll) =>

            //Calculate if the day is a weekday or weekend
            timeOfToll.DayOfWeek switch
            {
                DayOfWeek.Saturday => false,
                DayOfWeek.Sunday => false,
                _ => true
            };

        private enum TimeBand
        {
            MorningRush,
            Daytime,
            EveningRush,
            Overnight
        }

        private static TimeBand GetTimeBand(DateTime timeOfToll) =>

            //Add separate tolls for rush hour etc by introducing a TimeBand
            timeOfToll.Hour switch
            {
                < 6 or > 19 => TimeBand.Overnight,
                < 10 => TimeBand.MorningRush,
                < 16 => TimeBand.Daytime,
                _ => TimeBand.EveningRush,
            };

        public static (int, string, int, int, int) GetAge()
        {
            return (10, "Kalle", 12, 13, 14);
        }
    }
}

namespace ConsumerVehicleRegistration
{
    public class Car
    {
        public int Passengers { get; set; }
    }
}

namespace CommercialRegistration
{
    public class DeliveryTruck
    {
        public int GrossWeightClass { get; set; }
    }
}

namespace LiveryRegistration
{
    public class Taxi
    {
        public int Fares { get; set; }
    }

    public class Bus
    {
        public int Capacity { get; set; }

        public int Riders { get; set; }
    }
}