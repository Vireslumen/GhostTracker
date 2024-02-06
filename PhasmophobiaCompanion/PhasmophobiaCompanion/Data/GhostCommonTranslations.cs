using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhasmophobiaCompanion.Data
{
    public class GhostCommonTranslations
    {
        [Key]
        public int ID {  get; set; }
        public string LanguageCode { get; set; }
        public string GhostsTitle { get; set; }
        public string Search {  get; set; }
        public string FilterTitle { get; set; }
        public string ApplyTitle { get; set; }
        public string MaxSpeed { get; set; }
        public string MaxSpeedLoS { get; set; }
        public string MinSpeed { get; set; }
        public string Speed { get; set; }
        public string SanityHunt { get; set; }
        public string MinSanityHunt { get; set; }
        public string MaxSanityHunt { get; set; }
        public string GhostTitle { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string LoS { get; set; }
    }
}
