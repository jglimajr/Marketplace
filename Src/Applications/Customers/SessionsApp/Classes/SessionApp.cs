using System;

namespace InteliSystem.InteliMarketPlace.Applications.SessionsApp
{
    public class SessionApp
    {
        public string Id { get; set; }
        public Guid IdCustomer { get; set; }
        public string Device { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }
}