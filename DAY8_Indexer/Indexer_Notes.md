# 📌 C# Indexers – In-Depth Notes (Beginner → Architect Level)

## 1️⃣ What is an Indexer?

An **Indexer** in C# allows an object to be accessed using **array-style syntax (`[]`)**.

Instead of calling methods like:
```csharp
obj.GetValue(0);
````

You can write:

```csharp
obj[0];
```

👉 Indexers make objects behave like **collections** while still following **OOP principles**.

---

## 2️⃣ Why Do We Need Indexers? (Architect Thinking)

### Problem Without Indexers

* Code becomes verbose
* Access feels unnatural
* No clean abstraction for collections

### Solution With Indexers

* Clean syntax
* Encapsulation is preserved
* Validation & rules can be enforced

📌 **Indexer = Controlled array-like access to object data**

---

## 3️⃣ Key Definition (Interview-Ready)

> An indexer is a special member that enables an object to be indexed like an array, providing controlled access to internal data.

---

## 4️⃣ Mental Model (Very Important)

* Indexer behaves like a **property**
* But:

  * Has **parameters**
  * Has **no name**
  * Uses `this`
  * Uses `[]`

👉 Think of indexers as **parameterized properties**

---

## 5️⃣ Basic Syntax of an Indexer

```csharp
public returnType this[indexType index]
{
    get
    {
        // return value
    }
    set
    {
        // assign value
    }
}
```

### Important Rules

* Must be inside a **class or struct**
* Uses `this`
* Cannot be static
* Can be overloaded

---

## 6️⃣ First Simple Example (Array-Based)

```csharp
class Marks
{
    private int[] _marks = new int[5];

    public int this[int index]
    {
        get
        {
            return _marks[index];
        }
        set
        {
            _marks[index] = value;
        }
    }
}
```

### Usage

```csharp
Marks m = new Marks();
m[0] = 85;
Console.WriteLine(m[0]);
```

---

## 7️⃣ Indexer vs Array (Architect Comparison)

| Feature        | Array  | Indexer    |
| -------------- | ------ | ---------- |
| Data exposure  | Direct | Controlled |
| Validation     | ❌      | ✅          |
| Encapsulation  | ❌      | ✅          |
| Business rules | ❌      | ✅          |

📌 **Indexer ≠ Data structure**

---

## 8️⃣ Adding Validation (Real-World Design)

```csharp
public int this[int index]
{
    get
    {
        if (index < 0 || index >= _marks.Length)
            throw new IndexOutOfRangeException();

        return _marks[index];
    }
    set
    {
        if (value < 0 || value > 100)
            throw new ArgumentException("Marks must be 0–100");

        _marks[index] = value;
    }
}
```

🧠 Architect Rule:

> Never expose internal data without validation.

---

## 9️⃣ Indexer with String Key (Dictionary Style)

```csharp
class Student
{
    private Dictionary<string, int> _subjects = new();

    public int this[string subject]
    {
        get
        {
            return _subjects.ContainsKey(subject) ? _subjects[subject] : 0;
        }
        set
        {
            _subjects[subject] = value;
        }
    }
}
```

### Usage

```csharp
Student s = new Student();
s["Math"] = 95;
Console.WriteLine(s["Math"]);
```

---

## 🔟 Multiple Indexers

A class can have **multiple indexers** with different parameter types.

```csharp
public int this[int index] { get; set; }
public string this[string key] { get; set; }
```

Used in:

* Configuration systems
* ORMs
* Caching layers

---

## 1️⃣1️⃣ Read-Only Indexer

```csharp
public int this[int index]
{
    get { return _marks[index]; }
}
```

✔ Used when modification is not allowed

---

## 1️⃣2️⃣ When to Use Indexers

✅ When your class:

* Represents a collection
* Needs array-like access
* Requires validation or rules
* Exposes data frequently

---

## 1️⃣3️⃣ When NOT to Use Indexers

❌ When:

* Only single value access is needed
* Method name gives better clarity
* Logic is complex

---

## 1️⃣4️⃣ Common Beginner Mistakes

* Treating indexer like a method
* No bounds checking
* Overusing indexers
* Using indexers for non-collection logic

---

## 1️⃣5️⃣ Interview One-Liner (Must Remember)

> “Indexers allow array-style access to objects while maintaining encapsulation and enforcing business rules.”

---

## 1️⃣6️⃣ Architect Summary

* Indexers improve API design
* They hide internal structure
* They promote clean, readable code
* They are widely used in framework-level code

---

## 1️⃣7️⃣ Practice Suggestions

1. Library book indexer
2. Employee salary indexer
3. Configuration key-value indexer
4. Read-only report indexer

📌 Mastery comes from **designing**, not memorizing.

```
