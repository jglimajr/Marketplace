using System;
using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Entities.Sessions;
using InteliSystem.Utils.Authentications;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Classes.Abstracts;
using InteliSystem.Utils.Globals.Enumerators;
using System.Security.Claims;

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

        public Task<Return> AddAsync(SessionApp app)
        {
            return Task.Run<Return>(() =>
            {
                if (app.IsNull())
                {
                    this.AddNotification("Session", "Session not found");
                    return new Return(ReturnValues.Failed, null);
                }

                var id = Guid.NewGuid();
                var user = new User()
                {
                    Id = id.ToString(),
                    Device = app.Device,
                    IdCustomer = app.IdCustomer.ToString()
                };

                var token = JwtAuthentication.GetToken(user);
                var refreshtoken = JwtAuthentication.GetRefreshToken();

                var existsession = this._repository.GetRefreshTokenAsync(app.IdCustomer.ObjectToString(), app.Device).GetAwaiter().GetResult();

                Session session = null;
                var retAux = 0;
                if (existsession.IsNull())
                {
                    session = new Session(id, app.IdCustomer, app.Device, token, refreshtoken);

                    if (session.ExistNotifications)
                    {
                        this.AddNotifications(session.GetAllNotifications);
                        return new Return(ReturnValues.Failed, null);
                    }

                    retAux = this._repository.AddAsync(session).GetAwaiter().GetResult();
                    if (retAux <= 0)
                    {
                        this.AddNotification("Session", "Problem to save your session");
                        return new Return(ReturnValues.Failed, null);
                    }

                    return new Return(ReturnValues.Success, new SessionApp().Load(session));
                }

                session = new Session(existsession.Id, existsession.IdCustomer, existsession.Device, token, refreshtoken);

                retAux = this._repository.UpdateAsync(existsession).GetAwaiter().GetResult();
                if (retAux <= 0)
                    return new Return(ReturnValues.Failed, null);

                return new Return(ReturnValues.Success, new SessionApp().Load(session));
            });
        }

        public Task<Return> GetSessionAsync(string idcustomer, string device)
        {
            return Task.Run<Return>(() =>
            {

                var session = this._repository.GetRefreshTokenAsync(idcustomer, device).GetAwaiter().GetResult();

                if (session.IsNull())
                    return new Return(ReturnValues.Failed, "");

                return new Return(ReturnValues.Success, new SessionApp().Load(session));
            });
        }

        public Task<Return> UpdateAsync(string id, SessionApp session)
        {
            return Task.Run<Return>(() =>
            {
                var sessioupdate = new Session(new Guid(session.Id), session.IdCustomer, session.Device, session.Token, session.RefreshToken);
                var retAux = this._repository.UpdateAsync(sessioupdate).GetAwaiter().GetResult();
                if (retAux <= 0)
                    return new Return(ReturnValues.Failed, null);
                return new Return(ReturnValues.Success, session);
            });
        }

        public Task<Return> RefreshTokenAsync(string token, string refreshtoken)
        {
            return Task.Run<Return>(() =>
            {
                var principal = JwtAuthentication.GetClaimsPrincipal(token);


                var id = principal.FindFirst(ClaimTypes.Actor);
                var idcustomer = principal.FindFirst("IdCustomer");
                var device = principal.FindFirst("Device");

                // var session = this._repository.GetRefreshTokenAsync(idcustomer.Value, device.Value).GetAwaiter().GetResult();
                var session = this._repository.GetAsync(new Session(id: new Guid(id.Value), idcustomer: new Guid(idcustomer.Value), device: device.Value, "", "")).GetAwaiter().GetResult();

                if (refreshtoken != session.RefreshToken || token != session.Token)
                {
                    return new Return(ReturnValues.Failed, null);
                }

                if (!JwtAuthentication.IsExpiredToken(token))
                    return new Return(ReturnValues.Success, new { Token = token, RefreshToken = refreshtoken });

                var newtoken = JwtAuthentication.GetToken(principal.Claims);
                var newrefreshtoken = JwtAuthentication.GetRefreshToken();

                var updtsession = new SessionApp().Load(session);

                updtsession.Token = newtoken;
                updtsession.RefreshToken = newrefreshtoken;

                var retaux = this.UpdateAsync(updtsession.Id, updtsession);

                return new Return(ReturnValues.Success, new { Token = newtoken, RefreshToken = newrefreshtoken });

            });
        }

    }
}