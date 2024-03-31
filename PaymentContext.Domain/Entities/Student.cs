using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Emtities;

namespace PaymentContext.Domain.Entities;

public class Student : Entity
{
  private IList<Subscription> _subscriptions;
  public Student(Name name, Document document, Email email)
  {
    Name = name;
    Document = document;
    Email = email;
    _subscriptions = new List<Subscription>();

    AddNotifications(name, document, email);
  }
  public Name Name { get; set; }
  public Document Document { get; private set; }
  public Email Email { get; private set; }
  public Address Address { get; private set; } = new Address("", "", "", "", "", "", "");
  public IReadOnlyCollection<Subscription> Subscriptions { get {return _subscriptions.ToArray(); } }

  public void AddSubscription(Subscription subscription) 
  {
    // foreach (var sub in Subscriptions)
    // {
    //   sub.Inactivate();
    // }

    // _subscriptions.Add(subscription);
    var hasSubscriptionActive = false;
    foreach (var sub in Subscriptions)
    {
      if (sub.Active)
      {
        hasSubscriptionActive = true;
      }

      AddNotifications(new Contract<Subscription>()
        .Requires()
        .IsTrue(hasSubscriptionActive, "Student.Subscriptions", "Voce tem uma assinatura ativa")
        .AreEquals(0, subscription.Payments.Count, "Student.Subscriptions.Payments", "Assinatura sem pagamentos")
      );
    }
  }
}