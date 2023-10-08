using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Flashopedia.Services
{
	public interface IDataStore<T>
	{
		bool AddItem(T item);
		bool UpdateItem(T item);
		bool DeleteItem(Guid id);
		T GetItem(Guid id);
		ObservableCollection<T> GetItems();
	}
}
