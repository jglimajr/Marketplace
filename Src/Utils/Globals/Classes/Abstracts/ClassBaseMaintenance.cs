using System;
using System.Threading.Tasks;
using FluentValidator;
using InteliSystem.Utils.Globals.Enumerators;
using InteliSystem.Utils.Globals.Interfaces;

namespace InteliSystem.Utils.Globals.Classes.Abstracts
{
    public abstract class ClassBaseMaintenance<T> : Notifiable, IDisposable where T : class
    {
        private readonly IRepositoryGeneralGuid _repository;

        public ClassBaseMaintenance(IRepositoryGeneralGuid repository)
        {
            this._repository = repository;
        }

        public virtual void Dispose()
        {
            this._repository.Dispose();
        }

        public virtual Task<object> AddAsync(T app)
        {
            return Task.Run<object>(() =>
            {
                this._repository.AddAsync<T>(app).GetAwaiter().GetResult();
                return new Return(ReturnValues.Success, app);
            });
        }
    }
}