using System;

namespace Project.Model
{
    public class Audit
    {
        public Guid Id { get; set; }
        public string AuditUserId { get; set; }
        public string ChangeType { get; set; }
        public string ObjectType { get; set; }
        public string FromJson { get; set; }
        public string ToJson { get; set; }
        public string TableName { get; set; }
        public string IdentityJson { get; set; }
        public DateTime DateCreated { get; set; }
    }
}