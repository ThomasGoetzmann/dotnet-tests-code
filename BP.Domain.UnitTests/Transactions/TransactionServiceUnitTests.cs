using BP.Domain.Utils;
using NSubstitute;

namespace BP.Domain.UnitTests.Transactions;

public class TransactionServiceUnitTests
{
    [Fact]
    [Trait("TestType", "State change")]
    public void Transfer_FromOneAccountToAnother_DestinationBalanceHasChanged()
    {
        // Arrange
        var fakeLogger = Substitute.For<ITransactionLogger>();
        var fakeNotif = Substitute.For<INotificationService>();
        var fakeDateTime = Substitute.For<IDateTimeProvider>();
        var service = new TransactionService(fakeLogger, fakeNotif, fakeDateTime);

        var stefAccount = new Account("s", "Stef", 100);
        var domAccount = new Account("d", "Dom", 1000);
        
        // Act
        service.Transfer(stefAccount, domAccount, 70);

        // Assert
        Assert.Equal(1070, domAccount.Balance);
    }
    
    [Fact]
    [Trait("TestType", "State change")]
    public void Transfer_AtGivenTime_LogsTransaction()
    {
        // Arrange
        var fakeLogger = Substitute.For<ITransactionLogger>();
        var fakeNotif = Substitute.For<INotificationService>();
        var fakeDateTime = Substitute.For<IDateTimeProvider>();
        var service = new TransactionService(fakeLogger, fakeNotif, fakeDateTime);

        var birthdayThomas = new DateTime(2025, 04, 22);
        fakeDateTime.Now.Returns(birthdayThomas); // Configure the fake
        
        var tomAccount = new Account("ttt", "Tom", 100);
        var thomasAccount = new Account("sss", "Stef", 1000);
        
        // Act
        service.Transfer(tomAccount, thomasAccount, 70);

        // Assert
        fakeLogger
            .Received(1) // Check if the fake was called once
            .LogTransaction(
                Arg.Is<Transaction>(t => t.Timestamp == birthdayThomas)); // also check the argument
    }
}