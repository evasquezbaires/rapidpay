namespace RapidPay.API.Domain.Constants
{
    public static class ErrorMessages
    {
        public const string CARD_EXISTS = "The card number already exists.";
        public const string CARD_NOT_EXISTS = "The card id entered Not exists.";
        public const string AMOUNT_NOT_ENOUGH = "The card amount is not enough for the transaction.";
        public const string USER_EXISTS = "Already exists a user with the same name.";
        public const string USER_NOT_EXISTS = "The user entered Not exists.";
    }
}
