namespace SmashLeague.Services
{
    public abstract class ManagerErrorBase
    {
        public abstract string Message { get; }
        public abstract void AddError(string code, string message);
    }
}
