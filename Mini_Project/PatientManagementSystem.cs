using System;
using System.Collections.Generic;

namespace HospitalManagementSystem
{
    // Delegate for Billing Strategy
    public delegate double BillingStrategy(double baseCost);

    // Event Arguments for Patient Admission
    public class PatientAdmittedEventArgs : EventArgs
    {
        public string PatientName { get; set; }
        public string PatientType { get; set; }
        public DateTime AdmissionTime { get; set; }
    }

    // Event Arguments for Bill Generation
    public class BillGeneratedEventArgs : EventArgs
    {
        public string PatientName { get; set; }
        public int PatientId { get; set; }
        public double BaseCost { get; set; }
        public double FinalBill { get; set; }
        public string BillingType { get; set; }
        public DateTime BillingTime { get; set; }
    }

    // 1. Patient Class (Encapsulation)
    public class Patient
    {
        private int patientId;
        private string patientName;
        private int age;
        private string disease;
        private DateTime admissionDate;

        public Patient(int id, string name, int age, string disease)
        {
            this.patientId = id;
            this.patientName = name;
            this.age = age;
            this.disease = disease;
            this.admissionDate = DateTime.Now;
        }

        // Getters
        public int GetPatientId() => patientId;
        public string GetPatientName() => patientName;
        public int GetAge() => age;
        public string GetDisease() => disease;
        public DateTime GetAdmissionDate() => admissionDate;

        // Setters with validation
        public void SetAge(int newAge)
        {
            if (newAge > 0 && newAge < 150)
                age = newAge;
            else
                throw new ArgumentException("Invalid age");
        }

        public void SetDisease(string newDisease)
        {
            if (!string.IsNullOrWhiteSpace(newDisease))
                disease = newDisease;
        }

        public void DisplayPatientInfo()
        {
            Console.WriteLine($"├─ Patient ID: {patientId}");
            Console.WriteLine($"├─ Name: {patientName}");
            Console.WriteLine($"├─ Age: {age}");
            Console.WriteLine($"├─ Disease: {disease}");
            Console.WriteLine($"└─ Admission Date: {admissionDate:dd-MMM-yyyy HH:mm}");
        }
    }

    // 2. Patient Type Base Class (Inheritance)
    public abstract class PatientType
    {
        protected Patient patient;
        protected double baseTreatmentCost;
        protected BillingStrategy billingStrategy;

        // Events
        public event EventHandler<PatientAdmittedEventArgs> PatientAdmitted;
        public event EventHandler<BillGeneratedEventArgs> BillGenerated;

        public PatientType(Patient patient, double baseCost)
        {
            this.patient = patient;
            this.baseTreatmentCost = baseCost;
        }

        // Set billing strategy dynamically
        public void SetBillingStrategy(BillingStrategy strategy)
        {
            this.billingStrategy = strategy;
        }

        // Admit patient and raise event
        public void AdmitPatient()
        {
            Console.WriteLine($"\n{'═',60}");
            Console.WriteLine($"  ADMITTING PATIENT: {patient.GetPatientName()}");
            Console.WriteLine($"{'═',60}");
            patient.DisplayPatientInfo();
            Console.WriteLine($"  Patient Type: {GetPatientTypeName()}");
            Console.WriteLine($"{'═',60}\n");

            // Raise admission event
            OnPatientAdmitted(new PatientAdmittedEventArgs
            {
                PatientName = patient.GetPatientName(),
                PatientType = GetPatientTypeName(),
                AdmissionTime = DateTime.Now
            });
        }

        // Generate bill using billing strategy
        public void GenerateBill(string billingType)
        {
            if (billingStrategy == null)
            {
                Console.WriteLine(" No billing strategy set!");
                return;
            }

            double finalBill = billingStrategy(baseTreatmentCost);

            Console.WriteLine($"\n┌{'─',58}┐");
            Console.WriteLine($"│{"BILL SUMMARY",40}                 │");
            Console.WriteLine($"├{'─',58}┤");
            Console.WriteLine($"│ Patient ID       : {patient.GetPatientId(),-37} │");
            Console.WriteLine($"│ Patient Name     : {patient.GetPatientName(),-37} │");
            Console.WriteLine($"│ Patient Type     : {GetPatientTypeName(),-37} │");
            Console.WriteLine($"│ Disease          : {patient.GetDisease(),-37} │");
            Console.WriteLine($"│ Base Cost        : ₹{baseTreatmentCost,-36:F2} │");
            Console.WriteLine($"│ Billing Type     : {billingType,-37} │");
            Console.WriteLine($"│ Final Bill       : ₹{finalBill,-36:F2} │");
            Console.WriteLine($"│ Discount/Charge  : {GetDiscountInfo(baseTreatmentCost, finalBill),-37} │");
            Console.WriteLine($"└{'─',58}┘\n");

            // Raise bill generation event
            OnBillGenerated(new BillGeneratedEventArgs
            {
                PatientName = patient.GetPatientName(),
                PatientId = patient.GetPatientId(),
                BaseCost = baseTreatmentCost,
                FinalBill = finalBill,
                BillingType = billingType,
                BillingTime = DateTime.Now
            });
        }

        private string GetDiscountInfo(double baseCost, double finalBill)
        {
            double diff = finalBill - baseCost;
            if (diff < 0)
                return $"-₹{Math.Abs(diff):F2} (Discount)";
            else if (diff > 0)
                return $"+₹{diff:F2} (Additional)";
            else
                return "No Change";
        }

        // Event raising methods
        protected virtual void OnPatientAdmitted(PatientAdmittedEventArgs e)
        {
            PatientAdmitted?.Invoke(this, e);
        }

        protected virtual void OnBillGenerated(BillGeneratedEventArgs e)
        {
            BillGenerated?.Invoke(this, e);
        }

        // Abstract methods (Polymorphism)
        public abstract string GetPatientTypeName();
        public abstract void DisplayTreatmentDetails();
    }

    // 3. General Patient (Inheritance)
    public class GeneralPatient : PatientType
    {
        public GeneralPatient(Patient patient, double baseCost) : base(patient, baseCost) { }

        public override string GetPatientTypeName()
        {
            return "General Ward";
        }

        public override void DisplayTreatmentDetails()
        {
            Console.WriteLine("Treatment: General Ward - Basic medical care");
        }
    }

    // 4. Emergency Patient (Inheritance)
    public class EmergencyPatient : PatientType
    {
        public EmergencyPatient(Patient patient, double baseCost) : base(patient, baseCost) { }

        public override string GetPatientTypeName()
        {
            return "Emergency";
        }

        public override void DisplayTreatmentDetails()
        {
            Console.WriteLine("Treatment: Emergency - Immediate critical care");
        }
    }

    // 5. ICU Patient (Inheritance)
    public class ICUPatient : PatientType
    {
        public ICUPatient(Patient patient, double baseCost) : base(patient, baseCost) { }

        public override string GetPatientTypeName()
        {
            return "ICU - Intensive Care";
        }

        public override void DisplayTreatmentDetails()
        {
            Console.WriteLine("Treatment: ICU - 24/7 intensive monitoring");
        }
    }

    // 6. Billing Strategies (Delegates)
    public class BillingStrategies
    {
        // Standard billing - no change
        public static double StandardBilling(double baseCost)
        {
            return baseCost;
        }

        // Insurance billing - 40% discount
        public static double InsuranceBilling(double baseCost)
        {
            return baseCost * 0.60; // 40% discount
        }

        // Emergency billing - 50% extra charge
        public static double EmergencyBilling(double baseCost)
        {
            return baseCost * 1.50; // 50% extra
        }

        // Senior citizen - 30% discount
        public static double SeniorCitizenBilling(double baseCost)
        {
            return baseCost * 0.70; // 30% discount
        }

        // VIP billing - 20% extra for premium services
        public static BillingStrategy VIPBilling = (baseCost) => baseCost * 1.20;
    }

    // 7. Hospital Notification System (Event Handlers)
    public class HospitalNotificationSystem
    {
        private string departmentName;

        public HospitalNotificationSystem(string department)
        {
            this.departmentName = department;
        }

        // Subscribe to patient admission events
        public void SubscribeToAdmission(PatientType patientType)
        {
            patientType.PatientAdmitted += OnPatientAdmitted;
        }

        // Subscribe to bill generation events
        public void SubscribeToBilling(PatientType patientType)
        {
            patientType.BillGenerated += OnBillGenerated;
        }

        // Event handler for patient admission
        private void OnPatientAdmitted(object sender, PatientAdmittedEventArgs e)
        {
            Console.WriteLine($" [{departmentName}] NOTIFICATION:");
            Console.WriteLine($"   New patient admitted: {e.PatientName}");
            Console.WriteLine($"   Type: {e.PatientType}");
            Console.WriteLine($"   Time: {e.AdmissionTime:HH:mm:ss}");
            Console.WriteLine($"   Action: Prepare necessary resources\n");
        }

        // Event handler for bill generation
        private void OnBillGenerated(object sender, BillGeneratedEventArgs e)
        {
            Console.WriteLine($" [{departmentName}] BILLING NOTIFICATION:");
            Console.WriteLine($"   Patient: {e.PatientName} (ID: {e.PatientId})");
            Console.WriteLine($"   Billing Type: {e.BillingType}");
            Console.WriteLine($"   Amount: ₹{e.FinalBill:F2}");
            Console.WriteLine($"   Time: {e.BillingTime:HH:mm:ss}");
            Console.WriteLine($"   Status: Bill generated successfully\n");
        }
    }

    // 8. Hospital Management System
    class HospitalSystem
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        HOSPITAL PATIENT MANAGEMENT SYSTEM v1.0             ║");
            Console.WriteLine("║            Console-Based Healthcare Solution               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            // Create notification departments
            HospitalNotificationSystem admissionDept = new HospitalNotificationSystem("Admission Department");
            HospitalNotificationSystem billingDept = new HospitalNotificationSystem("Billing Department");
            HospitalNotificationSystem emergencyDept = new HospitalNotificationSystem("Emergency Department");
            HospitalNotificationSystem icuDept = new HospitalNotificationSystem("ICU Department");

            bool continueSystem = true;

            while (continueSystem)
            {
                try
                {
                    Console.WriteLine("\n" + new string('═', 62));
                    Console.WriteLine("            PATIENT ADMISSION AND BILLING SYSTEM");
                    Console.WriteLine(new string('═', 62));

                    // Patient Details
                    Console.WriteLine("\n📋 PATIENT INFORMATION");
                    Console.WriteLine(new string('─', 62));

                    Console.Write("Patient ID: ");
                    int patientId = int.Parse(Console.ReadLine());

                    Console.Write("Patient Name: ");
                    string patientName = Console.ReadLine();

                    Console.Write("Age: ");
                    int patientAge = int.Parse(Console.ReadLine());

                    Console.Write("Disease/Condition: ");
                    string disease = Console.ReadLine();

                    // Create patient object
                    Patient patient = new Patient(patientId, patientName, patientAge, disease);

                    // Select Patient Type
                    Console.WriteLine("\n PATIENT TYPE SELECTION");
                    Console.WriteLine(new string('─', 62));
                    Console.WriteLine("1. General Ward (Basic medical care)");
                    Console.WriteLine("2. Emergency (Critical care - 50% surcharge)");
                    Console.WriteLine("3. ICU (Intensive care - 24/7 monitoring)");
                    Console.Write("\nSelect Patient Type (1-3): ");
                    int patientTypeChoice = int.Parse(Console.ReadLine());

                    // Enter Treatment Cost
                    Console.WriteLine("\n TREATMENT COST");
                    Console.WriteLine(new string('─', 62));
                    Console.Write("Base Treatment Cost (₹): ");
                    double baseCost = double.Parse(Console.ReadLine());

                    // Create patient type object based on selection
                    PatientType patientType = null;

                    switch (patientTypeChoice)
                    {
                        case 1:
                            patientType = new GeneralPatient(patient, baseCost);
                            admissionDept.SubscribeToAdmission(patientType);
                            break;
                        case 2:
                            patientType = new EmergencyPatient(patient, baseCost);
                            emergencyDept.SubscribeToAdmission(patientType);
                            break;
                        case 3:
                            patientType = new ICUPatient(patient, baseCost);
                            icuDept.SubscribeToAdmission(patientType);
                            break;
                        default:
                            Console.WriteLine(" Invalid patient type selection!");
                            continue;
                    }

                    billingDept.SubscribeToBilling(patientType);

                    // Admit Patient
                    patientType.AdmitPatient();

                    // Select Billing Strategy
                    Console.WriteLine("\n BILLING STRATEGY");
                    Console.WriteLine(new string('─', 62));
                    Console.WriteLine("1. Standard Billing (No discount/surcharge)");
                    Console.WriteLine("2. Insurance Coverage (40% discount)");
                    Console.WriteLine("3. Emergency Billing (50% surcharge)");
                    Console.WriteLine("4. Senior Citizen (30% discount - Age 60+)");
                    Console.WriteLine("5. VIP Package (20% premium for exclusive services)");
                    Console.Write("\nSelect Billing Type (1-5): ");
                    int billingChoice = int.Parse(Console.ReadLine());

                    // Apply billing strategy
                    string billingDescription = "";
                    switch (billingChoice)
                    {
                        case 1:
                            patientType.SetBillingStrategy(BillingStrategies.StandardBilling);
                            billingDescription = "Standard Billing (No discount)";
                            break;
                        case 2:
                            patientType.SetBillingStrategy(BillingStrategies.InsuranceBilling);
                            billingDescription = "Insurance Coverage (40% discount)";
                            break;
                        case 3:
                            patientType.SetBillingStrategy(BillingStrategies.EmergencyBilling);
                            billingDescription = "Emergency Billing (50% surcharge)";
                            break;
                        case 4:
                            if (patientAge >= 60)
                            {
                                patientType.SetBillingStrategy(BillingStrategies.SeniorCitizenBilling);
                                billingDescription = "Senior Citizen (30% discount)";
                            }
                            else
                            {
                                Console.WriteLine("\n⚠ Patient age is below 60. Applying standard billing instead.");
                                patientType.SetBillingStrategy(BillingStrategies.StandardBilling);
                                billingDescription = "Standard Billing (Age < 60)";
                            }
                            break;
                        case 5:
                            patientType.SetBillingStrategy(BillingStrategies.VIPBilling);
                            billingDescription = "VIP Package (20% premium)";
                            break;
                        default:
                            Console.WriteLine(" Invalid billing choice! Applying standard billing.");
                            patientType.SetBillingStrategy(BillingStrategies.StandardBilling);
                            billingDescription = "Standard Billing (Default)";
                            break;
                    }

                    // Generate Bill
                    patientType.GenerateBill(billingDescription);

                    Console.WriteLine("\n✓ Patient processing completed successfully!");

                }
                catch (FormatException)
                {
                    Console.WriteLine("\n ERROR: Invalid input format! Please enter valid data.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\n ERROR: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n UNEXPECTED ERROR: {ex.Message}");
                }

                // Ask to continue or exit
                Console.WriteLine("\n" + new string('═', 62));
                Console.Write("Do you want to admit another patient? (Y/N): ");
                string response = Console.ReadLine()?.ToUpper();
                
                if (response != "Y" && response != "YES")
                {
                    continueSystem = false;
                }
            }

            // System Summary
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   SYSTEM FEATURES DEMO                      ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ ✓ Encapsulation: Private patient data with getters        ║");
            Console.WriteLine("║ ✓ Inheritance: GeneralPatient, EmergencyPatient, ICU      ║");
            Console.WriteLine("║ ✓ Polymorphism: Different patient type implementations    ║");
            Console.WriteLine("║ ✓ Delegates: Dynamic billing strategies                   ║");
            Console.WriteLine("║ ✓ Events: Real-time department notifications              ║");
            Console.WriteLine("║ ✓ Lambda: VIP billing using lambda expressions            ║");
            Console.WriteLine("║ ✓ Event-Driven: Pub-Sub pattern for notifications         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

            Console.WriteLine("\n✓ Thank you for using Hospital Patient Management System!");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
