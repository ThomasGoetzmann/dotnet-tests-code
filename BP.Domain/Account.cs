namespace BP.Domain;

using System;

public class Account
{
    public string AccountNumber { get; }
    public string OwnerName { get; private set; }
    public decimal Balance { get; private set; }
    public AccountStatus Status { get; private set; }

    public Account(string accountNumber, string ownerName, decimal initialBalance = 0)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Le numéro de compte ne peut pas être vide", nameof(accountNumber));
        
        if (string.IsNullOrWhiteSpace(ownerName))
            throw new ArgumentException("Le nom du propriétaire ne peut pas être vide", nameof(ownerName));
        
        if (initialBalance < 0)
            throw new ArgumentException("Le solde initial ne peut pas être négatif", nameof(initialBalance));

        AccountNumber = accountNumber;
        OwnerName = ownerName;
        Balance = initialBalance;
        Status = AccountStatus.Active;
    }

    public void ChangeOwnerName(string newOwnerName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newOwnerName);
        OwnerName = newOwnerName;
    }

    public decimal Deposit(decimal amount)
    {
        if (Status != AccountStatus.Active)
            throw new InvalidOperationException("Impossible d'effectuer un dépôt sur un compte non actif");
        
        if (amount <= 0)
            throw new ArgumentException("Le montant du dépôt doit être positif", nameof(amount));

        Balance += amount;
        return Balance;
    }

    public decimal Withdraw(decimal amount)
    {
        if (Status != AccountStatus.Active)
            throw new InvalidOperationException("Impossible d'effectuer un retrait sur un compte non actif");
        
        if (amount <= 0)
            throw new ArgumentException("Le montant du retrait doit être positif", nameof(amount));
        
        if (amount > Balance)
            throw new InvalidOperationException("Solde insuffisant pour effectuer ce retrait");

        Balance -= amount;
        return Balance;
    }

    public void Freeze()
    {
        if (Status != AccountStatus.Active)
            throw new InvalidOperationException("Impossible de geler un compte qui n'est pas actif");
        
        Status = AccountStatus.Frozen;
    }

    public void Close()
    {
        if (Status == AccountStatus.Closed)
            throw new InvalidOperationException("Ce compte est déjà fermé");
        
        Status = AccountStatus.Closed;
    }

    public void Reactivate()
    {
        if (Status == AccountStatus.Closed)
            throw new InvalidOperationException("Impossible de réactiver un compte fermé");
        
        Status = AccountStatus.Active;
    }
}

public enum AccountStatus
{
    Active,
    Frozen,
    Closed
}

