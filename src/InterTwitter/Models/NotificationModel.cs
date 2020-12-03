using InterTwitter.Enums;

namespace InterTwitter.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }

        public UserModel Author { get; set; }

        public OwlModel Owl { get; set; }

        public UserModel User { get; set; }

        public OwlAction Action { get; set; }

    }
}
