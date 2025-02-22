using WheelOfFortune.Stage;
using WheelOfFortune.UserInterface;
using Zenject;

namespace WheelOfFortune.General {

public class SceneInstaller : MonoInstaller
{
        // dependency injection area
        public override void InstallBindings()
        {
            Container.Bind<UIRewardPanel>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UICardPanel>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UICurrency>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIMainMenu>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIStageBar>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIZoneInfoPanel>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIStatistics>().FromComponentInHierarchy().AsSingle(); 

            Container.Bind<ManualStageSystem>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AutomaticStageSystem>().FromComponentInHierarchy().AsSingle();
            Container.Bind<StageManager>().FromComponentInHierarchy().AsSingle();

            Container.Bind<AutomaticStageButton>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ManualStageButton>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CloseButton>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CollectButton>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ReviveButton>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ClearDataButton>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GiveUpButton>().FromComponentInHierarchy().AsSingle(); 
            Container.Bind<SpinButton>().FromComponentInHierarchy().AsSingle();
            Container.Bind<StatisticsButton>().FromComponentInHierarchy().AsSingle();
        }
    }
}
