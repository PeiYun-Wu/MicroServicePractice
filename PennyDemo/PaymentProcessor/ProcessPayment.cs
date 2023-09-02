namespace PaymentProcessor
{
    public class ProcessPayment : IProcessPayment
    {
        public bool PaymentProcessor()
        {
            //使用自定義 需符合PCI標準支付
            return true;
        }
    }
}