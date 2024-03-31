using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler : Notifiable<Notification>, 
  IHandler<CreateBoletoSubscriptionCommand>
  // IHandler<CreateCreditCardSubscriptionCommand>
{
  private readonly IStudentRepository _repository;
  private readonly IEmailService _emailService;

  public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
  {
    _repository = repository;
    _emailService = emailService;
  }
  public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
  {
    command.Validate();
    if (command.IsValid!)
    {
      AddNotifications(command);
      return new CommandResult(false, "Cadastro não realizado");
    }

    if (_repository.DocumentExists(command.Document))
    {
      AddNotification("Document", "Documento ja existe");
    }
    
    if (_repository.EmailExists(command.PayerEmail))
    {
      AddNotification("Email", "E-mail ja existe");
    }

    var name = new Name(command.FirstName, command.LastName);
    var document = new Document(command.Document, EDocumentType.Cpf);
    var email = new Email(command.PayerEmail);
    var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

    var student = new Student(name, document, email);
    var subscription = new Subscription(DateTime.UtcNow.AddMonths(1)); 
    var payment = new BoletoPayment(
        command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Owner, 
        new Document(command.Document, command.PayerDocumentType), address, email
      );

    subscription.AddPayment(payment);
    student.AddSubscription(subscription);

    AddNotifications(name, document, email, address, student, subscription, payment);

    if (IsValid!)
    {
      return new CommandResult(false, "Nao foi possivel realizar assiantura");
    }

    _repository.CreateSubscription(student);

    _emailService.SendEmail(student.Name.ToString(), student.Email.Address, "Bem vinddo(a)", "Assinatura criada");

    return new CommandResult(true, "Assinatura realizada com sucesso");
  }

    // public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
    // {
    //   if (_repository.DocumentExists(command.Document))
    //   {
    //     AddNotification("Document", "Documento ja existe");
    //   }
      
    //   if (_repository.EmailExists(command.PayerEmail))
    //   {
    //     AddNotification("Email", "E-mail ja existe");
    //   }

    //   var name = new Name(command.FirstName, command.LastName);
    //   var document = new Document(command.Document, EDocumentType.Cpf);
    //   var email = new Email(command.PayerEmail);
    //   var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

    //   var student = new Student(name, document, email);
    //   var subscription = new Subscription(DateTime.UtcNow.AddMonths(1)); 
    //   var payment = new PayPalPayment(
    //       command.LastTransactionNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Owner, 
    //       new Document(command.Document, command.PayerDocumentType), address, email
    //     );

    //   subscription.AddPayment(payment);
    //   student.AddSubscription(subscription);

    //   AddNotifications(name, document, email, address, student, subscription, payment);

    //   _repository.CreateSubscription(student);

    //   _emailService.SendEmail(student.Name.ToString(), student.Email.Address, "Bem vinddo(a)", "Assinatura criada");

    //   return new CommandResult(true, "Assinatura realizada com sucesso");
    // }
}
