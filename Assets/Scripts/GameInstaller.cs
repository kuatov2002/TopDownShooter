using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInventoryManager>().To<InventoryManager>().AsSingle();
        Container.Bind<ILootCollector>().To<LootCollector>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IInventoryUI>().To<InventoryWindow>().FromComponentInHierarchy().AsSingle();
    }
}