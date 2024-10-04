namespace GhostTracker.Data
{
    /// <summary>
    ///     Entity framework модель для таблицы GhostGuessQuestionCommonTranslations.
    /// </summary>
    public class GhostGuessQuestionCommonTranslations
    {
        public int ID { get; set; }
        public string AnswerDontKnow { get; set; }
        public string AnswerNo { get; set; }
        public string AnswerThinkSo { get; set; }
        public string AnswerYes { get; set; }
        public string CheckBoxTitle { get; set; }
        public string ChooseAnswer { get; set; }
        public string GhostListTitle { get; set; }
        public string LanguageCode { get; set; }
        public string MeterSecTitle { get; set; }
        public string PageTitle { get; set; }
        public string SpeedTitle { get; set; }
        public string TapButtonTitle { get; set; }
    }
}