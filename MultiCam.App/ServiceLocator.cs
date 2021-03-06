﻿using MultiCam.Config.Controller;
using MultiCam.Config.Repository;
using MultiCam.Controller;
using MultiCam.DataContext;
using MultiCam.Grid.Controller;
using MultiCam.Info.Controller;
using MultiCam.Notify.Controller;
using MultiCam.Repository;
using ServiceLocatorFramework;

namespace MultiCam
{
    public class ServiceLocator
    {
        public static IServiceLocator Instance { get; private set; }
        public static void MockServices (IServiceLocator service) => Instance = service;
        public static void RegisterServices() {
            Instance = new ServiceLocatorFramework.ServiceLocator();

            //Context
            Instance.Set<IContextDb>().Implements<SqLite>().SingletonScope();

            //Repository
            Instance.Set<IDeviceRepository>().Implements<DeviceRepository>().SingletonScope();
            Instance.Set<IConfigRepository>().Implements<ConfigRepository>().SingletonScope();
            
            //Controllers
            Instance.Set<IAppController>().Implements<AppController>().SingletonScope();
            Instance.Set<IConfigController>().Implements<ConfigController>().SingletonScope();
            Instance.Set<INotifyController>().Implements<NotifyController>().SingletonScope();
            Instance.Set<IAboutController>().Implements<AboutController>().SingletonScope();
            Instance.Set<IGridController>().Implements<GridController>().SingletonScope();
        }
    }
}
