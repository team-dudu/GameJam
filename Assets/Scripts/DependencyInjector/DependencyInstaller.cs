using Zenject;
using UnityEngine;
using System.Collections;
using GameJam;

public class DependencyInstaller : MonoInstaller
{
    GameObject player;
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("Hello World!");
        Container.Bind<Greeter>().AsSingle().NonLazy();
        Container.Bind<PlayerController>().FromComponentInNewPrefab(player).AsSingle().NonLazy();
        //Container.Bind<IPlayerController>().To<PlayerController>().AsSingle().NonLazy();
    }
}

public class Greeter
{
    public Greeter(string message)
    {
        Debug.Log(message);
    }
}