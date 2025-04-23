namespace BP.Domain;

public interface INotificationService
{
    void NotifyCustomer(string customerName, string message);
}