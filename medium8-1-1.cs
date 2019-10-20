using System;
using System.Collections.Generic;
using System.Linq;

public class User
{
    private int _id;
    private string _firstName;
    private string _lastName;
    private int _salary;

    public int Salary { get => _salary; set => _salary = value; }
    public string FullName { get => _firstName + " " + _lastName; }
    public int Id { get => _id; }

    private User(int Id, string FirstName, string LastName, int Salary)
    {
        _id = Id;
        _firstName = FirstName;
        _lastName = LastName;
        _salary = Salary;
    }

    public static User CreateUser(int Id, string FirstName, string LastName, int Salary)
    {
        if (string.IsNullOrWhiteSpace(FirstName))
            throw new ArgumentException();

        if (string.IsNullOrWhiteSpace(LastName))
            throw new ArgumentException();

        return new User(Id, FirstName, LastName, Salary);
    }
}


public class UsersManager
{
    private List<User> _users;

    public UsersManager()
    {
        _users = new List<User>();
    }

    public User AddUser(int Id, string FirstName, string LastName, int Salary)
    {
        if (TryGetUser(FirstName, LastName) != null)
            throw new ArgumentException();

        if (TryGetUser(Id) != null)
            throw new ArgumentException();

        User user = User.CreateUser(Id, FirstName, LastName, Salary);
        _users.Add(user);

        return user;
    }

    public bool TryRemoveUser(int id)
    {
        return _users.Remove(TryGetUser(id));
    }

    public bool TryRemoveUser(string FirstName, string LastName)
    {
        return _users.Remove(TryGetUser(FirstName, LastName));
    }

    public User TryGetUser(string FirstName, string LastName)
    {
        return _users.FirstOrDefault(user => user.FullName == FirstName + " " + LastName);
    }

    public User TryGetUser(int id)
    {
        return _users.FirstOrDefault(user => user.Id == id);
    }

    public IEnumerable<User> GetUsersWithSalaryMoreThen(int salary)
    {
        return _users.Where(user => user.Salary > salary);
    }

    public IEnumerable<User> GetUsersWithSalaryLessThen(int salary)
    {
        return _users.Where(user => user.Salary < salary);
    }

    public IEnumerable<User> GetUsersWithSalaryBetween(int salaryMin, int salaryMax)
    {
        return _users.Where(user => (user.Salary >= salaryMin && user.Salary <= salaryMax));
    }
}

