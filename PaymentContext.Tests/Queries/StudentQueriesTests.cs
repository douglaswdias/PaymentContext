using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentQueriesTests
{
  private IList<Student> _students;
  public StudentQueriesTests()
  {
    for (var i = 0; i <= 10; i++)
    {
      _students.Add(new Student(
        new Name("Aluno", i.ToString()), 
        new Document("1111111111" + i.ToString(), EDocumentType.Cpf), 
        new Email(i.ToString() + "@gmail.com")
      ));
    }
  }
  
  [TestMethod]
  public void ShouldReturnNullWhenDocumentNotExists()
  {
    var exp = StudentQueries.GetStudent("11111111111");
    var student = _students.AsQueryable().Where(exp).FirstOrDefault();

    Assert.AreEqual(null, student);
  }

  [TestMethod]
  public void ShouldReturnStudentWhenDocumentExists()
  {
    var exp = StudentQueries.GetStudent("11111111111");
    var student = _students.AsQueryable().Where(exp).FirstOrDefault();

    Assert.AreEqual(null, student);
  }
}
