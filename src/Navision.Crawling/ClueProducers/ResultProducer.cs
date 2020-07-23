using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;
using CluedIn.Crawling.Navision.Core.Models;
using CluedIn.Crawling.Navision.Vocabularies;

namespace CluedIn.Crawling.Navision.ClueProducers
{
    public class ResultProducer : BaseClueProducer<Result>
    {
        private readonly IClueFactory _factory;

        public ResultProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }
        protected override Clue MakeClueImpl([NotNull] Result input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Unknown, input.Value.ToString(), accountId);

            var data = clue.Data.EntityData;

            if (!string.IsNullOrEmpty(input.Value))
                data.Name = input.Value.ToString();

            var vocab = new ResultVocabulary();

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            data.Properties[vocab.Value] = input.Value.PrintIfAvailable();

            return clue;
        }
    }
}
