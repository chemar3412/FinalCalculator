using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Final
{
    // ================================
    // BASIC CALCULATOR
    // ================================
    public class Calculator
    {
        public static double Add(double a, double b) => a + b;
        public static double Subtract(double a, double b) => a - b;
        public static double Multiply(double a, double b) => a * b;

        public static double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Cannot divide by 0.");

            return a / b;
        }
    }

    // ================================
    // MEMORY SYSTEM
    // ================================
    public class MemoryFunction
    {
        private List<double> memoryValues = new List<double>();
        private const int MaxSize = 10;

        public bool StoreValue(double value)
        {
            if (memoryValues.Count < MaxSize)
            {
                memoryValues.Add(value);
                return true;
            }
            return false;
        }

        public double? RetrieveValue(int index)
        {
            if (index >= 0 && index < memoryValues.Count)
                return memoryValues[index];

            return null;
        }

        public bool ReplaceValue(int index, double newValue)
        {
            if (index >= 0 && index < memoryValues.Count)
            {
                memoryValues[index] = newValue;
                return true;
            }
            return false;
        }

        public bool RemoveValue(int index)
        {
            if (index >= 0 && index < memoryValues.Count)
            {
                memoryValues.RemoveAt(index);
                return true;
            }
            return false;
        }

        public void ClearValues() => memoryValues.Clear();

        public List<double> GetAllValues() => new List<double>(memoryValues);

        public int GetCount() => memoryValues.Count;

        public double GetSum() => memoryValues.Sum();

        public double GetAverage() =>
            memoryValues.Count == 0 ? 0 : memoryValues.Average();

        public double? GetFirstLastDifference()
        {
            if (memoryValues.Count < 2)
                return null;

            return memoryValues.First() - memoryValues.Last();
        }
    }

    // ================================
    // EQUATION SOLVER
    // ================================
    public class EquationSolver
    {
        public static double Solver(string equation)
        {
            var table = new DataTable();
            var result = table.Compute(equation, "");
            return Convert.ToDouble(result);
        }
    }

    // ================================
    // MULTI-VALUE INPUT
    // ================================
    public static class MultiValueEquationSolver
    {
        public static List<double> GetNumbersFromUser()
        {
            Console.Write("Enter numbers separated by spaces: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input.");
                return new List<double>();
            }

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<double> numbers = new List<double>();

            foreach (string part in parts)
            {
                if (double.TryParse(part, out double value))
                    numbers.Add(value);
                else
                    Console.WriteLine($"'{part}' is not valid and was ignored.");
            }

            return numbers;
        }
    }

    // ================================
    // MULTI-VALUE OPERATIONS
    // ================================
    public static class MultiValueOperations
    {
        public static double Add(List<double> nums) => nums.Sum();

        public static double Subtract(List<double> nums)
        {
            double result = nums[0];
            for (int i = 1; i < nums.Count; i++)
                result -= nums[i];
            return result;
        }

        public static double Multiply(List<double> nums)
        {
            double result = 1;
            foreach (double n in nums)
                result *= n;
            return result;
        }

        public static double Divide(List<double> nums)
        {
            double result = nums[0];

            for (int i = 1; i < nums.Count; i++)
            {
                if (nums[i] == 0)
                {
                    Console.WriteLine("Error: Division by zero detected.");
                    return double.NaN;
                }
                result /= nums[i];
            }

            return result;
        }
    }

    // ================================
    // MAIN PROGRAM
    // ================================
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Final Project — Chelsea Martin");
            Console.WriteLine("Welcome to the Calculator!");

            RunMenuLoop();
        }

        // ------------------------------
        // INPUT HELPERS
        // ------------------------------
        static string GetChoice()
        {
            Console.Write("Select an option: ");
            return Console.ReadLine() ?? "";
        }

        static double GetNumber(string label)
        {
            while (true)
            {
                Console.Write($"Enter {label}: ");
                if (double.TryParse(Console.ReadLine(), out double num))
                    return num;

                Console.WriteLine("Invalid number. Try again.");
            }
        }

        // ------------------------------
        // MAIN MENU LOOP
        // ------------------------------
        static void RunMenuLoop()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== MAIN MENU ===");
                Console.WriteLine("1. Add");
                Console.WriteLine("2. Subtract");
                Console.WriteLine("3. Multiply");
                Console.WriteLine("4. Divide");
                Console.WriteLine("5. Enter an Equation");
                Console.WriteLine("6. Memory Function");
                Console.WriteLine("7. Multi-Value Equation");
                Console.WriteLine("8. Exit");

                string choice = GetChoice();

                switch (choice)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                        ProcessBasicOperation(choice);
                        break;

                    case "7":
                        MultiValueMenu();
                        break;

                    case "8":
                        Console.WriteLine("Exiting Calculator. Goodbye!");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        // ------------------------------
        // BASIC OPERATIONS HANDLER
        // ------------------------------
        static void ProcessBasicOperation(string choice)
        {
            switch (choice)
            {
                case "1":
                {
                    double a = GetNumber("first number");
                    double b = GetNumber("second number");
                    Console.WriteLine($"{a} + {b} = {Calculator.Add(a, b):F2}");
                    break;
                }

                case "2":
                {
                    double a = GetNumber("first number");
                    double b = GetNumber("second number");
                    Console.WriteLine($"{a} - {b} = {Calculator.Subtract(a, b):F2}");
                    break;
                }

                case "3":
                {
                    double a = GetNumber("first number");
                    double b = GetNumber("second number");
                    Console.WriteLine($"{a} * {b} = {Calculator.Multiply(a, b):F2}");
                    break;
                }

                case "4":
                {
                    double a = GetNumber("first number");
                    double b = GetNumber("second number");

                    try
                    {
                        Console.WriteLine($"{a} / {b} = {Calculator.Divide(a, b):F2}");
                    }
                    catch (DivideByZeroException)
                    {
                        Console.WriteLine("Cannot divide by zero.");
                    }
                    break;
                }

                case "5":
                {
                    Console.Write("Enter equation: ");
                    string? eq = Console.ReadLine();

                    try
                    {
                        double result = EquationSolver.Solver(eq);
                        Console.WriteLine($"Result: {result:F2}");
                    }
                    catch
                    {
                        Console.WriteLine("Invalid equation.");
                    }
                    break;
                }

                case "6":
                    MemoryMenu();
                    break;
            }
        }

        // ------------------------------
        // MEMORY MENU
        // ------------------------------
        static void MemoryMenu()
        {
            MemoryFunction memory = new MemoryFunction();
            bool inMenu = true;

            while (inMenu)
            {
                Console.WriteLine("\n=== MEMORY MENU ===");
                Console.WriteLine("1. Store a value");
                Console.WriteLine("2. Retrieve a value");
                Console.WriteLine("3. Replace a value");
                Console.WriteLine("4. Remove a value");
                Console.WriteLine("5. Clear all values");
                Console.WriteLine("6. Display all values");
                Console.WriteLine("7. Count values");
                Console.WriteLine("8. Sum values");
                Console.WriteLine("9. Average values");
                Console.WriteLine("10. First-last difference");
                Console.WriteLine("11. Exit Memory Menu");

                string choice = GetChoice();

                switch (choice)
                {
                    case "1":
                        double v = GetNumber("value");
                        Console.WriteLine(memory.StoreValue(v)
                            ? "Stored."
                            : "Memory full.");
                        break;

                    case "2":
                        int r = (int)GetNumber("index");
                        var val = memory.RetrieveValue(r);
                        Console.WriteLine(val.HasValue ? $"Value: {val}" : "Invalid index.");
                        break;

                    case "3":
                        int idx = (int)GetNumber("index");
                        double nv = GetNumber("new value");
                        Console.WriteLine(memory.ReplaceValue(idx, nv)
                            ? "Replaced."
                            : "Invalid index.");
                        break;

                    case "4":
                        int rem = (int)GetNumber("index");
                        Console.WriteLine(memory.RemoveValue(rem)
                            ? "Removed."
                            : "Invalid index.");
                        break;

                    case "5":
                        memory.ClearValues();
                        Console.WriteLine("Memory cleared.");
                        break;

                    case "6":
                        var all = memory.GetAllValues();
                        if (all.Count == 0)
                            Console.WriteLine("Memory empty.");
                        else
                            for (int i = 0; i < all.Count; i++)
                                Console.WriteLine($"[{i}] {all[i]}");
                        break;

                    case "7":
                        Console.WriteLine($"Count: {memory.GetCount()}");
                        break;

                    case "8":
                        Console.WriteLine($"Sum: {memory.GetSum()}");
                        break;

                    case "9":
                        Console.WriteLine($"Average: {memory.GetAverage()}");
                        break;

                    case "10":
                        var diff = memory.GetFirstLastDifference();
                        Console.WriteLine(diff.HasValue
                            ? $"Difference: {diff}"
                            : "Not enough values.");
                        break;

                    case "11":
                        inMenu = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        // ------------------------------
        // MULTI-VALUE MENU
        // ------------------------------
        static void MultiValueMenu()
        {
            List<double> numbers = MultiValueEquationSolver.GetNumbersFromUser();

            if (numbers.Count == 0)
            {
                Console.WriteLine("No valid numbers.");
                return;
            }

            Console.WriteLine("\nSelect operation:");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Subtract");
            Console.WriteLine("3. Multiply");
            Console.WriteLine("4. Divide");

            string choice = GetChoice();
            double result;

            switch (choice)
            {
                case "1":
                    result = MultiValueOperations.Add(numbers);
                    Console.WriteLine($"Result: {result:F2}");
                    break;

                case "2":
                    result = MultiValueOperations.Subtract(numbers);
                    Console.WriteLine($"Result: {result:F2}");
                    break;

                case "3":
                    result = MultiValueOperations.Multiply(numbers);
                    Console.WriteLine($"Result: {result:F2}");
                    break;

                case "4":
                    result = MultiValueOperations.Divide(numbers);
                    if (!double.IsNaN(result))
                        Console.WriteLine($"Result: {result:F2}");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
