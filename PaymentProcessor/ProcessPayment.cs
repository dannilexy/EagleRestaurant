namespace PaymentProcessor
{
    public class ProcessPayment : IProcessPayment
    {
        public Task<bool> PaymentProcessor()
        {
            //ImplementCustom Logic to process the payment
           return Task.FromResult(true);
        }
    }
}