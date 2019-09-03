using System;

namespace MultiCam.Model.Dtos {
    public class CboItemDto {
        public string Name { get; set; }
        public object Value { get; set; }
        public T GetValue<T>() => (T)this.Value;
    }
}
