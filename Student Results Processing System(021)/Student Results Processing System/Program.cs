using System;

namespace StudentResultsProcessingSystem
{
    class Student
    {
        public string FullName { get; set; } = "";
        public string StudentId { get; set; } = "";
        public string Programme { get; set; } = "";
        public string Level { get; set; } = "";
        public double[] Scores { get; set; } = new double[5];

        public double Total => Scores.Sum();
        public double Average => Total / Scores.Length;

        public string Grade
        {
            get
            {
                double a = Average;
                if (a >= 80) return "A";
                if (a >= 70) return "B";
                if (a >= 60) return "C";
                if (a >= 50) return "D";
                return "F";
            }
        }

        public string Status => Average >= 50 ? "Passed" : "Failed";
    }

    class Program
    {
        static readonly string[] Courses =
        {
            "Programming with C#",
            "Database Systems",
            "Computer Networks",
            "Web Development",
            "Mathematics for Computing"
        };

        static Student[] students = new Student[3];
        static bool hasData = false;

        static void Main()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== STUDENT RESULTS PROCESSING SYSTEM =====");
                Console.WriteLine("1. Enter Student Results");
                Console.WriteLine("2. View Student Report");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? "";
                switch (choice.Trim())
                {
                    case "1": EnterResults(); break;
                    case "2":
                        if (!hasData)
                            Console.WriteLine("No data yet. Please enter results first (option 1).");
                        else
                            ViewReport();
                        break;
                    case "3":
                        Console.WriteLine("Thank you for using the Student Results Processing System.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                        break;
                }
            }
        }

        static void EnterResults()
        {
            for (int i = 0; i < students.Length; i++)
            {
                Console.WriteLine($"\nEnter details for Student {i + 1}");
                var s = new Student();

                Console.Write("Enter full name: ");
                s.FullName = Console.ReadLine() ?? "";
                Console.Write("Enter student ID: ");
                s.StudentId = Console.ReadLine() ?? "";
                Console.Write("Enter programme: ");
                s.Programme = Console.ReadLine() ?? "";
                Console.Write("Enter level: ");
                s.Level = Console.ReadLine() ?? "";

                for (int c = 0; c < Courses.Length; c++)
                    s.Scores[c] = ReadValidScore($"Enter score for {Courses[c]}: ");

                students[i] = s;
            }
            hasData = true;
            Console.WriteLine("\nAll student results captured successfully.");
        }

        static double ReadValidScore(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double score) && score >= 0 && score <= 100)
                    return score;
                Console.WriteLine("Invalid score. Score must be between 0 and 100.");
            }
        }

        static void ViewReport()
        {
            Console.WriteLine("\n===== STUDENT RESULTS REPORT =====");
            foreach (var s in students)
            {
                Console.WriteLine();
                Console.WriteLine($"Student Name: {s.FullName}");
                Console.WriteLine($"Student ID: {s.StudentId}");
                Console.WriteLine($"Programme: {s.Programme}");
                Console.WriteLine($"Level: {s.Level}");
                for (int c = 0; c < Courses.Length; c++)
                    Console.WriteLine($"{Courses[c]}: {s.Scores[c]}");
                Console.WriteLine($"Total Score: {s.Total}");
                Console.WriteLine($"Average Score: {s.Average:F1}");
                Console.WriteLine($"Grade: {s.Grade}");
                Console.WriteLine($"Status: {s.Status}");
                Console.WriteLine(new string('-', 45));
            }

            // Bonus extras (not scorable, but nice)
            var best = students.OrderByDescending(x => x.Average).First();
            var worst = students.OrderBy(x => x.Average).First();
            double classAvg = students.Average(x => x.Average);
            Console.WriteLine($"Best Student: {best.FullName} ({best.Average:F1})");
            Console.WriteLine($"Lowest Average: {worst.FullName} ({worst.Average:F1})");
            Console.WriteLine($"Class Average: {classAvg:F1}");
        }
    }
}
