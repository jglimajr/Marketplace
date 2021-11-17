using System;
using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Repositories;
using InteliSystem.Utils.Globals.Enumerators;
using Utils.Globals.Notifications;

namespace InteliSystem.Utils.Globals.Classes.Abstracts
{
    public abstract class ClassBaseMaintenance<T> : InteliNotification, IDisposable where T : class
    {
        private readonly IBaseRepository _repository;

        public ClassBaseMaintenance(IBaseRepository repository)
        {
            this._repository = repository;
        }

        public virtual void Dispose()
        {
            this._repository.Dispose();
        }
    }
}