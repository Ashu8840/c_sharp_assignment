using System;
using System.Collections.Generic;

namespace OnlineLearningPlatform
{
    // Delegate for Evaluation Strategy
    public delegate double EvaluationStrategy(double marks);

    // Event Arguments for Evaluation Completion
    public class EvaluationCompletedEventArgs : EventArgs
    {
        public string LearnerName { get; set; }
        public double Score { get; set; }
        public bool IsCertified { get; set; }
        public string CourseType { get; set; }
    }

    // 1. Learner Management (Encapsulation)
    public class Learner
    {
        private int learnerId;
        private string learnerName;
        private double totalMarks; // Private - accessed only through methods

        public Learner(int id, string name, double marks)
        {
            learnerId = id;
            learnerName = name;
            SetTotalMarks(marks);
        }

        // Getter for Learner ID
        public int GetLearnerId()
        {
            return learnerId;
        }

        // Getter for Learner Name
        public string GetLearnerName()
        {
            return learnerName;
        }

        // Getter for Total Marks
        public double GetTotalMarks()
        {
            return totalMarks;
        }

        // Setter for Total Marks with validation
        public void SetTotalMarks(double marks)
        {
            if (marks >= 0 && marks <= 100)
            {
                totalMarks = marks;
            }
            else
            {
                throw new ArgumentException("Marks must be between 0 and 100");
            }
        }

        public void DisplayLearnerInfo()
        {
            Console.WriteLine($"Learner ID: {learnerId}");
            Console.WriteLine($"Learner Name: {learnerName}");
            Console.WriteLine($"Total Marks: {totalMarks}");
        }
    }

    // 2. Course Types (Inheritance) - Base Class
    public abstract class Course
    {
        protected string courseName;
        protected string courseCode;
        protected Learner learner;
        protected EvaluationStrategy evaluationStrategy;

        // Event for Evaluation Completion
        public event EventHandler<EvaluationCompletedEventArgs> EvaluationCompleted;

        public Course(string name, string code, Learner learner)
        {
            this.courseName = name;
            this.courseCode = code;
            this.learner = learner;
        }

        // Set Evaluation Strategy using Delegate
        public void SetEvaluationStrategy(EvaluationStrategy strategy)
        {
            this.evaluationStrategy = strategy;
        }

        // Evaluate the learner
        public void EvaluateLearner()
        {
            if (evaluationStrategy == null)
            {
                Console.WriteLine("No evaluation strategy set!");
                return;
            }

            double score = evaluationStrategy(learner.GetTotalMarks());
            Console.WriteLine($"\n--- Evaluating {learner.GetLearnerName()} in {courseName} ---");
            Console.WriteLine($"Raw Marks: {learner.GetTotalMarks()}");
            Console.WriteLine($"Calculated Score: {score}%");

            bool isCertified = CheckCertification(score);

            // Raise Event
            OnEvaluationCompleted(new EvaluationCompletedEventArgs
            {
                LearnerName = learner.GetLearnerName(),
                Score = score,
                IsCertified = isCertified,
                CourseType = GetCourseType()
            });
        }

        // Method to raise the event
        protected virtual void OnEvaluationCompleted(EvaluationCompletedEventArgs e)
        {
            EvaluationCompleted?.Invoke(this, e);
        }

        // 4. Certification Rules (Polymorphism) - Abstract method
        public abstract bool CheckCertification(double score);

        // Abstract method to get course type
        public abstract string GetCourseType();

        public void DisplayCourseInfo()
        {
            Console.WriteLine($"\nCourse Name: {courseName}");
            Console.WriteLine($"Course Code: {courseCode}");
            Console.WriteLine($"Course Type: {GetCourseType()}");
        }
    }

    // Free Course (Inheritance)
    public class FreeCourse : Course
    {
        public FreeCourse(string name, string code, Learner learner)
            : base(name, code, learner)
        {
        }

        // Override: Certificate issued for score ≥ 50
        public override bool CheckCertification(double score)
        {
            return score >= 50;
        }

        public override string GetCourseType()
        {
            return "Free Course";
        }
    }

    // Paid Course (Inheritance)
    public class PaidCourse : Course
    {
        private double courseFee;

        public PaidCourse(string name, string code, Learner learner, double fee)
            : base(name, code, learner)
        {
            this.courseFee = fee;
        }

        // Override: Certificate issued for score ≥ 70
        public override bool CheckCertification(double score)
        {
            return score >= 70;
        }

        public override string GetCourseType()
        {
            return $"Paid Course (Fee: ${courseFee})";
        }
    }

    // 3. Evaluation Strategies (Delegates)
    public class EvaluationStrategies
    {
        // Quiz-based evaluation (70% pass threshold)
        public static double QuizBasedEvaluation(double marks)
        {
            return marks * 0.7; // 70% weightage
        }

        // Project-based evaluation (90% pass threshold)
        public static double ProjectBasedEvaluation(double marks)
        {
            return marks * 0.9; // 90% weightage
        }

        // Lambda expression for custom evaluation
        public static EvaluationStrategy CustomEvaluation = (marks) => marks * 0.8;
    }

    // 6. Notification Module
    public class NotificationModule
    {
        // Subscribe to evaluation completion event
        public void Subscribe(Course course)
        {
            course.EvaluationCompleted += OnEvaluationCompleted;
        }

        // Unsubscribe from evaluation completion event
        public void Unsubscribe(Course course)
        {
            course.EvaluationCompleted -= OnEvaluationCompleted;
        }

        // Event handler for evaluation completion
        private void OnEvaluationCompleted(object sender, EvaluationCompletedEventArgs e)
        {
            Console.WriteLine("\n=== NOTIFICATION SYSTEM ===");
            Console.WriteLine($"Dear {e.LearnerName},");
            Console.WriteLine($"Your evaluation in {e.CourseType} is complete.");
            Console.WriteLine($"Your Score: {e.Score:F2}%");

            if (e.IsCertified)
            {
                Console.WriteLine("✓ CONGRATULATIONS! Certificate has been issued.");
                Console.WriteLine("Your certificate is available in your dashboard.");
            }
            else
            {
                Console.WriteLine("✗ FAILED: You did not meet the certification requirements.");
                Console.WriteLine("Please retake the course to earn a certificate.");
            }
            Console.WriteLine("===========================\n");
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   ONLINE LEARNING PLATFORM - COURSE EVALUATION SYSTEM      ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            // Create Notification Module
            NotificationModule notificationModule = new NotificationModule();

            // ========== Scenario 1: Free Course with Quiz-based Evaluation ==========
            Console.WriteLine("\n********** SCENARIO 1: FREE COURSE - QUIZ-BASED **********");
            Learner learner1 = new Learner(101, "Alice Johnson", 75);
            learner1.DisplayLearnerInfo();

            FreeCourse freeCourse1 = new FreeCourse("Introduction to C#", "CS101", learner1);
            freeCourse1.DisplayCourseInfo();

            // Subscribe to notification
            notificationModule.Subscribe(freeCourse1);

            // Set Quiz-based evaluation strategy
            freeCourse1.SetEvaluationStrategy(EvaluationStrategies.QuizBasedEvaluation);
            freeCourse1.EvaluateLearner();

            // ========== Scenario 2: Paid Course with Project-based Evaluation ==========
            Console.WriteLine("\n\n********** SCENARIO 2: PAID COURSE - PROJECT-BASED **********");
            Learner learner2 = new Learner(102, "Bob Smith", 85);
            learner2.DisplayLearnerInfo();

            PaidCourse paidCourse1 = new PaidCourse("Advanced Data Structures", "CS201", learner2, 199.99);
            paidCourse1.DisplayCourseInfo();

            // Subscribe to notification
            notificationModule.Subscribe(paidCourse1);

            // Set Project-based evaluation strategy
            paidCourse1.SetEvaluationStrategy(EvaluationStrategies.ProjectBasedEvaluation);
            paidCourse1.EvaluateLearner();

            // ========== Scenario 3: Free Course - Student Fails ==========
            Console.WriteLine("\n\n********** SCENARIO 3: FREE COURSE - FAILED STUDENT **********");
            Learner learner3 = new Learner(103, "Charlie Davis", 45);
            learner3.DisplayLearnerInfo();

            FreeCourse freeCourse2 = new FreeCourse("Python Basics", "PY101", learner3);
            freeCourse2.DisplayCourseInfo();

            notificationModule.Subscribe(freeCourse2);
            freeCourse2.SetEvaluationStrategy(EvaluationStrategies.QuizBasedEvaluation);
            freeCourse2.EvaluateLearner();

            // ========== Scenario 4: Paid Course - Student Fails ==========
            Console.WriteLine("\n\n********** SCENARIO 4: PAID COURSE - FAILED STUDENT **********");
            Learner learner4 = new Learner(104, "Diana Martinez", 65);
            learner4.DisplayLearnerInfo();

            PaidCourse paidCourse2 = new PaidCourse("Machine Learning Specialization", "ML301", learner4, 299.99);
            paidCourse2.DisplayCourseInfo();

            notificationModule.Subscribe(paidCourse2);
            paidCourse2.SetEvaluationStrategy(EvaluationStrategies.ProjectBasedEvaluation);
            paidCourse2.EvaluateLearner();

            // ========== Scenario 5: Using Lambda Expression ==========
            Console.WriteLine("\n\n********** SCENARIO 5: CUSTOM EVALUATION (LAMBDA) **********");
            Learner learner5 = new Learner(105, "Eve Williams", 80);
            learner5.DisplayLearnerInfo();

            PaidCourse paidCourse3 = new PaidCourse("Web Development Bootcamp", "WEB101", learner5, 149.99);
            paidCourse3.DisplayCourseInfo();

            notificationModule.Subscribe(paidCourse3);

            // Using lambda expression for custom evaluation
            paidCourse3.SetEvaluationStrategy((marks) => marks * 0.85);
            paidCourse3.EvaluateLearner();

            // ========== Demonstrating Encapsulation ==========
            Console.WriteLine("\n\n********** DEMONSTRATING ENCAPSULATION **********");
            Console.WriteLine("Updating learner marks using setter method...");
            try
            {
                learner1.SetTotalMarks(90);
                Console.WriteLine($"Updated marks for {learner1.GetLearnerName()}: {learner1.GetTotalMarks()}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // ========== Summary ==========
            Console.WriteLine("\n\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    KEY CONCEPTS DEMONSTRATED               ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ ✓ Encapsulation: Private marks with getter/setter         ║");
            Console.WriteLine("║ ✓ Inheritance: FreeCourse & PaidCourse extend Course      ║");
            Console.WriteLine("║ ✓ Polymorphism: Different certification rules             ║");
            Console.WriteLine("║ ✓ Delegates: Dynamic evaluation strategies                ║");
            Console.WriteLine("║ ✓ Lambda Expressions: Custom evaluation logic             ║");
            Console.WriteLine("║ ✓ Events: Evaluation completion notifications             ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
