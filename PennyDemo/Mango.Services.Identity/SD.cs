using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mango.Services.Identity
{
    public static class SD
    {
        //身分data, 使用身分服務器所保護的東西
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
           new List<IdentityResource>
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
           };
        //客戶端想訪問的資料 Scope作用域
        public static IEnumerable<ApiScope> ApiScopes =>
           new List<ApiScope> {
                new ApiScope("mango", "Mango Server"),
                new ApiScope(name: "read",   displayName: "Read your data."),
                new ApiScope(name: "write",  displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
           };

        //對於一個特定的服務器,可以有多個客戶端
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    ClientSecrets= { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes={ "read", "write","profile"}
                },
                 new Client
                {
                    ClientId="mango",
                    ClientSecrets= { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris={ "https://localhost:7017/signin-oidc" },  //換成https ,launchSettings.json(用ssiPort會錯) 用開啟的那個port才正確
                    PostLogoutRedirectUris={"https://localhost:7017/signout-callback-oidc" }, //簽名成功後送去的地方
                    AllowedScopes=new List<string>  //允許的作用區
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "mango"
                    }
                },
            };
    }
}
