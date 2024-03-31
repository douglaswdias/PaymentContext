using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class DocumentTests
{
  [TestMethod]
  public void ShouldReturnErrorWhenCnjpIsInvalid()
  {
    var doc = new Document("123", EDocumentType.Cnpj);
    Assert.IsFalse(doc.IsValid);
  }

  [TestMethod]
  public void ShouldReturnSuccessWhenCnjpIsValid()
  {
    var doc = new Document("11111111111111", EDocumentType.Cnpj);
    Assert.IsTrue(doc.IsValid);
  }

  [TestMethod]
  public void ShouldReturnErrorWhenCpfIsInvalid()
  {
    var doc = new Document("123", EDocumentType.Cpf);
    Assert.IsFalse(doc.IsValid);
  }

  [TestMethod]
  public void ShouldReturnSuccessWhenCpfIsValid()
  {
    var doc = new Document("11111111111", EDocumentType.Cpf);
    Assert.IsTrue(doc.IsValid);
  }
}
