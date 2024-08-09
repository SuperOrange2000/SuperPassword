namespace SuperPassword.Entity.Interface
{
    public enum ResponseStatus
    {
        Success,
        NoContent,
        Warning,
        ClientError,
        NetworkError,
        ServerInternalError,
        DatabaseError,
    }
}
