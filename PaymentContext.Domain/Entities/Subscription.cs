using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.Emtities;

namespace PaymentContext.Domain.Entities;

public class Subscription : Entity
{
  private IList<Payment> _payments;
  public Subscription(DateTime? expireDate)
  {
    CreateDate = DateTime.UtcNow;
    LastUpdateDate = DateTime.UtcNow;
    ExpireDate = expireDate;
    Active = true;
    _payments = new List<Payment>();
  }

  public DateTime CreateDate { get; private set; }
  public DateTime LastUpdateDate { get; private set; }
  public DateTime? ExpireDate { get; private set; }
  public bool Active { get; private set; }
  public IReadOnlyCollection<Payment> Payments { get {return _payments.ToArray(); } }

  public void AddPayment(Payment payment)
  {
    AddNotifications(new Contract<Notification>().Requires()
      .IsGreaterThan(DateTime.UtcNow, payment.PaidDate, "Subscrption.Payments", "Data de pagamente precisa ser futura")
    );
    _payments.Add(payment);
  }

  public void Activate()
  {
    Active = true;
    LastUpdateDate = DateTime.UtcNow;
  }

  public void Inactivate()
  {
    Active = false;
    LastUpdateDate = DateTime.UtcNow;
  }
}