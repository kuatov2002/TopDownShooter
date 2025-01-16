using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInventoryManager>().To<InventoryManager>().FromComponentInHierarchy().AsSingle();

        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();

        Container.Bind<Loot>().FromComponentInHierarchy().AsTransient();

        Container.Bind<InventoryWindow>().FromComponentInHierarchy().AsSingle();
        


    }
}