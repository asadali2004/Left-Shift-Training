# Flip Key Generator

A computer science instructor is designing a tool to help students understand ASCII values and string manipulation. The utility removes characters with even ASCII codes from a word and reverses the remaining ones, offering a hands-on way to explore how data can be transformed for security or encoding purposes.

## Method to Implement

In the class `Program`, implement the following method:

| Method | Description |
|---|---|
| `public string CleanseAndInvert(string input)` | Takes a word and transforms it into a customized key. |

### Validation Rules

1. **Input must not be null** and must be **at least 6 characters**. If not, return an empty string.
2. **Input must not contain** any **spaces, digits, or special characters**. If it does, return an empty string.

### Key Generation Logic

- Convert the input to **lowercase**.
- Remove all characters whose **ASCII values are even**.
- Reverse the remaining characters.
- In the reversed string, convert **even positioned characters** (0‑based index) to **uppercase**.
- Return the generated key.

## Main Method Requirements

- Prompt the user to enter a string input.
- Call `CleanseAndInvert` and print:
	- **"The generated key is - &lt;generated key&gt;"** if valid
	- **"Invalid Input"** if the method returns an empty string

## Notes

- Do not edit the existing code template.
- The sample inputs/outputs show user input in bold (in the original statement).
- Implement the business requirements within the `Main` method.
- Do not use `Environment.Exit()`.

## Sample Input/Output

### Sample Input 1
```
Enter the word
Aeroplane
```

### Sample Output 1
```
The generated key is - EaOeA
```

### Sample Input 2
```
Enter the word
Cowages
```

### Sample Output 2
```
The generated key is - SeGaWoC
```

### Sample Input 3
```
Enter the word
Magic
```

### Sample Output 3
```
Invalid Input
```

### Sample Input 4
```
Enter the word
Kinder World
```

### Sample Output 4
```
Invalid Input
```

### Sample Input 5
```
Enter the word
B@rbie
```

### Sample Output 5
```
Invalid Input
```


