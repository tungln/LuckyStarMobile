﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuckyStar.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(int index);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
