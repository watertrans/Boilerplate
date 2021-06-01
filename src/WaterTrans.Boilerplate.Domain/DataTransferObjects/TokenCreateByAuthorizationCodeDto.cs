using System;

namespace WaterTrans.Boilerplate.Domain.DataTransferObjects
{
    public class TokenCreateByAuthorizationCodeDto
    {
        public string GrantType { get; set; }
        public string Scope { get; set; }
        public string ClientId { get; set; }
        public string Code { get; set; }
    }
}
