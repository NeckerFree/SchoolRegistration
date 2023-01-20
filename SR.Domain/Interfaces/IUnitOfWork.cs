﻿namespace SR.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //IBusinessEntityContactRepository BusinessEntityContacts { get; }
        //IContactTypeRepository ContactTypes { get; }
        IStudentRepository Students { get; }
        IEvaluationRepository Evaluationes { get; }
        ICourseRepository Courses { get; }
        int Complete();
    }
}
