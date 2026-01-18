using UnityEngine;
using Zenject;

namespace DI.Installers
{
    [CreateAssetMenu(fileName = nameof(DataInstaller), menuName = "Installer/" + nameof(DataInstaller))]
    public class DataInstaller : ScriptableObjectInstaller
    {
        [field: SerializeField] public ScriptableObject[] Data { get; private set; }

        public override void InstallBindings()
        {
            foreach (var data in Data)
            {
                Container
                    .Bind(data.GetType())
                    .FromInstance(data)
                    .AsSingle();
            }
        }
    }
}