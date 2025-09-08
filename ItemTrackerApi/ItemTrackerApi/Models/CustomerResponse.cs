namespace ItemTrackerApi.Models
{
    public class CustomerResponse
    {
        #region Properties

        public string CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public DateTime RegistrationDate { get; set; }

        #endregion
    }
}
