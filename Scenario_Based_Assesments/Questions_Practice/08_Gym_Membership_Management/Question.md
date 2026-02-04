# Question 8: Gym Membership Management

## Scenario
A fitness center needs to manage memberships and class schedules.

## Requirements

### In class Member:
- `int MemberId`
- `string Name`
- `string MembershipType` (Basic/Premium/Platinum)
- `DateTime JoinDate`
- `DateTime ExpiryDate`

### In class FitnessClass:
- `string ClassName`
- `string Instructor`
- `DateTime Schedule`
- `int MaxParticipants`
- `List<string> RegisteredMembers`

### In class GymManager:

#### Method 1
```csharp
public void AddMember(string name, string membershipType, int months)
```
- Creates membership with expiry date

#### Method 2
```csharp
public void AddClass(string className, string instructor, DateTime schedule, int maxParticipants)
```

#### Method 3
```csharp
public bool RegisterForClass(int memberId, string className)
```
- Registers member if class has space

#### Method 4
```csharp
public Dictionary<string, List<Member>> GroupMembersByMembershipType()
```
- Groups members by their plan

#### Method 5
```csharp
public List<FitnessClass> GetUpcomingClasses()
```
- Returns classes scheduled for next 7 days

## Sample Use Cases:
- Manage different membership tiers
- Schedule fitness classes
- Register members for classes
- Check membership expiry
- View class occupancy
