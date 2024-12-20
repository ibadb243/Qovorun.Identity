using Duende.IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Qovorun.Identity;

public class Configuration
{
    public static IEnumerable<ApiScope> ApiScopers => new List<ApiScope>();

    public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>();

    public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>();

    public static IEnumerable<Client> Clients => new List<Client>();
}