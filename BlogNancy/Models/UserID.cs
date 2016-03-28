using System;
using Nancy.Security;
using System.Collections.Generic;

namespace BlogNancy.Models
{
    public class UserID : IUserIdentity
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; private set; }
    }
}