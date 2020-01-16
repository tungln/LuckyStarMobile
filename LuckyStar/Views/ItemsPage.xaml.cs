using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LuckyStar.Models;
using LuckyStar.Views;
using LuckyStar.ViewModels;
using System.Collections.ObjectModel;

namespace LuckyStar.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    //[DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        private readonly int luckyRate = 2;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
            CubeA_Picker.SelectedIndex = 0;
            CubeB_Picker.SelectedIndex = 0;
            CubeC_Picker.SelectedIndex = 0;
        }

        //async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        //{
        //    var item = args.SelectedItem as LuckyItem;
        //    if (item == null)
        //        return;

        //    await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

        //    // Manually deselect item.
        //    ItemsListView.SelectedItem = null;
        //}

        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.LuckyItems.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private void RunLucky_Clicked(object sender, EventArgs e)
        {
            LuckyItem item = new LuckyItem(
               ((CubeItem)CubeA_Picker.SelectedItem)?.Id,
               ((CubeItem)CubeB_Picker.SelectedItem)?.Id,
               ((CubeItem)CubeC_Picker.SelectedItem)?.Id
            );

            Predict(item);

            CubeAddItem(item);
        }

        private void CubeAddItem(LuckyItem item)
        {
            MessagingCenter.Send(this, "AddItem", item);

            //if (CubeItems == null)
            //{
            //    CubeItems = new ObservableCollection<CubeModel>();
            //}

            //CubeItems.Add(new CubeModel(SelectItems[0].Selected, SelectItems[1].Selected, SelectItems[2].Selected));
            //ItemsListView.ItemsSource = CubeItems;
        }

        private void Predict(LuckyItem luckyItem)
        {
            var lucky = new Dictionary<string, int>
            {
                { "Bau", 0 },
                { "Cua", 0 },
                { "Tom", 0 },
                { "Ca", 0 },
                { "Ga", 0 },
                { "Nai", 0 }
            };

            var luckyItems = this.viewModel.LuckyItems;
            int sumTotal = 0;
            for (int i = 0; i < luckyItems.Count - 1; i++)
            {
                var item = luckyItems[i];
                var nextItem = luckyItems[i + 1];

                if (this.IsContainFull(luckyItem, item))
                {
                    lucky[nextItem.CubeA] += 1 * luckyRate;
                    lucky[nextItem.CubeB] += 1 * luckyRate;
                    lucky[nextItem.CubeC] += 1 * luckyRate;

                    sumTotal += 3 * luckyRate;
                }

                if (item.IsContain(luckyItem.CubeA))
                {
                    lucky[nextItem.CubeA]++;
                    lucky[nextItem.CubeB]++;
                    lucky[nextItem.CubeC]++;

                    sumTotal += 3;
                }

                if (item.IsContain(luckyItem.CubeB))
                {
                    lucky[nextItem.CubeA]++;
                    lucky[nextItem.CubeB]++;
                    lucky[nextItem.CubeC]++;

                    sumTotal += 3;
                }

                if (item.IsContain(luckyItem.CubeC))
                {
                    lucky[nextItem.CubeA]++;
                    lucky[nextItem.CubeB]++;
                    lucky[nextItem.CubeC]++;

                    sumTotal += 3;
                }
            }

            List<PercentItem> percentItems = new List<PercentItem>();

            foreach (string key in lucky.Keys)
            {
                var percent = lucky[key] > 0 ? (double)lucky[key] / sumTotal : 0;
                percent = Math.Round(percent, 4);

                percentItems.Add(new PercentItem(key.ToString(), percent));
            }

            percentItems.Sort((a, b) =>
            {
                if (a.Percent > b.Percent) return -1;
                else if (a.Percent < b.Percent) return 1;
                else return 0;
            });

            ShowPercent(percentItems);
        }

        private bool IsContainFull(LuckyItem cube, LuckyItem cubeCompare)
        {
            List<string> list = new List<string>
            {
                cube.CubeA,
                cube.CubeB,
                cube.CubeC
            };

            list.Remove(cubeCompare.CubeA);
            list.Remove(cubeCompare.CubeB);
            list.Remove(cubeCompare.CubeC);

            return list.Count == 0;
        }

        private void ShowPercent(List<PercentItem> percentItems)
        {
            Percent_Grid.Children.Clear();
            Percent_Grid.ColumnDefinitions.Clear();

            for (int i = 0; i < percentItems.Count; i++)
            {
                var item = percentItems[i];
                ColumnDefinition gridCol = new ColumnDefinition();
                Percent_Grid.ColumnDefinitions.Add(gridCol);

                Label caption = new Label
                {
                    Text = item.Name,
                    HorizontalTextAlignment = TextAlignment.Center,
                };

                Grid.SetRow(caption, 0);
                Grid.SetColumn(caption, i);

                Label percent = new Label
                {
                    Text = item.Percent.ToString("0 %"),
                    HorizontalTextAlignment = TextAlignment.End,
                    Padding = new Thickness(2, 0),
                    BackgroundColor = i % 2 == 0 ? Color.Aqua : Color.AntiqueWhite
                };

                Grid.SetRow(percent, 1);
                Grid.SetColumn(percent, i);

                Percent_Grid.Children.Add(caption);
                Percent_Grid.Children.Add(percent);
            }
        }
    }
}