namespace CcsWeb
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Threading.Tasks;

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message) => 
            Task.FromResult<int>(0);
    }
}

