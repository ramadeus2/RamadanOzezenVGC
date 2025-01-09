using UnityEngine;
using Zenject;
namespace WheelOfFortune.General {
    [CreateAssetMenu(fileName = "ScriptableObjInstaller", menuName = "Installers/ScriptableObjInstaller")]
    public class ScriptableObjInstaller: ScriptableObjectInstaller<ScriptableObjInstaller> {
        public GameSettings gameSettings;
        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings).AsSingle();
        }
    }
}