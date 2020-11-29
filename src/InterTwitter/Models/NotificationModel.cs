using InterTwitter.Enums;

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

        public string OwlText { get; set; }
    }
}
