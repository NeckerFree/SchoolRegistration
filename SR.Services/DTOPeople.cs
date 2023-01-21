namespace SR.Services
{
    public class DTOStudents
    {
        public int BusinessEntityId { get; internal set; }
        public string? Title { get; internal set; }
        public string? FirstName { get; internal set; }
        public string? LastName { get; internal set; }
        public string? Evaluation { get; internal set; }
        public DateTime BirthDate { get; internal set; }
        public string? JobTitle { get; internal set; }
        public string? FullName { get; internal set; } = null;
    }
}