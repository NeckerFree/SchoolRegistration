using SR.Domain.Interfaces;
using SR.Services.Interfaces;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace SR.Services
{
    public class StudentService : IStudentService
    {
        public IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<DTOStudents>> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        public PagedList<DTOStudents> GetPagedStudents(StudentParameters StudentParameters)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<DTOStudents>> GetAllStudents()
        //{
        //    var Students = await _unitOfWork.Students.GetAll();
        //    var Evaluation = await _unitOfWork.Evaluationes.GetAll();
        //    var Courses = await _unitOfWork.Courses.GetAll();
        //    var result = (from p in Students
        //                  join em in Courses on p.BusinessEntityId equals em.BusinessEntityId
        //                  join e in Evaluation on p.BusinessEntityId equals e.BusinessEntityId
        //                  select new DTOStudents
        //                  {
        //                      BusinessEntityId = p.BusinessEntityId,
        //                      Title = p.Title,
        //                      FirstName = p.FirstName,
        //                      LastName = p.LastName,
        //                      Evaluation = e.Evaluation1,
        //                      BirthDate = em.BirthDate,
        //                      JobTitle = em.JobTitle,
        //                      FullName = $"{p.FirstName} {p.LastName}"
        //                  }
        //                  );
        //    return result;
        //}



        //public PagedList<DTOStudents> GetPagedStudents(StudentParameters StudentParameters)
        //{
        //    var allStudents = GetAllStudents().Result.AsQueryable<DTOStudents>();

        //    if (StudentParameters.MinYearOfBirth != null && StudentParameters.MaxYearOfBirth != null)
        //    {
        //        allStudents = allStudents.Where(o => o.BirthDate.Year >= StudentParameters.MinYearOfBirth && o.BirthDate.Year <= StudentParameters.MaxYearOfBirth);
        //    }
        //    SearchStudents(ref allStudents, StudentParameters.Name, StudentParameters.JobTitle);
        //    OrderStudents(ref allStudents, StudentParameters.OrderBy);
        //    return PagedList<DTOStudents>.ToPagedList(allStudents, StudentParameters.PageNumber, StudentParameters.PageSize);
        //}

        private void OrderStudents(ref IQueryable<DTOStudents> allStudents, string? orderBy)
        {
            if (!allStudents.Any())
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                allStudents.OrderBy(p => p.FullName);
                return;
            }
            var orderParameters = orderBy.Trim().Split(',');
            var propertyInfos = typeof(DTOStudents).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var orderParam in orderParameters)
            {
                if (string.IsNullOrWhiteSpace(orderParam)) continue;
                var propertyQuery = orderParam.Trim().Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyQuery, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null) continue;
                var sortingOrder = orderParam.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            allStudents = allStudents.OrderBy(orderQuery);
            return;
        }


        private void SearchStudents(ref IQueryable<DTOStudents> allStudents, string? name, string? jobTitle)
        {
            if (allStudents.Any() == false) return;
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(jobTitle)) return;
            allStudents = ((IQueryable<DTOStudents>)(
                from ap in allStudents
                where (
                (string.IsNullOrWhiteSpace(ap.FullName) || string.IsNullOrWhiteSpace(name) || ap.FullName.ToLower().Contains(name.Trim().ToLower())) &&
                (string.IsNullOrWhiteSpace(ap.JobTitle) || string.IsNullOrWhiteSpace(jobTitle) || ap.JobTitle.ToLower().Contains(jobTitle.Trim().ToLower())))
                select ap));

        }
    }
}
