using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Constants;

namespace WaterTrans.Boilerplate.Domain.Entities
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public string LoginId { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public int Iterations { get; set; }
        public List<string> Roles { get; set; }
        public AccountStatus Status { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime ConcurrencyToken { get; set; }
    }
}
