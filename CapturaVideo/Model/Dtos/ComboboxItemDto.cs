namespace CapturaVideo.Model.Dtos {
    public class ComboboxItemDto {
        public string Name { get; set; }
        public object Value { get; set; }
        public T GetValue<T>() => (T)this.Value; 
    }
}
