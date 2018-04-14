using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace TestApp
{
    [Headers("Content-Type: application/json")]
    public interface IBankApi
    {
        [Get("/api/Branches")]
        Task<IList<BankBranch>> GetBranches();

        [Get("/api/Pays")]
        Task<IList<PayInfo>> GetPays([Header("Authorization")] string token);
        [Get("/api/Card")]
        Task<IList<CardInfo>> GetCards([Header("Authorization")] string token);

        [Get("/api/Card")]
        Task<CardInfo> GetCard(int id, [Header("Authorization")] string token);

        [Post("/api/Card")]
        Task<CardInfo> AddCard(CardInfo value, [Header("Authorization")] string token);

        [Put("/api/Card")]
        Task<CardInfo> ModifyCard(int id, CardInfo value, [Header("Authorization")] string token);

        [Delete("/api/Card")]
        Task DeleteCard(int id, [Header("Authorization")] string token);


        [Get("/api/UserData")]
        Task<BankUser> CurrentUser([Header("Authorization")] string token);

        [Post("/api/UserData")]
        Task<BankUser> CreateUser(BankUser value, [Header("Authorization")] string token);

        [Put("/api/UserData")]
        Task<BankUser> ModifyUser(int id, BankUser value, [Header("Authorization")] string token);
    }
}