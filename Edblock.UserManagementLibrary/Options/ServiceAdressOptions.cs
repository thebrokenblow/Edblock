namespace Edblock.UserManagementLibrary.Options;

public class ServiceAdressOptions
{
    public const string SectionName = nameof(ServiceAdressOptions);
    public required string IdentityServer { get; set; }
    public required string UserManagementService { get; set; }
}