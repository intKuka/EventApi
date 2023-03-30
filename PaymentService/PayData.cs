namespace PaymentService
{
    public class PayData
    {
        private Payment? _currentOperation;
        private readonly Payment _stub;

        public PayData()
        {
            _stub = new Payment
            {
                Id = Guid.Empty,
                DateCreation = DateTime.MinValue
            };
        }

        public Task CreateOperation(string message)
        {
            _currentOperation = new Payment
            {
                Description = message
            };
            return Task.CompletedTask;
        }

        public Task ConfirmOperation()
        {
            if (_currentOperation == null) return Task.CompletedTask;
            _currentOperation.State = PayState.Confirmed;
            _currentOperation.DateConfirmation = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task CancelOperation()
        {
            if (_currentOperation == null) return Task.CompletedTask;
            _currentOperation!.State = PayState.Canceled;
            _currentOperation.DateCancellation = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task<Payment> GetCurrentOperation()
        {
            return Task.FromResult(_currentOperation ?? _stub);
        }
    }
}
