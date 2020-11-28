using System;
namespace InterTwitter.Models
{
    public class UserModel 
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Picture { get; set; }

        public string ProfileHeaderImage { get; set; } = "pic_logo_small.png";
    }
}
