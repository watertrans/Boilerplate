﻿using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Constants;

namespace WaterTrans.Boilerplate.Domain.Entities
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public List<string> Roles { get; set; }
        public AccountStatus Status { get; set; }
        public DateTimeOffset? LastLoginTime { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }
}