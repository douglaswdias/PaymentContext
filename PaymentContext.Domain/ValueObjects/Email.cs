using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObject;

namespace PaymentContext.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string address)
    {
        Address = address;

        AddNotifications(new Contract<Notification>().Requires().IsEmail(Address, "Email.Address", "Email invalido"));
    }

    public string Address { get; private set; }
}