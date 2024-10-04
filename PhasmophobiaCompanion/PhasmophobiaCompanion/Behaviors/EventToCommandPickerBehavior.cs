using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GhostTracker.Behaviors
{
    /// <summary>
    ///     Поведение для Xamarin.Forms Picker control, которое конвертирует event SelectedIndexChanged в Command.
    /// </summary>
    public class EventToCommandPickerBehavior : Behavior<Picker>
    {
        /// <summary>
        ///     Определение BindableProperty для ICommand.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandPickerBehavior));

        /// <summary>
        ///     Флаг инициализирован ли уже был Picker
        /// </summary>
        private bool initialized;

        /// <summary>
        ///     Свойство Command, которое будет связано с командой в ViewModel.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        ///     Подключение обработчика событий при присоединении поведения к Picker.
        /// </summary>
        protected override void OnAttachedTo(Picker bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.SelectedIndexChanged += OnPickerSelectedIndexChanged;
        }

        /// <summary>
        ///     Отсоединение обработчика событий при отсоединении поведения от Picker.
        /// </summary>
        protected override void OnDetachingFrom(Picker bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.SelectedIndexChanged -= OnPickerSelectedIndexChanged;
        }

        /// <summary>
        ///     Вызов команды при срабатывании события SelectedIndexChanged.
        /// </summary>
        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is Picker picker)
            {
                if (!initialized)
                {
                    initialized = true;
                    return;
                }

                if (picker.SelectedItem != null && Command != null && Command.CanExecute(picker.SelectedItem))
                    Command.Execute(picker.SelectedItem);
            }
        }
    }
}