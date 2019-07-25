using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapturaVideo.Model
{
    internal class ComboboxItem
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public T GetValue<T>() => (T)this.Value; 
    }
}
