namespace DiscogsConnect
{
    public class SearchCriteria
    {
        public string Query { get; set; }
        public ResourceType Type { get; set; }
        public string Title { get; set; }
        public string ReleaseTitle { get; set; }
        public string Credit { get; set; }
        public string Artist { get; set; }
        public string Anv { get; set; }
        public string Label { get; set; }
        public string Genre { get; set; }
        public string Style { get; set; }
        public string Country { get; set; }
        public string Year { get; set; }
        public string Format { get; set; }
        public string Catno { get; set; }
        public string Barcode { get; set; }
        public string Track { get; set; }
        public string Submitter { get; set; }
        public string Contributor { get; set; }
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 100;
    }
}