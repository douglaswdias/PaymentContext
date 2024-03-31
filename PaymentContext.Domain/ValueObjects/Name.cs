using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObject;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{
  public Name(string firstName, string lastName)
  {
    FirstName = firstName;
    LastName = lastName;

    AddNotifications(new Contract<Notification>().Requires()
      .IsLowerThan(FirstName, 3, "Name.FirstName", "Nome deve ter mais de 3 caracteres")
      .IsGreaterThan(LastName, 15, "Name.LastName", "Sobrenome deve ter menos de 15 caracteres")
      .IsLowerThan(LastName, 3, "Name.LastName", "Sobrenome deve ter mais de 3 caracteres")
      .IsGreaterThan(FirstName, 15, "Name.FirstName", "Nome deve ter menos de 15 caracteres")
    );
  }

  public string FirstName { get; private set; }
  public string LastName { get; private set; }

  public override string ToString()
  {
    return $"{FirstName} {LastName}";
  }
}