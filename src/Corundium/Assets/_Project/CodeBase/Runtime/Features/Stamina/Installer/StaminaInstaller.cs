using DevFuckers._Project.CodeBase.Runtime.Features.Stamina.Data;
using Zenject;

namespace DevFuckers._Project.CodeBase.Runtime.Features.Stamina.Installer
{
    public class StaminaInstaller : MonoInstaller
    {
        public float InitialValue = 100;
        public float RefillSpeed = 20;
        public string ConfigPath = StaminaSpendingConfig.Name;

        public override void InstallBindings()
        {
            var model = new Stamina(InitialValue, InitialValue);
            Container.Bind<Stamina>().FromInstance(model).AsSingle();
            
            Container.Bind<StaminaSpendingConfigProvider>()
                .AsSingle()
                .WithArguments(ConfigPath);

            // 3. Логика расхода
            Container.Bind<IStaminaSpender>().To<StaminaSpender>().AsSingle();

            // 4. Логика восстановления (автоматически вызывается через ITickable)
            Container.BindInterfacesTo<StaminaRefiller>()
                .AsSingle()
                .WithArguments(RefillSpeed);
        }
    }
}