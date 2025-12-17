# Hospital Patient Management System - Mini Project

## 🏥 Overview

A comprehensive console-based **Patient Management System** designed for hospitals to manage different patient types, implement dynamic billing strategies, and enable real-time department notifications using delegates and events. The system strictly follows object-oriented principles and event-driven programming architecture.

---

## 🎯 System Objectives

The Hospital Patient Management System streamlines the entire patient workflow from admission to billing:

1. **Admit Patient** - Register patient with personal and medical details
2. **Select Patient Type** - Categorize as General, Emergency, or ICU patient
3. **Calculate Treatment Bill** - Compute base treatment costs
4. **Apply Billing Strategy** - Dynamically apply discounts/charges using delegates
5. **Generate Bill** - Create detailed billing summary
6. **Trigger Events** - Notify relevant hospital departments in real-time

---

## 🔧 Technical Implementation

### 1. **Encapsulation - Patient Data Protection**

The `Patient` class demonstrates proper encapsulation with all sensitive data kept private:

- **Private Fields**: Patient ID, name, age, disease, admission date
- **Controlled Access**: Data accessible only through getter and setter methods
- **Validation**: Setters include validation logic (e.g., age must be 0-150)
- **Data Integrity**: Prevents unauthorized direct access to patient records

### 2. **Inheritance - Patient Type Hierarchy**

Implemented a three-level patient classification system:

- **Base Class**: `PatientType` (abstract) - Contains common functionality
- **Derived Classes**:
  - `GeneralPatient` - Basic ward patients with standard care
  - `EmergencyPatient` - Critical care patients requiring immediate attention
  - `ICUPatient` - Intensive care patients with 24/7 monitoring

Each patient type inherits common behavior but provides specialized treatment details.

### 3. **Polymorphism - Flexible Patient Handling**

Method overriding enables different behavior for each patient type:

- **Abstract Methods**: `GetPatientTypeName()`, `DisplayTreatmentDetails()`
- **Runtime Behavior**: Different implementations based on actual patient type
- **Extensibility**: Easy to add new patient types without modifying existing code

### 4. **Delegates - Dynamic Billing Strategies**

`BillingStrategy` delegate enables runtime selection of billing logic:

**Available Strategies:**

- **Standard Billing** - No modification (100% of base cost)
- **Insurance Billing** - 40% discount for insured patients
- **Emergency Billing** - 50% surcharge for critical care
- **Senior Citizen Billing** - 30% discount for elderly patients (60+)
- **VIP Billing** - 20% premium for exclusive services (using lambda)

The system can switch between billing strategies dynamically based on patient eligibility and preferences.

### 5. **Events - Real-Time Notification System**

Two custom events drive the notification system:

**Event 1: Patient Admitted**

- Triggered when a patient is admitted
- Notifies: Admission Department, specific ward departments
- Information: Patient name, type, admission time

**Event 2: Bill Generated**

- Triggered after bill calculation
- Notifies: Billing Department, Accounts Department
- Information: Patient details, billing type, final amount

### 6. **Event-Driven Architecture - Publisher-Subscriber Pattern**

`HospitalNotificationSystem` implements the observer pattern:

- **Multiple Departments**: Admission, Billing, Emergency, ICU departments
- **Subscribe/Unsubscribe**: Departments subscribe to relevant events
- **Decoupled Design**: Event publishers don't know about subscribers
- **Scalability**: Easy to add new departments without changing core logic

### 7. **Lambda Expressions - Functional Programming**

VIP billing strategy demonstrates lambda syntax:

```csharp
BillingStrategy VIPBilling = (baseCost) => baseCost * 1.20;
```

This provides concise, inline function definitions for delegates.

---

## 📊 System Scenarios Demonstrated

### Scenario 1: General Patient with Insurance

- **Patient**: John Smith, 45 years old
- **Condition**: Fever & Cold
- **Base Cost**: $5,000
- **Billing**: Insurance (40% discount)
- **Final Bill**: $3,000
- **Notifications**: Admission Dept, Billing Dept

### Scenario 2: Emergency Patient - Critical Care

- **Patient**: Sarah Johnson, 32 years old
- **Condition**: Accident with multiple injuries
- **Base Cost**: $8,000
- **Billing**: Emergency (50% surcharge)
- **Final Bill**: $12,000
- **Notifications**: Emergency Dept, Billing Dept

### Scenario 3: ICU Patient - Senior Citizen

- **Patient**: Robert Williams, 72 years old
- **Condition**: Heart Failure
- **Base Cost**: $15,000
- **Billing**: Senior Citizen (30% discount)
- **Final Bill**: $10,500
- **Notifications**: ICU Dept, Billing Dept

### Scenario 4: VIP Patient with Premium Services

- **Patient**: Emma Davis, 38 years old
- **Condition**: Routine Checkup
- **Base Cost**: $3,000
- **Billing**: VIP Package (20% premium) - Lambda Expression
- **Final Bill**: $3,600
- **Notifications**: Admission Dept, Billing Dept

### Scenario 5: Standard Billing

- **Patient**: Michael Brown, 28 years old
- **Condition**: Viral Infection
- **Base Cost**: $4,000
- **Billing**: Standard (No discount)
- **Final Bill**: $4,000
- **Notifications**: Admission Dept, Billing Dept

---

## 🎨 Key Features

✅ **Object-Oriented Design** - All four OOP pillars implemented  
✅ **Type Safety** - Strong typing with custom event arguments  
✅ **Extensibility** - Easy to add new patient types and billing strategies  
✅ **Event-Driven** - Asynchronous notification system  
✅ **Validation** - Input validation for patient data  
✅ **Beautiful UI** - Well-formatted console output with Unicode characters  
✅ **Real-Time Updates** - Instant notifications to all subscribed departments  
✅ **Flexible Billing** - Multiple billing strategies using delegates

---

## 🏗️ Architecture Highlights

### Design Patterns Used:

1. **Strategy Pattern** - Billing strategies via delegates
2. **Observer Pattern** - Event-driven notifications
3. **Template Method Pattern** - Abstract patient type class
4. **Factory Method Pattern** - Patient type creation

### SOLID Principles:

- **S**ingle Responsibility - Each class has one clear purpose
- **O**pen/Closed - Open for extension (new patient types), closed for modification
- **L**iskov Substitution - Derived patient types can substitute base type
- **I**nterface Segregation - Focused, specific event arguments
- **D**ependency Inversion - Depends on abstractions (delegates, events)

---

## 🚀 How to Run

```bash
cd Mini_Project
csc PatientManagementSystem.cs
.\PatientManagementSystem.exe
```

---

## 📈 Future Enhancements

- Add database integration for patient records persistence
- Implement appointment scheduling system
- Add doctor assignment and management
- Create web-based dashboard
- Integrate SMS/Email notification system
- Add payment gateway integration
- Generate PDF bills and medical reports
- Implement bed availability tracking
- Add medical inventory management

---

## 🎓 Learning Outcomes

This mini project demonstrates mastery of:

- **Advanced C# Features**: Delegates, events, lambda expressions
- **OOP Principles**: Encapsulation, inheritance, polymorphism, abstraction
- **Design Patterns**: Strategy, Observer, Template Method
- **Event-Driven Programming**: Publisher-subscriber architecture
- **Clean Code**: Well-structured, maintainable, and extensible code
- **Real-World Application**: Practical hospital management scenario

---

## 👨‍💻 Technical Stack

- **Language**: C# (.NET Framework)
- **Platform**: Console Application
- **Architecture**: Event-Driven, Object-Oriented
- **Patterns**: Strategy, Observer, Template Method
- **Concepts**: Delegates, Events, Lambda Expressions, Generics

---

## 📝 Conclusion

The Hospital Patient Management System showcases how modern C# features like delegates, events, and lambda expressions can be combined with solid object-oriented design to create a flexible, maintainable, and extensible software system. The event-driven architecture ensures real-time communication between different hospital departments, while the delegate-based billing strategy provides maximum flexibility for various patient scenarios.

This project serves as an excellent example of applying theoretical programming concepts to solve real-world healthcare management challenges.
