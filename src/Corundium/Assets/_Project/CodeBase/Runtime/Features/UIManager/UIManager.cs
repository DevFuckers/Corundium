namespace DevFuckers._Project.CodeBase.Runtime.Features.UIManager
{
    public enum UIState
    {
        Normal,
        QuickMenuOpen,
        SettingsPanelOpen,
        WinUIShowing,
        Shop,
        DefeatUIShowing
    }

    public class UIManager
    {
        private UIState _currentState = UIState.Normal;

        public UIState CurrentState => _currentState;

        public void SetState(UIState newState)
        {
            _currentState = newState;
        }

        public bool CanOpenQuickMenu() 
            => _currentState == UIState.Normal;
    }
}