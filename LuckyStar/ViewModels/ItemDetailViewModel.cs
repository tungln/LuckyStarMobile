using System;

using LuckyStar.Models;

namespace LuckyStar.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public LuckyItem Item { get; set; }
        public ItemDetailViewModel(LuckyItem item = null)
        {
            //Title = item?.Text;
            //Item = item;
        }
    }
}
