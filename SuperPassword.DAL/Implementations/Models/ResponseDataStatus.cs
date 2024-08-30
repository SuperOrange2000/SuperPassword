namespace SuperPassword.DAL.Implementations.Models
{
    public enum ResponseDataStatus
    {
        NoContent = 0,
        Success = 100,
        Warning = 200,
        Error = 400,
        ClientError,
        DatabaseError,
        JsonDeserializationFailure,
        NameConflictError,
        ResourcesNotFoundError,
        Forbidden,
    }
}
