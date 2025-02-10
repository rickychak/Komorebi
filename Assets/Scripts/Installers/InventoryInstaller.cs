using Komorebi.Debug.Equipment;
using UnityEngine;
using Zenject;

namespace Komorebi.Product
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private InventoryManager inventoryManager;

        public override void InstallBindings()
        {
            Container.Bind<IInventorySystem>()
                .To<InventoryManager>()
                .FromInstance(inventoryManager)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<InventoryDebugInfo>()
                .AsSingle()
                .NonLazy();
        }
    }
} 