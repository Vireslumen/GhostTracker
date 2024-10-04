using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GhostTracker.Models
{
    /// <summary>
    ///     Представляет собой вопрос указывающий насколько призраки подходит под варианты ответа на вопрос.
    /// </summary>
    public class GhostGuessQuestion : INotifyPropertyChanged
    {
        private bool isVisible;
        private int visibility;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                OnPropertyChanged();
            }
        }
        public int Answer { get; set; }
        public int AnswerMeaning { get; set; }
        public int AnswerNegativeMeaning { get; set; }
        public int Id { get; set; }
        public int Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged();
            }
        }
        public List<Ghost> Ghosts { get; set; } = new List<Ghost>();
        public List<int> GhostsId { get; set; }
        public string QuestionText { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Связывает вопрос  - GhostGuessQuestion с призраками Ghost через имеющийся список Id призраков - GhostsId.
        /// </summary>
        /// <param name="allGhosts">Список всех призраков Ghost.</param>
        public void PopulateAssociatedGhosts(List<Ghost> allGhosts)
        {
            foreach (var ghostId in GhostsId)
            {
                var ghost = allGhosts.FirstOrDefault(c => c.Id == ghostId);
                if (ghost != null) Ghosts.Add(ghost);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}