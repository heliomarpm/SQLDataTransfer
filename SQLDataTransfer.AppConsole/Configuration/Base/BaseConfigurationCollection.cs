using System.Configuration;

namespace SQLDataTransfer.AppConsole.Configuration
{
    public class BaseConfigurationCollection<T> : ConfigurationElementCollection where T : BaseConfigurationElement
    {
        protected override ConfigurationElement CreateNewElement() => System.Activator.CreateInstance<T>();

        protected override object GetElementKey(ConfigurationElement element) => ((T)element).Name;

        public T this[int index] => (T)BaseGet(index);

        public new T this[string name] => (T)BaseGet(name);
    }
}

