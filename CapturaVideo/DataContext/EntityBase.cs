using MultiCam.Model.Enums;

namespace MultiCam.DataContext{
    public abstract class EntityBase {
        public int Id { get; set; }
        public EDbState State { get; set; }
    }
}
