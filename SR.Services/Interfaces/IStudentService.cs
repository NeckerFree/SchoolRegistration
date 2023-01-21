namespace SR.Services.Interfaces
{
    public  interface IStudentService
    {
        Task<IEnumerable<DTOStudents>> GetAllStudents();

        PagedList<DTOStudents> GetPagedStudents(StudentParameters StudentParameters);
    }
    
}
