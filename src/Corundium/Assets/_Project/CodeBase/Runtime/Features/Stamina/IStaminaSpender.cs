namespace DevFuckers._Project.CodeBase.Runtime.Features.Stamina
{
    public interface IStaminaSpender
    {
        float StaminaValue { get; }
        void SpendFor(ESpendindStaminaType spendingType);
        void SpendFor(ESpendindStaminaType spendingType, float duration);
        bool CanSpendFor(ESpendindStaminaType spendingType, float duration);
        bool CanSpendFor(ESpendindStaminaType spendingType);
    }
}