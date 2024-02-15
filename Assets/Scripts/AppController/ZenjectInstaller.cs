using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TableGenerator>().AsSingle();
        Container.Bind<AppControllerStorage>().AsSingle();

        //Container.Bind<IStorage>().To<BinnaryStorage>().AsSingle();
        Container.Bind<IStorage>().To<JSONStorage>().AsSingle();
        //Container.Bind<IStorage>().To<PlayerPrefsStorage>().AsSingle();
    }
}
