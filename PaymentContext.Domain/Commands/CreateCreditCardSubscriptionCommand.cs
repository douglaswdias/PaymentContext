using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands;

public class CreateCreditCardSubscriptionCommand
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Document { get; set; }
  public string Address { get; set; }
  public string CardHolderName { get; private set; }
  public string CardNumber { get; private set; }
  public string LastTransactionNumber { get; private set; }
  public string PaymentNumber { get; set; }
  public DateTime PaidDate { get; set; }
  public DateTime ExpireDate { get; set; }
  public decimal Total { get; set; }
  public decimal TotalPaid { get; set; }
  public string Owner { get; set; }
  public string PayerDocument { get; set; }
  public EDocumentType PayerDocumentType { get; set; }
  public string Street { get; set; }
  public string Number { get; set; }
  public string Neighborhood { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string Country { get; set; }
  public string ZipCode { get; set; }
  public string PayerEmail { get; set; }
}