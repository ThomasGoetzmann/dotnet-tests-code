namespace BP.Domain;

public interface ITransactionLogger
{
    void LogTransaction(Transaction transaction);
}