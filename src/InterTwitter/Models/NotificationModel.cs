using InterTwitter.Enums;
using System.Collections.Generic;

namespace InterTwitter.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public int OwlId { get; set; }

        public int UserId { get; set; }

        public string UserAvatar { get; set; }

        public string UserName { get; set; }

        public OwlAction Action { get; set; }

        public OwlType MediaType { get; set; }

        public string OwlText { get; set; }

        public List<string> MediaList { get; set; }

    }
}
