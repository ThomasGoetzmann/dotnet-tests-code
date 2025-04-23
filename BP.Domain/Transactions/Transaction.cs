namespace BP.Domain;

public class Transaction
{
    public string TransactionId { get; set; }
    public string SourceAccountNumber { get; set; }
    public string DestinationAccountNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
}