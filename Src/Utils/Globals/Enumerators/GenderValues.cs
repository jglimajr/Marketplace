using System.ComponentModel;

namespace InteliSystem.Utils.Globals.Enumerators
{
    public enum GenderValues
    {
        [Description("Masculino")] Male = 1,
        [Description("Feminino")] Female,
        [Description("Outros")] Other,
        [Description("Não Informado")] Uninformed
    }
}