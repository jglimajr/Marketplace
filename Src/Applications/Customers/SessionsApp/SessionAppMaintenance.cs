using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Domains.Sessions;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Classes.Abstracts;
using InteliSystem.Utils.Globals.Enumerators;

namespace InteliSystem.InteliMarketPlace.Applications.SessionsApp
{
    public class SessionAppMaintenance : ClassBaseMaintenance<SessionApp>
    {
        private readonly ISessionRepository _repository;
        public SessionAppMaintenance(ISessionRepository repository)
            : base(repository)
        {
            this._repository = repository;
        }

        public override Task<object> AddAsync(SessionApp app)
        {
            return Task.Run<object>(() =>
            {
                if (app.IsNull())
                {
                    this.AddNotification("Session", "Session not found");
                    return new Return(ReturnValues.Failed, null);
                }

                return null;
            });
        }

    }
}