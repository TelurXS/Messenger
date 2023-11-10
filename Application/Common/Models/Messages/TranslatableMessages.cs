namespace Application.Common.Models.Messages;

public static class TranslatableMessages
{
    public static class Validation
    {
        public const string PROPERTY_CANNOT_BE_EMPTY = "PROPERTY_CANNOT_BE_EMPTY";
        public const string PROPERTY_MUST_BE_CORRECT_LENGTH = "PROPERTY_MUST_BE_CORRECT_LENGTH";
        public const string PROPERTY_MUST_BE_EMAIL = "PROPERTY_MUST_BE_EMAIL";
            
        public static class Accounts
        {
            public const string LOGIN_IS_EXIST = "ACCOUNT_LOGIN_IS_EXIST";
            public const string EMAIL_IS_EXIST = "ACCOUNT_EMAIL_IS_EXIST";

            public const string LOGIN_IS_NOT_AVAILABLE = "ACCOUNT_LOGIN_IS_NOT_AVAILABLE";
            public const string EMAIL_IS_NOT_AVAILABLE = "ACCOUNT_EMAIL_IS_NOT_AVAILABLE";
            
            public const string ID_IS_NOT_EXIST = "ACCOUNT_ID_IS_NOT_EXIST";
        }
    
        public static class Groups
        {
            public const string ID_IS_NOT_EXIST = "GROUP_ID_IS_NOT_EXIST";
        }
    
        public static class Messages
        {
            public const string ID_IS_NOT_EXIST = "GROUP_ID_IS_NOT_EXIST";

            public const string SENT_AT_CANNOT_BE_IN_FUTURE = "SENT_AT_CANNOT_BE_IN_FUTURE";
        }
    }
}