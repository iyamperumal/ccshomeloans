namespace CcsWeb
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Threading.Tasks;

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message) => 
            Task.FromResult<int>(0);
    }
}

