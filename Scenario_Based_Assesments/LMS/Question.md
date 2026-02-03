# Question 1: Library Management System

## 📘 Scenario
A library needs a console application to manage books and categorize them by genre.

## 🛠️ Requirements

### In class Book, implement the following properties:
- `string Title`
- `string Author`
- `string Genre`
- `int PublicationYear`

### In class LibraryUtility, implement the following methods:

#### Method 1
```csharp
public void AddBook(string title, string author, string genre, int year)
```
- Adds book with auto-incremented ID

#### Method 2
```csharp
public SortedDictionary<string, List<Book>> GroupBooksByGenre()
```
- Groups books by genre alphabetically

#### Method 3
```csharp
public List<Book> GetBooksByAuthor(string author)
```
- Returns all books by specific author

#### Method 4
```csharp
public int GetTotalBooksCount()
```
- Returns total number of books

## Sample Use Cases
1. Add Fiction, Non-Fiction, Mystery books
2. Display books grouped by genre
3. Search books by specific author
4. Show statistics (total books, books per genre)
