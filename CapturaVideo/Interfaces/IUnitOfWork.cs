using MultiCam.Config.Repository;
using MultiCam.Repository;

namespace MultiCam
{
    public interface IUnitOfWork
    {
        DeviceRepository DeviceRepository { get; }
        ConfigRepository ConfigRepository { get; }

        void Commit();
    }
}
