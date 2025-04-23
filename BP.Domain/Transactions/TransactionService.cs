namespace BP.Domain;

public class TransactionService
{
    private readonly ITransactionLogger _logger;
    private readonly INotificationService _notificationService;

    public TransactionService(ITransactionLogger logger, INotificationService notificationService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
    }

    public void Transfer(Account source, Account destination, decimal amount)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
            
        if (destination == null)
            throw new ArgumentNullException(nameof(destination));
            
        if (amount <= 0)
            throw new ArgumentException("Le montant du transfert doit être positif", nameof(amount));

        // Effectuer le transfert
        source.Withdraw(amount);
        destination.Deposit(amount);

        // Journaliser la transaction
        _logger.LogTransaction(new Transaction
        {
            TransactionId = Guid.NewGuid().ToString(),
            SourceAccountNumber = source.AccountNumber,
            DestinationAccountNumber = destination.AccountNumber,
            Amount = amount,
            Timestamp = DateTime.Now
        });

        // Notifier le propriétaire du compte source
        _notificationService.NotifyCustomer(
            source.OwnerName, 
            $"Un transfert de {amount:C} a été effectué vers le compte {destination.AccountNumber}."
        );
    }
}