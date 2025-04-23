namespace BP.Domain.UnitTests;

public class AccountUnitTests
{
    [Fact]
    [Trait("TestType", "State change")]
    public void ChangeOwnerName_ExistingAccount_NameChanged()
    {
        // Arrange
        var account = new Account(
            "0123",
            "Thomas GOETZMANN",
            1000);
        
        // Act
        account.ChangeOwnerName("Tom");
        
        // Assert
        Assert.Equal("Tom", account.OwnerName);
    }

    [Fact]
    [Trait("TestType", "Return")]
    public void Withdraw_LessThanBalance_ReturnsRemainingBalance()
    {
        // Arrange
        var account = new Account(
            "0123",
            "Thomas Goetzmann",
            1000);
        
        // Act
        var remainingBalance = account.Withdraw(100);
        
        // Assert
        Assert.Equal(900, remainingBalance);
    }

    [Fact]
    [Trait("TestType", "Exception")]
    public void Deposit_FrozenAccount_ThrowsException()
    {
        // Arrange
        var account = new Account(
            "0123",
            "Thomas Goetzmann",
            1000);
        account.Freeze();
        
        //Act
        Action depositAction = () => account.Deposit(100);
        
        //Assert
        Assert.Throws<InvalidOperationException>(depositAction);
    }
}