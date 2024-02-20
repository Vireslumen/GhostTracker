using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhasmophobiaCompanion.Behaviors
{
    /// <summary>
    ///     Поведение для Xamarin.Forms Entry control, которое конвертирует event Completed в Command.
    /// </summary>
    public class EventToCommandBehavior : Behavior<Entry>
    {
        /// <summary>
        ///     Определение BindableProperty для ICommand.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior));

        /// <summary>
        ///     Свойство Command, которое будет связано с командой в ViewModel.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        ///     Подключение обработчика событий при присоединении поведения к Entry.
        /// </summary>
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Completed += OnCompleted;
        }

        /// <summary>
        ///     Вызов команды при срабатывании события Completed.
        /// </summary>
        private void OnCompleted(object sender, EventArgs e)
        {
            if (Command?.CanExecute(null) ?? false) Command.Execute(null);
        }

        /// <summary>
        ///     Отсоединение обработчика событий при отсоединении поведения от Entry.
        /// </summary>
        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Completed -= OnCompleted;
        }
    }
}