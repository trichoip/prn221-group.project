namespace WebApp
{
    public class Constrains
    {
        public class AccountRole
        {
            public static string ADMIN = "admin";
            public static string USER = "user";
        }
        public class SessionAttribute
        {
            public static string AUTH_USER_NAME = "authUsername";
            public static string AUTH_USER_ROLE = "authUserRole";
            public static string AUTH_USER_FIRST_NAME = "authUserFirstName";
        }
        public class OrderStatus
        {
            public static string PENDING = "Pending";
            public static string DELIVERING = "Delivering";
            public static string COMPLETED = "Completed";
            public static string CANCELLED = "Cancelled";
            public static string DELETED = "Deleted";

        }
        public class RatingStatus
        {
            public static string PENDING = "Pending";
            public static string ACCEPTED = "Accepted";
            public static string REJECTED = "Rejected";

        }
    }
}
