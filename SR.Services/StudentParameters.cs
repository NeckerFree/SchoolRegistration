namespace SR.Services
{
    public class StudentParameters : QueryStringParameters
    {
        public StudentParameters()
        {
            OrderBy= "FullName";
            
        }
        public uint? MinYearOfBirth { get; set; }
        public uint? MaxYearOfBirth { get; set; } = (uint)DateTime.Now.Year;
        public bool ValidYearRange => (MaxYearOfBirth == null & MinYearOfBirth == null) || (MaxYearOfBirth > MinYearOfBirth);
        public string? Name { get; internal set; } = "";
        public string? JobTitle { get; internal set; } = "";
        public string? OrderBy { get; set; }
    }
}
