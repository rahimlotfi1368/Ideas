using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Base
{
    public class Audit<TKey>: BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public Guid CreatorUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? DeleterUserId { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Guid? ModifierUserId { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
