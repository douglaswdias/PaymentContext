using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;


namespace PaymentContext.Tests.Entities;

[TestClass]
public class SubscriptionHandlerTestes
{
  [TestMethod]
  public void ShouldReturnErrorWhenDocumentExists()
  {
    var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
    var command = new CreateBoletoSubscriptionCommand();
    command.FirstName = "Douglas";
    command.LastName = "Dias";
    command.Document = "11111111111";
    command.Address = "Tiradentes";
    command.BarCode = "123456789";
    command.BoletoNumber = "123456789";
    command.PaymentNumber = "123456";
    command.PaidDate = DateTime.UtcNow;
    command.ExpireDate = DateTime.UtcNow.AddMonths(1);
    command.Total = 60;
    command.TotalPaid = 60;
    command.Owner = "Douglas";
    command.PayerDocument = "40336334800";
    command.PayerDocumentType = EDocumentType.Cpf;
    command.PayerEmail = "douglas@gmail.com";
    command.Street = "Tiradentes";
    command.Number = "1122";
    command.Neighborhood = "Centro";
    command.City = "Dois Corregos";
    command.State = "SP";
    command.Country = "Brasil";
    command.ZipCode = "17300013";
    
    handler.Handle(command);
    
    Assert.AreEqual(false, handler.IsValid);  
  }
}
