using System;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Enumerators;
using InteliSystem.Utils.Globals.Functions;

namespace InteliSystem.InteliMarketPlace.Entities.Manufactures;
public class Manufacture : ClassBaseGuid
{
    public Manufacture()
        : base()
    {
        LoadProperties("", "", "");
    }

    public Manufacture(string cnpj, string name, string socialReason)
    {
        LoadProperties(cnpj, name, socialReason);
    }

    public Manufacture(Guid id, string cnpj, string name, string socialReason, StatusValues status = StatusValues.Active)
        : base(id, status)
    {
        LoadProperties(cnpj, name, socialReason);
    }

    public string Cnpj { get; private set; }
    public string Name { get; private set; }
    public string SocialReason { get; private set; }

    private void Validate()
    {
        if (!Validations.IsCnpj(this.Cnpj))
            this.AddNotification("Cnpj", "Cnpj inválido ou não informado");
        if (SocialReason.IsEmpty())
            this.AddNotification("SocialReason", "Razão Social não informada");
    }

    private void LoadProperties(string cnpj, string name, string socialReason)
    {
        Cnpj = cnpj;
        Name = name;
        SocialReason = socialReason;
        Validate();
    }

}