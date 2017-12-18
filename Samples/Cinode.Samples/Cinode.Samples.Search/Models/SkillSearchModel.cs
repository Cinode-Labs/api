using System.Collections.Generic;

namespace Cinode.Samples.Search.Models
{
    public class SkillSearchModel
    {
        public string Term { get; set; }
        public List<CompanyUserModel> CompanyUsers { get; set; }
    }
}
