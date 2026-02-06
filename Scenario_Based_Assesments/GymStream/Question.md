# Project Brief: GymStream Membership Validation System

## 1. Executive Summary

GymStream is an emerging fitness-tech startup. To ensure a seamless user experience, the company requires a robust C# backend module to handle new membership enrollments. The system focuses on Business Rule Validation using custom exception handling and automated billing calculations based on loyalty-tier discounts.

---

## 2. Technical Specifications

### A. Exception Architecture

To maintain clean code and specific error reporting, the system utilizes a custom exception class.

- **Custom Exception**: InvalidTierException
- **Inheritance**: Must inherit from the system Exception class.
- **Purpose**: To be thrown when a user inputs a subscription tier not supported by the platform.

---

### B. Core Model: Membership Class

The system is built around the following data structure:

| Property Name      | Data Type | Description                                      |
|--------------------|-----------|--------------------------------------------------|
| Tier               | string    | The subscription plan (Basic, Premium, or Elite) |
| DurationInMonths   | int       | Length of the membership contract                |
| BasePricePerMonth  | double    | The standard monthly rate before discounts       |

---

### C. Functional Methods

#### Method 1: ValidateEnrollment()

- **Return Type**: bool
- **Logic Gate 1**: If Tier is not "Basic", "Premium", or "Elite", throw InvalidTierException.
- **Logic Gate 2**: If DurationInMonths $\le 0$, throw a standard Exception.
- **Success**: Return true if all business rules are satisfied.

#### Method 2: CalculateTotalBill()

- **Return Type**: double
- **Formula**: $Total = (Duration \times Price)$
- **Discounting Logic**:
  - Basic: 2% Discount
  - Premium: 7% Discount
  - Elite: 12% Discount
- **Final Output**: $Total - (Total \times \text{Discount Rate})$

---

## 3. Implementation Workflow

1. **Input Phase**: Capture user data for Tier, Duration, and Price.
2. **Try Block**: Attempt to call ValidateEnrollment().
3. **Calculation**: If no exception is thrown, invoke CalculateTotalBill().
4. **Catch Blocks**:
   - Intercept InvalidTierException to display specific tier errors.
   - Intercept general Exception for duration errors.
5. **Output**: Display the final bill formatted to two decimal places.

---

## 4. Sample Test Cases

| Scenario        | Tier Input | Duration | Result                                     |
|-----------------|------------|----------|--------------------------------------------|
| Success         | Premium    | 12       | Enrollment Successful.                     |
| Invalid Input   | Gold       | 6        | Error: Tier not recognized.                |
| Zero Duration   | Basic      | 0        | Error: Duration must be at least one month |
