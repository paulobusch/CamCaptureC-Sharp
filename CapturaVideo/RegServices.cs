using MultiCam.Info.Controller;
using MultiCam.Config.Controller;
using MultiCam.Config.Repository;
using MultiCam.Controller;
using MultiCam.DataContext;
using MultiCam.Notify.Controller;
using MultiCam.Repository;

using Ninject;
using MultiCam.Grid.Controller;

namespace MultiCam
{
    public static class RegServices
    {
        public static IKernel Ioc;

        /// <summary>
        /// Load your modules or register your services here!
        /// More info: https://pt.stackoverflow.com/questions/54765/ioc-com-ninject
        /// </summary>
        public static void RegisterServices()
        {
            Ioc = new StandardKernel();

            //Context
            Ioc.Bind<IContextDb>().To<SqLite>().InSingletonScope();
            Ioc.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();

            //Controllers
            Ioc.Bind<IAppController>().To<AppController>().InSingletonScope();
            Ioc.Bind<IConfigController>().To<ConfigController>().InSingletonScope();
            Ioc.Bind<INotifyController>().To<NotifyController>().InSingletonScope();
            Ioc.Bind<IAboutController>().To<AboutController>().InSingletonScope();
            Ioc.Bind<IGridController>().To<GridController>().InSingletonScope();

            //Repository
            Ioc.Bind<IDeviceRepository>().To<DeviceRepository>().InSingletonScope();
            Ioc.Bind<IConfigRepository>().To<ConfigRepository>().InSingletonScope();
        }
    }
}
