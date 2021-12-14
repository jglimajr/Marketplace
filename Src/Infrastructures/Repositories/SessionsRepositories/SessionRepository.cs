using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using InteliSystem.InteliMarketPlace.Applications.SessionsApp;
using InteliSystem.InteliMarketPlace.Entities.Sessions;
using InteliSystem.InteliMarketPlace.Repositories;
using InteliSystem.Utils.Dapper.Extensions;
using Microsoft.VisualBasic;

namespace SessionsRepositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private string SqlSelect = "Select [Id], [IdCustomer], [Device], [Token], [RefreshToken], [Status], [DateTimeCreate], [DateTimeUpdate] From Sessions {0}";
        public SessionRepository(IDbConnection conn) : base(conn) { }

        public Task<Session> GetRefreshTokenAsync(string idcustomer, string device)
        {
            var sSql = string.Format(SqlSelect, "Where IdCustomer = @IdCustomer And Device = @Device");
            return this.Connection.QueryFirstOrDefaultAsync<Session>(sSql, new { IdCustomer = idcustomer, Device = device });
        }
    }
}