using System;
using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Repositories;
using InteliSystem.Utils.Globals.Enumerators;
using Utils.Globals.Notifications;

namespace InteliSystem.Utils.Globals.Classes.Abstracts
{
    public abstract class ClassBaseMaintenance<T> : InteliNotification, IDisposable where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public ClassBaseMaintenance(IBaseRepository repository)
        {
            this._repository = (IGenericRepository<T>)repository;
        }

        public virtual void Dispose()
        {
            this._repository.Dispose();
        }

        public virtual Task<object> AddAsync(T app)
        {
            return Task.Run<object>(() =>
            {
                this._repository.AddAsync(app).GetAwaiter().GetResult();
                return new Return(ReturnValues.Success, app);
            });
        }
    }
}