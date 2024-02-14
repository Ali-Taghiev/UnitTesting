namespace UTApplication.Services;

public interface IIdentityValidator
{
    bool IsValid(string IdentityNum);
    string Country {  get; }
    public ValidationMode ValidationMode { get; set; }

}
public enum ValidationMode
{   None,
    Detailed,
    Quick
}