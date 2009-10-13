using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ThoughtWorks.TreeSurgeon.Core.Configuration {
	public abstract class ConfigurationElementCollection<T> : ConfigurationElementCollection<T,string> where T: ConfigurationElement, new()
	{

	}

	public abstract class ConfigurationElementCollection<T, KeyT> : ConfigurationElementCollection where T : ConfigurationElement, new() {

		protected abstract KeyT GetElementKey( T element );

		protected override ConfigurationElement CreateNewElement() {
			return new T();
		}

		protected override object GetElementKey( ConfigurationElement element ) {
			return GetElementKey((T)element);
		}

		public T this[int index] {
			get { return (T)BaseGet(index); }
		}

		public override ConfigurationElementCollectionType CollectionType {
			get { return ConfigurationElementCollectionType.BasicMap; }
		}
	}



}
