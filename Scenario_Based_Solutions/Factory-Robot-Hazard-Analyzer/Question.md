# Factory Robot Hazard Analyzer

Develop a Factory Robot Hazard Analyzer system that evaluates the hazard risk score of a factory robot based on **arm precision**, **worker density**, and **machinery state**. The system must validate all inputs, calculate the hazard risk score, and handle invalid scenarios using a custom exception.

## Functionalities

In the class `RobotHazardAuditor`, implement the specified method:

| Method | Functionality |
|---|---|
| `public double CalculateHazardRisk(double armPrecision, int workerDensity, string machineryState)` | Calculates and returns the hazard risk score. |

### Validation Rules

- **Arm precision** must be in the range **0.0 to 1.0**.
	- If not, throw `RobotSafetyException` with the message: **"Error:  Arm precision must be 0.0-1.0"**
- **Worker density** must be in the range **1 to 20**.
	- If not, throw `RobotSafetyException` with the message: **"Error: Worker density must be 1-20"**
- **Machinery state** must be **"Worn"**, **"Faulty"**, or **"Critical"** (case‑sensitive).
	- If not, throw `RobotSafetyException` with the message: **"Error: Unsupported machinery state"**

### Machine Risk Factor

- **Worn → 1.3**
- **Faulty → 2.0**
- **Critical → 3.0**

### Hazard Risk Formula

$$
Hazard\ Risk = ((1.0 - armPrecision) \times 15.0) + (workerDensity \times machineRiskFactor)
$$

## Notes

- The exception object itself should display the message.
- Create a class `RobotSafetyException` that inherits from `Exception`.
- In `Program.Main`, call `CalculateHazardRisk` and print: **"Robot Hazard Risk Score: <risk>"**
- If an exception is thrown, catch it and display the exception message.
- Do not use `Environment.Exit()`.
- Do not change the given code template.

## Sample Input/Output

### Sample Input 1
```
Enter Arm Precision (0.0 - 1.0):
0.5
Enter Worker Density (1 - 20):
10
Enter Machinery State (Worn/Faulty/Critical):
Critical
```

### Sample Output 1
```
Robot Hazard Risk Score: 37.5
```

### Sample Input 2
```
Enter Arm Precision (0.0 - 1.0):
1.3
Enter Worker Density (1 - 20):
4
Enter Machinery State (Worn/Faulty/Critical):
Worn
```

### Sample Output 2
```
Error: Arm precision must be 0.0-1.0
```

### Sample Input 3
```
Enter Arm Precision (0.0 - 1.0):
0.7
Enter Worker Density (1 - 20):
26
Enter Machinery State (Worn/Faulty/Critical):
Critical
```

### Sample Output 3
```
Error: Worker density must be 1-20
```

### Sample Input 4
```
Enter Arm Precision (0.0 - 1.0):
0.3
Enter Worker Density (1 - 20):
14
Enter Machinery State (Worn/Faulty/Critical):
Optimal
```

### Sample Output 4
```
Error: Unsupported machinery state
```

