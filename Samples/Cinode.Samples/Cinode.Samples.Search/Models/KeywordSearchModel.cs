
using System.Collections.Generic;

namespace Cinode.Samples.Search.Models
{
    public class KeywordSearchModel
    {
        public string Term { get; set; }
        public List<KeywordModel> Keywords { get; set; }
    }
}
