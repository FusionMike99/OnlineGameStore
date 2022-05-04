using System;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Entities
{
    public class Publisher : IBaseEntity<int>
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
