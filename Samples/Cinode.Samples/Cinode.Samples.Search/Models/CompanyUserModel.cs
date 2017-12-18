namespace Cinode.Samples.Search.Models
{
    public class CompanyUserModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Fullname => $"{Firstname} {Lastname}";
        public string Title { get; set; }        
    }
}
