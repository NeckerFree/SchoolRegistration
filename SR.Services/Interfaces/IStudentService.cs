namespace SR.Services.Interfaces
{
    public  interface IStudentService
    {
        Task<IEnumerable<DTOPeople>> GetAllPeople();

        PagedList<DTOPeople> GetPagedPeople(StudentParameters StudentParameters);
    }
    
}
