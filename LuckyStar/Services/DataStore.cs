using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LuckyStar.Models;

namespace LuckyStar.Services
{
    public class DataStore : IDataStore<LuckyItem>
    {
        readonly List<LuckyItem> items;

        public DataStore()
        {
            items = new List<LuckyItem>();
            {
                //new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description." }
                //new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description = "This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description = "This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description = "This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description = "This is an item description." },
                //new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description = "This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(LuckyItem item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(LuckyItem item)
        {
            //var oldItem = items.Where((LuckyItem arg) => arg.Id == item.Id).FirstOrDefault();
            //items.Remove(oldItem);
            //items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            //var oldItem = items.Where((LuckyItem arg) => arg.Id == id).FirstOrDefault();
            //items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<LuckyItem> GetItemAsync(int index)
        {
            return await Task.FromResult(items[index]);
        }

        public async Task<IEnumerable<LuckyItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}