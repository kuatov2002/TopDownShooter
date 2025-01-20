using System.Collections.Generic;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject] public List<Zombie> enemies;
    public override void InstallBindings()
    {
        Container.Bind<IInventoryManager>().To<InventoryManager>().AsSingle();
        Container.Bind<ILootCollector>().To<LootCollector>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IInventoryUI>().To<InventoryWindow>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<List<Zombie>>().WithId("Enemies").FromInstance(enemies).AsSingle();
        Container.Bind<SaveManager>().AsSingle();
    }
}