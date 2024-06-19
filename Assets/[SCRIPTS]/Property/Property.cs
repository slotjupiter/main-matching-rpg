using System;
using System.Collections.Generic;

namespace slotJupiter
{
    public class Property
    {
        private Dictionary<Enum, object> properties = new();

        public void Add<T>(Enum propertyKey, T propertyValue)
        {
            properties[propertyKey] = new PropertyValue<T>(propertyValue);
        }

        public void Remove(Enum propertyKey)
        {
            properties.Remove(propertyKey);
        }

        public PropertyValue<T> Get<T>(Enum propertyKey)
        {
            if (properties.TryGetValue(propertyKey, out object property) && property is PropertyValue<T> typedProperty)
                return typedProperty;
            else
                return default;
        }
    }

    public class PropertyValue<T>
    {
        private T _currentValue;

        public Action<T, PropertyValue<T>> OnChanged { get; set; }

        public T Value
        {
            get => _currentValue;
            set
            {
                var oldValue = _currentValue;
                _currentValue = value;
                OnChanged?.Invoke(oldValue, this);
            }
        }

        public PropertyValue(T value)
        {
            _currentValue = value;
        }
    }
}
