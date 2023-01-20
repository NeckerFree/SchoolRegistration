//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SR.DataAccess;
//using SR.WebApp.Models;


//namespace SR.WebApp.Controllers
//{
//    public class DTOEvaluationsController : Controller
//    {
//        private readonly SchoolContext _context;

//        public DTOEvaluationsController(SchoolContext context)
//        {
//            _context = context;
//        }

//        // GET: DTOEvaluations
//        public async Task<IActionResult> Index(Guid id, int selectedStart = 0)
//        {
//            IQueryable<DTOEvaluation> evaluationsByCourse = GetEvaluations(id, selectedStart);
//            ModelEvaluation modelEvaluations = new ModelEvaluation()
//            {
//                IdCourse = id,
//                Evaluations = evaluationsByCourse,

//            };

//            if (evaluationsByCourse != null)
//            {
//                modelEvaluations.Evaluations = await evaluationsByCourse.ToListAsync();
//                return View(modelEvaluations);
//            }
//            return Problem("'Evaluations By Course'  is null.");
//        }

//        private IQueryable<DTOEvaluation> GetEvaluations(Guid id, int selectedStart)
//        {
//            IQueryable<DTOEvaluation> evaluationsByCourse = (from ev in _context.Evaluations
//                                                             join cs in _context.CourseStudents on ev.CourseStudentId equals cs.Id
//                                                             join c in _context.Courses on cs.CourseId equals c.Id
//                                                             where c.Id == id
//                                                             select new DTOEvaluation
//                                                             {
//                                                                 Id = id,
//                                                                 Name = c.Name,
//                                                                 Active = c.Active,
//                                                                 CreationDate = ev.CreationDate,
//                                                                 Description = ev.Description,
//                                                                 Grade = cs.Grade,
//                                                                 Stars = ev.Stars
//                                                             });
//            if (selectedStart > 0)
//            {
//                evaluationsByCourse = (from ec in evaluationsByCourse
//                                       where ec.Stars == selectedStart
//                                       select ec);
//            }
//            return evaluationsByCourse;
//        }

//        // GET: DTOEvaluations/Filter/5
//        public ActionResult Filter()
//        {
//            FilterData filterData = new FilterData
//            {
//                IdCourse = Guid.Parse(Request.Form["IdCourse"].ToString()),
//                SelectedStar = int.Parse(Request.Form["SelectedStar"].ToString())
//            };
//            return RedirectToAction("Index", new  {id= filterData.IdCourse, selectedStart= filterData.SelectedStar });

//        }

//        private bool DTOEvaluationExists(Guid id)
//        {
//            return (_context.DTOEvaluation?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
