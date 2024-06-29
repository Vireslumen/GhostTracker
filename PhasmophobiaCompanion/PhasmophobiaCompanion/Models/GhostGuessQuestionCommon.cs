namespace PhasmophobiaCompanion.Models
{
    /// <summary>
    ///     Класс GhostGuessQuestionCommon содержит атрибуты для локализации текстовых элементов интерфейса,
    ///     относящихся к определению призрака - GhostGuessQuestion.
    /// </summary>
    public class GhostGuessQuestionCommon
    {
        /// <summary>
        ///     Текст не знающего ответа на вопрос в интерфейсе. Пример: "Don't know", "Не знаю".
        /// </summary>
        public string AnswerDontKnow { get; set; }
        /// <summary>
        ///     Текст отрицательного ответа на вопрос в интерфейсе. Пример: "Yes", "Да".
        /// </summary>
        public string AnswerNo { get; set; }
        /// <summary>
        ///     Текст не решительно-положительного ответа на вопрос в интерфейсе. Пример: "Think so", "Вроде да".
        /// </summary>
        public string AnswerThinkSo { get; set; }
        /// <summary>
        ///     Текст положительного ответа на вопрос в интерфейсе. Пример: "Yes", "Да".
        /// </summary>
        public string AnswerYes { get; set; }
        /// <summary>
        ///     Название чекбокса для определения учитывать ли скорость призрака в расчетах. Пример: "Consider Ghost Speed",
        ///     "Учитывать скорость призрака".
        /// </summary>
        public string CheckBoxTitle { get; set; }
        /// <summary>
        ///     Название поля говорящего о необходимости выбора ответа. Пример: "Choose an answer:", "Выберите ответ:".
        /// </summary>
        public string ChooseAnswer { get; set; }
        /// <summary>
        ///     Название списка предполагаемых призраков в интерфейсе. Пример: "A list of supposed ghosts:", "Список предполагаемых
        ///     призраков:".
        /// </summary>
        public string GhostListTitle { get; set; }
        /// <summary>
        ///     Текст единицы измерения у поля выдаваемой скорости призрака в интерфейсе. Пример: "meter/sec", "м/с".
        /// </summary>
        public string MeterSecTitle { get; set; }
        /// <summary>
        ///     Название страницы определения призрака в интерфейсе. Пример: "Guess the Ghost", "Определи призрака".
        /// </summary>
        public string PageTitle { get; set; }
        /// <summary>
        ///     Текст поля выдаваемой скорости призрака в интерфейсе. Пример: "Ghost Speed:", "Скорость призрака".
        /// </summary>
        public string SpeedTitle { get; set; }
        /// <summary>
        ///     Название кнопки для измерения скорость призрака в интерфейсе. Пример: "Tap every ghost step", "Нажимай каждый шаг
        ///     призрака".
        /// </summary>
        public string TapButtonTitle { get; set; }
    }
}