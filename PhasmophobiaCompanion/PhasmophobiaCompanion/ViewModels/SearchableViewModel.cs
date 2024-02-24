using System.Threading;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.ViewModels
{
    /// <summary>
    ///     Предоставляет базовую функциональность для ViewModel с возможностью отложенного поиска.
    /// </summary>
    public abstract class SearchableViewModel : BaseViewModel
    {
        /// <summary>
        ///     Интервал времени (в миллисекундах) для задержки поиска после последнего изменения текста.
        /// </summary>
        private const int SearchInterval = 500;

        private readonly Timer searchTimer;
        protected string searchText;

        public SearchableViewModel()
        {
            searchTimer = new Timer(OnSearchTimerElapsed, null, Timeout.Infinite, Timeout.Infinite);
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
                RestartSearchTimer();
            }
        }

        /// <summary>
        ///     Обработчик события завершения таймера. Запускает поиск в основном потоке.
        /// </summary>
        /// <param name="state">Состояние таймера (не используется).</param>
        private void OnSearchTimerElapsed(object state)
        {
            // Вызов метода поиска на основном потоке
            Device.BeginInvokeOnMainThread(() => { PerformSearch(); });
        }

        /// <summary>
        ///     Абстрактный метод для выполнения поиска. Необходимо реализовать в производных классах.
        /// </summary>
        protected abstract void PerformSearch();

        /// <summary>
        ///     Перезапускает таймер для начала поиска. Таймер сбрасывается при каждом новом вводе.
        /// </summary>
        private void RestartSearchTimer()
        {
            // Сброс и перезапуск таймера каждый раз при изменении текста
            searchTimer.Change(SearchInterval, Timeout.Infinite);
        }
    }
}