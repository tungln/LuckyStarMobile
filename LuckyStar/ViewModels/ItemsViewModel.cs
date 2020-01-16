using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using LuckyStar.Models;
using LuckyStar.Views;

namespace LuckyStar.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<LuckyItem> LuckyItems { get; set; }
        public ObservableCollection<PercentItem> PercentItems { get; set; }
        public ObservableCollection<CubeItem> CubeItems { get; set; }

        public ObservableCollection<CubeItem> SelectA { get; set; }
        public ObservableCollection<CubeItem> SelectB { get; set; }
        public ObservableCollection<CubeItem> SelectC { get; set; }

        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Lucky";
            LuckyItems = new ObservableCollection<LuckyItem>();
            PercentItems = new ObservableCollection<PercentItem>();
            CubeItems = new ObservableCollection<CubeItem>
            {
                new CubeItem("Bau", "Bầu"),
                new CubeItem("Cua", "Cua"),
                new CubeItem("Tom", "Tôm"),
                new CubeItem("Ca", "Cá"),
                new CubeItem("Ga", "Gà"),
                new CubeItem("Nai", "Nai"),
            };

            SelectA = new ObservableCollection<CubeItem>(CubeItems);
            SelectB = new ObservableCollection<CubeItem>(CubeItems);
            SelectC = new ObservableCollection<CubeItem>(CubeItems);

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ItemsPage, LuckyItem>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as LuckyItem;
                LuckyItems.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            //MessagingCenter.Subscribe<ItemsPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Items.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});

            //MessagingCenter.Subscribe<ItemsPage, CubeModel>(this, "AddCube", async (obj, item) =>
            //{
            //    var newItem = item as CubeModel;
            //    Cubes.Add(newItem);
            //    await DataStore.AddCubeAsync();
            //});
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                LuckyItems.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    LuckyItems.Add(item);
                }

                //Cubes.Clear();
                //var cubes = await DataStore.GetCubesAsync(true);
                //foreach (var cube in cubes)
                //{
                //    Cubes.Add(cube);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}