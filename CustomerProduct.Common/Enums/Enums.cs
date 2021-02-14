namespace CustomerProduct.Common.Enums
{
    public static class Enums
    {
        public enum EntityResponseCodes
        {
            Successfull = 0,
            NoRecordFound = -1,
            ValidationError = -2,
            DbError = -3
        }

        public enum ApiResponseCodes
        {
            Ok = 0,
            NotFound = -1,
            ValidationError = -2,
            DbError = -3
        }


    }
}
