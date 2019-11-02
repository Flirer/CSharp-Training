using System;
using System.Collections.Generic;
using System.Linq;

public class User
{
    private string _firstName;
    private string _lastName;

    public readonly int Salary;
    public readonly int Id;

    public string FullName => ToFullName(_firstName, _lastName);

    public User(int id, string firstName, string lastName, int salary)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException(firstName);

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException(lastName);

        Id = id;
        _firstName = firstName;
        _lastName = lastName;
        Salary = salary;
    }

    public static string ToFullName(string firstName, string lastname)
    {
        return firstName + " " + lastname;
    }
}


public class UserStorage
{
    private List<User> _users;

    public UserStorage()
    {
        _users = new List<User>();
    }

    public User AddUser(User user)
    {
        if (user == null)
            throw new ArgumentException(user);

        if (TryGetUser(user.FullName) != null)
            throw new ArgumentException(user.FullName);

        if (TryGetUser(user.Id) != null)
            throw new ArgumentException(user.Id);

        _users.Add(user);

        return user;
    }

    public User TryGetUser(string firstName, string lastName)
    {
        return _users.FirstOrDefault(user => user.FullName == User.ToFullName(firstName, lastName));
    }

    public User TryGetUser(string FullNfme)
    {
        return _users.FirstOrDefault(user => user.FullName == FullNfme);
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

