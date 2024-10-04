using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GhostTracker.Models;
using Serilog;
using Xamarin.Forms;

namespace GhostTracker.ViewModels
{
    /// <summary>
    ///     Базовый класс ViewModel для управления разворачиваемыми элементами. 
    ///     Предоставляет функциональность для переключения состояния раскрытия элементов 
    ///     и ограничения количества одновременно раскрытых элементов.
    /// </summary>
    public abstract class UnfoldingItemsViewModel : BaseViewModel
    {
        private const int MaxExpandedItems = 3;
        protected Queue<UnfoldingItem> expandedItems = new Queue<UnfoldingItem>();

        protected UnfoldingItemsViewModel()
        {
            ToggleExpandCommand = new Command<UnfoldingItem>(ToggleItem);
        }

        public ICommand ToggleExpandCommand { get; protected set; }

        /// <summary>
        ///     Переключает состояние раскрытия элемента и управляет очередью раскрытых элементов.
        ///     Удаляет из очереди элементы, если их количество превышает заданный максимум.
        /// </summary>
        /// <param name="item">Элемент, чьё состояние будет переключено</param>
        protected void ToggleItem(UnfoldingItem item)
        {
            try
            {
                if (item.CanExpand)
                {
                    item.IsExpanded = !item.IsExpanded;
                    if (item.IsExpanded)
                    {
                        expandedItems.Enqueue(item);
                        if (expandedItems.Count > MaxExpandedItems)
                        {
                            var itemToCollapse = expandedItems.Dequeue();
                            itemToCollapse.IsExpanded = false;
                        }
                    }
                    else
                    {
                        var tempItems = new Queue<UnfoldingItem>(expandedItems.Where(x => x.IsExpanded));
                        expandedItems = tempItems;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при обработке разворачивания элемента.");
            }
        }
    }
}