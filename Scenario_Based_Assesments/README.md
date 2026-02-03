# Scenario Based Practice - C# Learning

A comprehensive collection of **C# scenario-based practice projects** covering object-oriented programming concepts, data structures, exception handling, unit testing, and real-world application development.

---

## 📁 Project Structure

```
Scenario_Based_Assesments/
│
├── LMS/                                       # Library Management System
├── Hotel_Room_Booking_System/                 # Hotel Room Booking System
├── Employee_Management_System/                # Employee Management System
├── Restaurant_Management_System/              # Restaurant Management System
├── BikeRental/                                # Bike Rental System
├── Exception_Handling_Practice_3rd_FEB/       # Exception Handling Scenarios
├── Factory-Robot-Hazard-Analyzer/             # Robot Hazard Analysis System
├── Flip_Key/                                  # Flip Key Generator
├── Stream_Buzz/                               # StreamBuzz Engagement Tracker
├── Nunit-Assement-Q1/                         # NUnit Bank Account Testing
├── String_Based_Assessments/                  # String-based practice projects
│   ├── WordWand/
│   ├── MyNameIsYours/
│   ├── PasswordGenerator/
│   ├── GoAirSecurity/
│   └── ShopValidator/
├── StudentApp/                                # Delegates and student result processing
│
└── Scenario_Based_Assesments.slnx             # Solution File
```

---

## 📚 Projects Overview

| # | Project Name | Topics Covered | Description |
|---|---|---|---|
| **1** | **LMS** | Collections, LINQ, Dictionaries | Library management system with book categorization by genre |
| **2** | **Hotel_Room_Booking_System** | OOP, Collections, Dictionary | Hotel room booking management with price filtering |
| **3** | **Employee_Management_System** | OOP, Collections, Sorting | Employee management organized by departments |
| **4** | **Restaurant_Management_System** | *To be implemented* | Restaurant management features |
| **5** | **BikeRental** | Collections, SortedDictionary | Bike rental system with brand-based grouping |
| **6** | **Exception_Handling_Practice_3rd_FEB** | Exception Handling, Try-Catch | Banking, file operations, and bonus processing scenarios |
| **7** | **Factory-Robot-Hazard-Analyzer** | Custom Exceptions, Validation, Math | Robot hazard risk calculation with validation rules |
| **8** | **Flip_Key** | String Manipulation, ASCII, Validation | Key generation from string input with transformations |
| **9** | **Stream_Buzz** | Collections, Dictionary, LINQ | Content creator engagement tracking system |
| **10** | **Nunit-Assement-Q1** | Unit Testing, NUnit | Bank account test cases for deposit and withdrawal |
| **11** | **WordWand** | String Manipulation, Validation | Word transformations and filtering tasks |
| **12** | **MyNameIsYours** | String Matching, Subsequences | Name subsequence checking |
| **13** | **PasswordGenerator** | String Rules, Validation | Password generation and username validation |
| **14** | **GoAirSecurity** | String Parsing, Validation | Airport security entry checks |
| **15** | **ShopValidator** | String Parsing, Validation | Warranty/receipt validation |
| **16** | **StudentApp** | Delegates, LINQ, Sorting | Student results processing with delegate usage |

---

## 🎯 Detailed Project Descriptions

### 1️⃣ **LMS - Library Management System**
- **Key Concepts**: Generics, Collections, Dictionaries, LINQ
- **Main Features**:
  - Add books with auto-incremented IDs
  - Group books by genre (alphabetically sorted)
  - Search books by author
  - Display total book count and statistics
- **Key Method**: `GroupBooksByGenre()` → `SortedDictionary<string, List<Book>>`

### 2️⃣ **Hotel_Room_Booking_System**
- **Key Concepts**: OOP, Dictionary, Collections
- **Main Features**:
  - Manage room inventory with different types (Single/Double/Suite)
  - Group available rooms by type
  - Book rooms for specified nights with cost calculation
  - Filter rooms by price range
- **Key Method**: `GetAvailableRoomsByPriceRange(min, max)`

### 3️⃣ **Employee_Management_System**
- **Key Concepts**: OOP, Collections, SortedDictionary, DateTime
- **Main Features**:
  - Add employees with auto-generated IDs (E001, E002...)
  - Group employees by department
  - Calculate department-wise salary expenditure
  - Filter employees by joining date
- **Key Method**: `GroupEmployeesByDepartment()` → `SortedDictionary<string, List<Employee>>`

### 4️⃣ **BikeRental - Bike Rental System**
- **Key Concepts**: Collections, SortedDictionary, Inventory Management
- **Main Features**:
  - Add bike details (model, brand, price per day)
  - Group bikes by brand in sorted order
  - Manage bike inventory efficiently
- **Key Method**: `GroupBikesByBrand()` → `SortedDictionary<string, List<Bike>>`

### 5️⃣ **Exception_Handling_Practice_3rd_FEB**
- **Key Concepts**: Exception Handling, Try-Catch-Finally, Custom Validation
- **Scenarios**:
  - **BankingWithdrawalValidation**: Validate withdrawal amounts
  - **EmployeeBonusProcessing**: Calculate bonuses with error handling
  - **ValidateFileReading**: Read and validate file data
- **Focus**: Robust error handling and user input validation

### 6️⃣ **Factory-Robot-Hazard-Analyzer**
- **Key Concepts**: Custom Exceptions, Input Validation, Mathematical Calculations
- **Main Features**:
  - Validate arm precision (0.0-1.0)
  - Validate worker density (1-20)
  - Validate machinery state (Worn/Faulty/Critical)
  - Calculate hazard risk using formula: `((1.0 - armPrecision) × 15.0) + (workerDensity × machineRiskFactor)`
- **Custom Exception**: `RobotSafetyException`

### 7️⃣ **Flip_Key - Key Generator**
- **Key Concepts**: String Manipulation, ASCII Values, Validation
- **Algorithm**:
  1. Validate input (6+ chars, no spaces/digits/special chars)
  2. Convert to lowercase
  3. Remove characters with even ASCII values
  4. Reverse the remaining string
  5. Convert even-positioned characters to uppercase
- **Example**: "Aeroplane" → "EaOeA"

### 8️⃣ **Stream_Buzz - Engagement Tracker**
- **Key Concepts**: Collections, Dictionary, LINQ, Data Aggregation
- **Main Features**:
  - Register creators with weekly engagement metrics
  - Track top-performing weeks based on like thresholds
  - Calculate average engagement across all creators
  - Interactive menu system
- **Key Method**: `GetTopPostCounts(likeThreshold)` → `Dictionary<string, int>`

### 9️⃣ **Nunit-Assement-Q1 - Bank Account Testing**
- **Key Concepts**: Unit Testing, NUnit Framework, Test Cases
- **Test Cases**:
  - Deposit valid amount
  - Deposit negative amount (exception)
  - Withdraw valid amount
  - Withdraw with insufficient funds (exception)
- **Methods Tested**: `Deposit()`, `Withdraw()`, `Balance` property

### 🔟 **StudentApp - Student Result Processing**
- **Key Concepts**: Delegates, LINQ, Sorting, Action/Func/Predicate
- **Main Features**:
  - Create and list students with marks
  - Sort students by marks
  - Evaluate pass/fail using delegate logic

---

## 🚀 Getting Started

### Prerequisites
- .NET SDK 10.0 or higher
- Visual Studio, VS Code, or any C# IDE
- NUnit framework (for unit testing project)

### Running a Project

```bash
# Navigate to project folder
cd LMS

# Build the project
dotnet build

# Run the project
dotnet run
```

### Opening Solution
```bash
# Open the solution file
dotnet open Scenario_Based_Assesments.slnx
```

---

## 🎓 Learning Path

**Beginners** → Collections & OOP
- Start with: `LMS` → `Hotel_Room_Booking_System` → `Employee_Management_System`

**Intermediate** → String Manipulation & Validation
- Continue with: `BikeRental` → `Flip_Key` → `String_Based_Assessments`

**Advanced** → Exception Handling & Testing
- Explore: `Exception_Handling_Practice_3rd_FEB` → `Stream_Buzz` → `Nunit-Assement-Q1`

---

## 📊 Topics Covered

| Topic | Projects |
|-------|----------|
| **Collections & Generics** | LMS, Hotel_Room_Booking_System, Employee_Management_System, BikeRental, Stream_Buzz |
| **Dictionary & Sorting** | LMS, Employee_Management_System, BikeRental |
| **OOP Principles** | All projects |
| **String Manipulation** | Flip_Key, WordWand, MyNameIsYours, PasswordGenerator, GoAirSecurity, ShopValidator |
| **Delegates & Functional Types** | StudentApp |
| **Exception Handling** | Factory-Robot-Hazard, Exception_Handling |
| **Custom Exceptions** | Factory-Robot-Hazard-Analyzer |
| **Unit Testing (NUnit)** | Nunit-Assement-Q1 |
| **Data Validation** | All projects |
| **DateTime Operations** | 03_Employee_Management_System |
| **Mathematical Formulas** | Factory-Robot-Hazard-Analyzer |

---

## 📝 Notes

- Each project is independent and can be run separately
- All projects follow best practices for code structure and naming conventions
- Exception handling is implemented where required
- Projects demonstrate real-world scenarios and use cases
- Code includes proper validation and error handling

---

## 👨‍💻 Contributing

These projects are part of a learning journey. Feel free to enhance, refactor, or add additional features to practice different C# concepts.

---

**Happy Learning! 🎉**
