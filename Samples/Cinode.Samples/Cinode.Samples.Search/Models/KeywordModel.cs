
using System.Collections.Generic;

namespace Cinode.Samples.Search.Models
{
    public class KeywordModel
    {
        public int Id { get; set; }
        public int KeywordType { get; set; }
        public int? MasterSynonymId { get; set; }
        public string MasterSynonym { get; set; }
        public List<string> Synonyms { get; set; }

        public bool Universal { get; set; }
    }
}
