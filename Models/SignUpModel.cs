namespace TwitterClient.Models
{
    public class SignUpModel
    {
        public string fullname { get; set; }
        public string username { get; set; }
        public string email { set; get; }
        public string password { set; get; }
    }
    public class UserLogin
    {
        public string message { get; set; }
        public bool result { get; set; }
        public string value { get; set; }
    }
    public class LoginResultOutput
    {
        public UserLogin UserLogin { get; set; }


    }
    public class RegisterUserOutPut
    {
        public bool RegisterUser { get; set; }
    } 
    public class Twitte
    {
        public Guid Id { set; get; }
        public string TextTwite { set; get; }
    }
    public class UserProfile
    {
        public string FullName { set; get; }
        public string Email { set; get; }
        public int FollowerCount { set; get; }
        public int FollowingCount { set; get; }
        public int TwiteCount { get { return (Twittes != null ? Twittes.Count : 0) + (reTwittes != null ? reTwittes.Count : 0); } }
        public List<Twitte> Twittes { set; get; }
        public List<Twitte> reTwittes { set; get; }
        public List<AllUser> AllUser { get; set; }

    }
    public class TwitterUserProfile
    {
        public UserProfile UserProfile { set; get; }
    }
    public partial class CreateReTwitteResult
    { 
        public CreateReTwitte CreateReTwitte { get; set; }      
        public CreateReTwitte CreateTwitte { get; set; }
    }
    
    public partial class CreateReTwitte
    { 
        public bool Result { get; set; }
    }

    public class FollowOutPut
    {
        public bool addFollow { get; set; }
    }
    public   class AllUser
    { 
        public string FullName { get; set; } 
        public string Email { get; set; } 
        public string Id { get; set; }
    }

}
