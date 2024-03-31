using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;


namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
  private readonly Name _name;
  private readonly Document _document;
  private readonly Email _email;
  private readonly Address _address;
  private readonly Student _student;
  private readonly Subscription _subscription;

  public StudentTests()
  {
    _name = new Name("Douglas", "Dias");
    _document = new Document("40336334800", EDocumentType.Cpf);
    _email = new Email("douglas@gmail.com");
    _address = new Address("Rua 1", "1234", "Bairro Legal", "Gotham", "SP", "BR", "13400000");
    _student = new Student(_name, _document, _email);
    _subscription = new Subscription(null);    
  }

  [TestMethod]
  public void ShouldReturnErrorWhenActiveSubscription()
  {
    var payment = new PayPalPayment("123456789",DateTime.UtcNow, DateTime.UtcNow.AddDays(5), 10, 10, "douglas@gmail.com", _document, _address, _email);

    _subscription.AddPayment(payment);
    _student.AddSubscription(_subscription);
    _student.AddSubscription(_subscription);

    Assert.IsFalse(_student.IsValid);
  }

  [TestMethod]
  public void ShouldReturnSuccessWhenAddSubscription()
  {
    var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);
    
    _subscription.AddPayment(payment);
    _student.AddSubscription(_subscription);
    Assert.IsFalse(_student.IsValid);
  }

  [TestMethod]
  public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
  {
    _student.AddSubscription(_subscription);

    Assert.IsFalse(_student.IsValid);
  }
}
