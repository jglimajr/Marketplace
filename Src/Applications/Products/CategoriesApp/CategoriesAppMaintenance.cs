
using InteliSystem.InteliMarketPlace.Domains.Products;
using InteliSystem.InteliMarketPlace.Repositories;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Classes.Abstracts;
using InteliSystem.Utils.Globals.Enumerators;

namespace InteliSystem.InteliMarketPlace.Applications.CategoriesApp;

public class CategoriesAppMaintenance : ClassBaseMaintenance<CategoryApp>
{
    private readonly ICategoriesRepository _repository;
    public CategoriesAppMaintenance(ICategoriesRepository repository)
        : base(repository) => this._repository = repository;



    public Task<Return> AddAsync(CategoryApp categoryApp)
    {
        return Task.Run<Return>(() =>
        {
            if (categoryApp.IsNull())
            {
                this.AddNotification("Category", "Category not informed");
                return new Return(ReturnValues.Failed, this.GetAllNotifications);
            }

            var category = new Category(categoryApp.Name, categoryApp.Description);

            if (category.ExistNotifications)
            {
                this.AddNotifications(category.GetAllNotifications);
                return new Return(ReturnValues.Failed, this.GetAllNotifications);
            }

            var retaux = this._repository.AddAsync(category).GetAwaiter().GetResult();

            if (retaux <= 0)
            {
                this.AddNotification("Category", "Category not informed");
                return new Return(ReturnValues.Failed, this.GetAllNotifications);
            }

            return new Return(ReturnValues.Success, new CategoryApp().Load(category));
        });
    }
    public Task<Return> UpdateAsync(string id, CategoryApp categoryApp)
    {
        return Task.Run<Return>(() =>
        {
            if (categoryApp.IsNull())
            {
                this.AddNotification("Category", "Category not informed");
                return new Return(ReturnValues.Failed, this.GetAllNotifications);
            }
            if (id != categoryApp.Id)
            {
                this.AddNotification("Category", "Category not informed");
                return new Return(ReturnValues.Failed, this.GetAllNotifications);
            }

            var category = new Category(categoryApp.Id, categoryApp.Name, categoryApp.Description);

            if (category.ExistNotifications)
            {
                this.AddNotifications(category.GetAllNotifications);
                return new Return(ReturnValues.Failed, this.GetAllNotifications);
            }

            var retaux = this._repository.UpdateAsync(category).GetAwaiter().GetResult();

            if (retaux <= 0)
            {
                this.AddNotification("Category", "Category not informed");
                return new Return(ReturnValues.Failed, this.GetAllNotifications);
            }

            return new Return(ReturnValues.Success, new CategoryApp().Load(category));
        });
    }
    public Task<Return> DeleteAsync(string id, CategoryApp categoryApp)
    {
        return Task.Run<Return>(() =>
        {
            if (categoryApp.IsNull())
            {
                this.AddNotification("Category", "Category not informed");
                return new Return(ReturnValues.Failed, this.GetAllNotifications);
            }
            if (id != categoryApp.Id)
            {
                this.AddNotification("Category", "Category not informed");
                return new Return(ReturnValues.Failed, this.GetAllNotifications);
            }

            var category = new Category(categoryApp.Id, categoryApp.Name, categoryApp.Description);

            var retaux = this._repository.DeleteAsync(category).GetAwaiter().GetResult();

            if (retaux <= 0)
            {
                this.AddNotification("Category", "Category not informed");
                return new Return(ReturnValues.Failed, this.GetAllNotifications);
            }

            return new Return(ReturnValues.Success, null);
        });
    }

    public Task<Return> GetAllAsync()
    {
        return Task.Run<Return>(() =>
        {
            var retaux = this._repository.GetAllAsync().GetAwaiter().GetResult();
            var categories = new List<CategoryApp>();

            retaux.ToList().ForEach(category =>
            {
                categories.Add(new CategoryApp().Load(category));
            });

            return new Return(ReturnValues.Success, categories);
        });
    }

    public Task<Return> GetAsync(string id)
    {
        return Task.Run<Return>(() =>
        {
            var retaux = this._repository.GetAsync(new Category(id, "", "")).GetAwaiter().GetResult();

            if (retaux.IsNull())
            {
                this.AddNotification("Category", "Category not found");
                return new Return(ReturnValues.Failed, this.GetAllNotifications);
            }

            return new Return(ReturnValues.Success, new CategoryApp().Load(retaux));
        });
    }
}