# 🎯 C# Programming Assignments Repository

Welcome to my comprehensive C# programming assignments repository! This collection showcases advanced C# concepts including **Object-Oriented Programming**, **Delegates**, **Lambda Expressions**, **Events**, and **Real-World Application Development**.

---

## 📚 Repository Structure

```
c_sharp_assignment/
├── Day_6/          # Delegates & Strategy Pattern
├── Day_7/          # Delegates, Lambda Expressions & Events
└── Mini_Project/   # Hospital Management System (Comprehensive Project)
```

---

## 📅 Day 6 - C# Delegates & Strategy Pattern

### 🎯 Learning Objectives

Master the concept of delegates in C# and their practical application in implementing flexible, strategy-based systems.

### 📦 Assignment 1: Smart Online Shopping Discount System

**Scenario**: E-commerce platform with dynamic discount strategies

**Key Components:**

- **Product Class**: Encapsulates product details (ID, name, price)
- **Customer Class**: Manages customer information and type (Occasional/Regular)
- **Discount Strategies**: Delegate-based discount calculation
  - 🎉 **Festival Discount**: 17% off for occasional customers
  - ⭐ **Premium Discount**: 28% off for regular customers
- **Shopping Class**: Orchestrates discount application and final price calculation

**Technical Highlights:**

- Delegate usage for dynamic strategy selection
- Encapsulation of business logic
- Runtime behavior modification
- Clean separation of concerns

### 💳 Assignment 2: Smart Payment Gateway System

**Scenario**: Multi-payment processing system for online transactions

**Key Components:**

- **Customer Class**: Manages customer payment preferences
- **Payment Processor Delegates**: Handle different payment methods
  - 💳 **Credit Card Processing**: Card validation and authorization
  - 📱 **UPI Processing**: Instant mobile payment handling
  - 🏦 **Net Banking**: Secure bank transfer processing
- **Payment Gateway**: Dynamic payment method selection

**Technical Highlights:**

- Delegate-based payment strategy pattern
- Runtime payment method switching
- Flexible architecture for adding new payment methods
- Type-safe payment processing

**Key Takeaway**: Both assignments demonstrate how delegates enable flexible, extensible code that can adapt to changing requirements without modification of core logic.

---

## 📅 Day 7 - Delegates, Lambda Expressions & Events

### 🎯 Learning Objectives

Advanced C# features including delegates, lambda expressions, and event-driven programming for building scalable EdTech systems.

### 🎓 Project: Online Learning Platform - Course Evaluation & Certification System

**Scenario**: EdTech platform (similar to Coursera/Udemy) with automated course evaluation and certification

### 🔑 Core Features Implemented

#### 1️⃣ **Learner Management (Encapsulation)**

- Private fields for sensitive data (ID, name, marks)
- Getter/setter methods with validation
- Marks restricted to 0-100 range
- Data integrity and security

#### 2️⃣ **Course Types (Inheritance)**

- **Base Class**: `Course` (abstract) - Common course functionality
- **Free Course**: Certificate threshold ≥ 50%
- **Paid Course**: Certificate threshold ≥ 70%, includes course fees
- Shared behavior with specialized implementations

#### 3️⃣ **Evaluation Strategies (Delegates)**

- **Quiz-Based Evaluation**: 70% weightage on raw marks
- **Project-Based Evaluation**: 90% weightage on raw marks
- **Custom Evaluation**: Lambda expression-based strategies
- Runtime strategy selection

#### 4️⃣ **Certification Rules (Polymorphism)**

- Method overriding for different course types
- Dynamic certification threshold checking
- Type-specific certification logic

#### 5️⃣ **Event-Driven Notifications (Events)**

- **EvaluationCompleted Event**: Triggered after evaluation
- **Custom EventArgs**: Carries learner data, score, certification status
- Asynchronous notification system

#### 6️⃣ **Notification Module (Observer Pattern)**

- Subscribes to evaluation events
- Sends success/failure notifications
- Displays certificate information
- Suggests retry for failed students

#### 7️⃣ **Lambda Expressions**

- Concise evaluation strategy definitions
- Inline delegate implementations
- Functional programming approach

### 📊 Demonstrated Scenarios

✅ Free course with quiz evaluation - Student passes  
✅ Paid course with project evaluation - Student passes  
✅ Free course - Student fails (score < 50%)  
✅ Paid course - Student fails (score < 70%)  
✅ Custom evaluation using lambda expressions

### 🎨 Technical Excellence

- **All OOP Pillars**: Encapsulation, Inheritance, Polymorphism, Abstraction
- **Event-Driven Architecture**: Decoupled notification system
- **SOLID Principles**: Maintainable and extensible design
- **Delegate Pattern**: Flexible strategy implementation
- **Lambda Expressions**: Modern C# syntax

---

## 🏥 Mini Project - Hospital Patient Management System

### 🎯 Project Overview

A **comprehensive console-based Patient Management System** designed for hospitals to manage different patient types, implement dynamic billing strategies, and enable real-time department notifications using advanced C# features.

### 🔄 System Workflow

```
1. Admit Patient → 2. Select Patient Type → 3. Calculate Treatment Bill
     ↓                     ↓                           ↓
4. Apply Billing Strategy → 5. Generate Bill → 6. Trigger Events & Notify
```

### 🏗️ System Architecture

#### **Patient Types (Inheritance Hierarchy)**

```
        PatientType (Abstract Base)
               ├── GeneralPatient (Basic ward care)
               ├── EmergencyPatient (Critical care)
               └── ICUPatient (Intensive monitoring)
```

#### **Billing Strategies (Delegates)**

```
BillingStrategy Delegate
    ├── Standard Billing (100% base cost)
    ├── Insurance Billing (40% discount)
    ├── Emergency Billing (50% surcharge)
    ├── Senior Citizen (30% discount)
    └── VIP Billing (20% premium) - Lambda
```

### 🔔 Real-Time Notification System

**Event 1: PatientAdmitted**

- Notifies relevant departments
- Provides patient details and admission time
- Triggers resource preparation

**Event 2: BillGenerated**

- Notifies billing and accounts departments
- Includes billing details and final amount
- Confirms successful bill generation

### 🎭 Live Scenarios

| Scenario | Patient Type | Condition         | Base Cost | Billing Type             | Final Bill |
| -------- | ------------ | ----------------- | --------- | ------------------------ | ---------- |
| 1        | General      | Fever & Cold      | $5,000    | Insurance (40% off)      | $3,000     |
| 2        | Emergency    | Multiple Injuries | $8,000    | Emergency (+50%)         | $12,000    |
| 3        | ICU          | Heart Failure     | $15,000   | Senior Citizen (30% off) | $10,500    |
| 4        | General      | Routine Checkup   | $3,000    | VIP (+20%)               | $3,600     |
| 5        | General      | Viral Infection   | $4,000    | Standard                 | $4,000     |

### 🎯 Key Features

✨ **Object-Oriented Design** - All four OOP pillars implemented  
✨ **Encapsulation** - Private patient data with controlled access  
✨ **Inheritance** - Three-level patient type hierarchy  
✨ **Polymorphism** - Method overriding for patient-specific behavior  
✨ **Delegates** - Dynamic billing strategies  
✨ **Events** - Real-time department notifications  
✨ **Lambda Expressions** - Concise VIP billing logic  
✨ **Event-Driven Architecture** - Publisher-subscriber pattern  
✨ **Beautiful Console UI** - Unicode characters and formatting

### 🏆 Design Patterns Implemented

- **Strategy Pattern** - Billing strategies via delegates
- **Observer Pattern** - Event-driven notifications
- **Template Method Pattern** - Abstract patient type class
- **Factory Method Pattern** - Patient creation

### 💡 SOLID Principles

✅ **Single Responsibility** - Each class has one clear purpose  
✅ **Open/Closed** - Open for extension, closed for modification  
✅ **Liskov Substitution** - Derived types can substitute base type  
✅ **Interface Segregation** - Focused event arguments  
✅ **Dependency Inversion** - Depends on abstractions

---

## 🚀 How to Run Projects

### Day 6 Assignments

```bash
cd Day_6
csc Assignment1.cs
.\Assignment1.exe

csc Assignment2.cs
.\Assignment2.exe
```

### Day 7 Assignment

```bash
cd Day_7
csc CourseEvaluationSystem.cs
.\CourseEvaluationSystem.exe
```

### Mini Project

```bash
cd Mini_Project
csc PatientManagementSystem.cs
.\PatientManagementSystem.exe
```

---

## 🎓 Learning Outcomes

This repository demonstrates mastery of:

### Core C# Concepts

✅ Object-Oriented Programming (Encapsulation, Inheritance, Polymorphism, Abstraction)  
✅ Delegates and Multicast Delegates  
✅ Lambda Expressions and Anonymous Functions  
✅ Events and Event Handling  
✅ Custom EventArgs

### Advanced Concepts

✅ Design Patterns (Strategy, Observer, Template Method, Factory)  
✅ SOLID Principles  
✅ Event-Driven Architecture  
✅ Publisher-Subscriber Pattern  
✅ Type Safety and Generics

### Software Engineering

✅ Clean Code Practices  
✅ Code Reusability and Maintainability  
✅ Extensibility and Scalability  
✅ Real-World Problem Solving  
✅ System Design and Architecture

---

## 📈 Project Progression

```
Day 6 (Foundation)          Day 7 (Advanced)              Mini Project (Mastery)
      ↓                            ↓                              ↓
  Delegates              Delegates + Lambda            Full System Integration
     +                         +                              +
Strategy Pattern            Events                 All Concepts Combined
                              +                              +
                      Event-Driven Design          Real-World Application
```

---

## 🎯 Technical Stack

**Language**: C# (.NET Framework)  
**Paradigms**: Object-Oriented, Event-Driven, Functional  
**Patterns**: Strategy, Observer, Template Method, Factory  
**Features**: Delegates, Events, Lambda, Generics, LINQ-ready

---

## 📝 Conclusion

This repository represents a comprehensive journey through advanced C# programming concepts. Starting from basic delegate usage in Day 6, progressing to lambda expressions and events in Day 7, and culminating in a full-featured hospital management system in the mini project. Each assignment builds upon previous concepts while introducing new techniques, demonstrating how these features can be combined to create robust, maintainable, and extensible software systems.

The progression from simple discount calculations to complex event-driven hospital management showcases the power and flexibility of C# as a modern, enterprise-grade programming language.

---

## 📧 Contact & Feedback

Feel free to explore the code, suggest improvements, or use these projects as learning resources!

**Happy Coding! 🚀**

# Day 7 Assignment - Delegates, Lambda Expressions, and Events

Completed a comprehensive assignment on delegates, lambda expressions, and events for an Online Learning Platform (EdTech) Course Evaluation and Certification System.

## Overview

The assignment implements a flexible, extensible, and event-driven course evaluation system similar to platforms like Coursera or Udemy. The system evaluates learners enrolled in different types of courses and issues certificates based on their performance.

## Key Concepts Implemented

### 1. Learner Management (Encapsulation)

Created a Learner class with proper encapsulation where learner ID, learner name, and total marks are managed. The marks field is kept private and can only be accessed through getter and setter methods. The setter includes validation to ensure marks stay within the 0 to 100 range, demonstrating proper data protection and controlled access.

### 2. Course Types (Inheritance)

Implemented a base Course class that is inherited by two derived classes: FreeCourse and PaidCourse. Both course types share common functionality from the base class but have their own specific implementations. The PaidCourse additionally tracks the course fee. This demonstrates the inheritance hierarchy and code reusability.

### 3. Evaluation Strategy (Delegates)

Used delegates to implement dynamic evaluation strategies. Created an EvaluationStrategy delegate that represents different evaluation logic. Implemented two evaluation methods: Quiz-based evaluation that applies 70 percent weightage to marks and Project-based evaluation that applies 90 percent weightage. The evaluation strategy can be changed at runtime, showing the flexibility of delegates.

### 4. Certification Rules (Polymorphism)

Applied method overriding to implement different certification thresholds for free and paid courses. Free courses issue certificates for scores of 50 or above, while paid courses require a higher threshold of 70 or above. The CheckCertification method is abstract in the base class and overridden in derived classes, demonstrating polymorphism.

### 5. Event-Driven Notification (Events)

Implemented an event-driven architecture where an EvaluationCompleted event is raised after evaluation finishes. The event carries information about the learner, their score, certification status, and course type through custom EventArgs. This decouples the evaluation logic from the notification logic.

### 6. Notification Module

Created a separate NotificationModule class that subscribes to the EvaluationCompleted event. When the event is raised, the notification module automatically sends appropriate messages to learners. It displays congratulatory messages with certificate information for passing students and failure notifications with retry suggestions for students who didn't meet the requirements.

### 7. Lambda Expressions

Demonstrated the use of lambda expressions for creating concise evaluation strategies. Implemented custom evaluation logic using lambda syntax, showing how anonymous functions can be used with delegates for cleaner and more readable code.

## System Features

The system handles multiple scenarios including:

- Free course with quiz-based evaluation where students pass
- Paid course with project-based evaluation where students pass
- Free course scenarios where students fail to meet the 50 percent threshold
- Paid course scenarios where students fail to meet the 70 percent threshold
- Custom evaluation strategies using lambda expressions
- Dynamic switching between different evaluation methods
- Automatic event-driven notifications for all evaluation outcomes

## Technical Highlights

The assignment showcases all four pillars of object-oriented programming: encapsulation through private fields and methods, inheritance through course hierarchy, polymorphism through method overriding, and abstraction through the base course class. Additionally, it demonstrates advanced C# features like delegates for strategy pattern implementation, events for publisher-subscriber pattern, and lambda expressions for functional programming style.

The notification system is completely decoupled from the evaluation system through events, making it easy to add more notification channels like email or SMS in the future without modifying the core evaluation logic. The delegate-based evaluation strategy makes it simple to add new evaluation methods without changing existing code.

This comprehensive system design follows SOLID principles and demonstrates how modern C# features like delegates, events, and lambda expressions can be combined with object-oriented programming to create flexible and maintainable software systems.
