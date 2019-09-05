using Zenject;
using UnityEngine;
using System.Collections;
using GameJam;

public class DependencyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("Hello World!");
        Container.Bind<Greeter>().AsSingle().NonLazy();
        Container.Bind<Character>().AsTransient().NonLazy();
        Container.Bind<IPlayerController>().AsSingle().NonLazy();
    }
}

public class Greeter
{
    public Greeter(string message)
    {
        Debug.Log(message);
    }
}