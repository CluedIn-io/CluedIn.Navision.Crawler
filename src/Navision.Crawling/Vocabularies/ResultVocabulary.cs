using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.Navision.Vocabularies
{
    public class ResultVocabulary:SimpleVocabulary
    {
        public ResultVocabulary()
        {
            VocabularyName = "Navision Result";
            KeyPrefix = "navision.result";
            KeySeparator = ".";
            Grouping = EntityType.Unknown;

            AddGroup("Navision Result Details", group =>
            {
                Value = group.Add(new VocabularyKey("Value", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
            });
        }
        public VocabularyKey Value { get; internal set; }
    }
}
