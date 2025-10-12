namespace DevFuckers._Project.CodeBase.Runtime.Features.Stamina
{
    public interface IStaminaSpender
    {
        float StaminaValue { get; }
        void SpendFor(string spendingId);
        void SpendFor(string spendingId, float duration);
        bool CanSpendFor(string spendingId, float duration);
        bool CanSpendFor(string spendingId);
    }
}