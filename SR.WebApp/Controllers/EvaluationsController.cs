using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SR.DataAccess;
using SR.Models;
using SR.WebApp.Models;


namespace SR.WebApp.Controllers
{
    public class EvaluationsController : Controller
    {
        private readonly SchoolContext _context;

        public EvaluationsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Evaluations
        //public async Task<IActionResult> Index()
        //{
        //    var schoolContext = _context.Evaluations.Include(e => e.CourseStudent);
        //    //NewEvaluation newEvaluation= new NewEvaluation();
        //    //newEvaluation.Evaluation = await schoolContext.ToListAsync();
        //    //return View(newEvaluation);
        //    return View(await schoolContext.ToListAsync());
        //}
        public async Task<IActionResult> Index(Guid id, int selectedStart = 0)
        {
            IQueryable<DTOEvaluation> evaluationsByCourse = GetEvaluations(id, selectedStart);
            ModelEvaluation modelEvaluations = new ModelEvaluation()
            {
                IdCourse = id,
                Evaluations = evaluationsByCourse,

            };

            if (evaluationsByCourse != null)
            {
                modelEvaluations.Evaluations = await evaluationsByCourse.ToListAsync();
                return View(modelEvaluations);
            }
            return Problem("'Evaluations By Course'  is null.");
        }

        private IQueryable<DTOEvaluation> GetEvaluations(Guid id, int selectedStart)
        {
            IQueryable<DTOEvaluation> evaluationsByCourse = (from ev in _context.Evaluations
                                                             join cs in _context.CourseStudents on ev.CourseStudentId equals cs.Id
                                                             join c in _context.Courses on cs.CourseId equals c.Id
                                                             join s in _context.Students on cs.StudentId equals s.Id
                                                             orderby ev.CreationDate 
                                                             select new DTOEvaluation
                                                             {
                                                                 Id = id,
                                                                 CourseId=c.Id,
                                                                 Name = c.Name,
                                                                 StudentName=s.FullName,
                                                                 Active = c.Active,
                                                                 CreationDate = ev.CreationDate,
                                                                 Description = ev.Description,
                                                                 Grade = cs.Grade,
                                                                 Stars = ev.Stars
                                                             });
            if (id != Guid.Empty)
            {
                evaluationsByCourse = (from ec in evaluationsByCourse
                                       where ec.CourseId == id
                                       select ec);
            }
            if (selectedStart > 0)
            {
                evaluationsByCourse = (from ec in evaluationsByCourse
                                       where ec.Stars == selectedStart
                                       select ec);
            }
            return evaluationsByCourse;
        }

        // GET: DTOEvaluations/Filter/5
        public ActionResult Filter()
        {
            FilterData filterData = new FilterData
            {
                IdCourse = Guid.Parse(Request.Form["IdCourse"].ToString()),
                SelectedStar = int.Parse(Request.Form["SelectedStar"].ToString())
            };
            return RedirectToAction("Index", new { id = filterData.IdCourse, selectedStart = filterData.SelectedStar });

        }

        // GET: Evaluations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Evaluations == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations
                .Include(e => e.CourseStudent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluation == null)
            {
                return NotFound();
            }

            return View(evaluation);
        }

        // GET: Evaluations/Create
        public IActionResult Create()
        {
            //List<SelectListItem> items = new List<SelectListItem>();

            //items.Add(new SelectListItem { Text = "Action", Value = "0" });

            //items.Add(new SelectListItem { Text = "Drama", Value = "1" });

            //items.Add(new SelectListItem { Text = "Comedy", Value = "2", Selected = true });

            //items.Add(new SelectListItem { Text = "Science Fiction", Value = "3" });

            //ViewBag.MovieType = items;
            //var data = (from cs in _context.CourseStudents
            //            join c in _context.Courses on cs.CourseId equals c.Id
            //            select new { c, cs }
            //          );
            //ViewBag.CourseStudents = new SelectList(data, "Id", "Name");
            ViewData["Courses"] = new SelectList(_context.Courses, "Id", "Name");
                     
            ViewData["Students"] = new SelectList(_context.Students, "Id", "FullName");
            return View();
        }

        // POST: Evaluations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId, StudentId,Stars,Description,CreationDate")] NewEvaluation newEvaluation)
        {
            if (ModelState.IsValid)
            {
                Evaluation evaluation = newEvaluation;
                evaluation.Id = Guid.NewGuid();

                var courseStudentId = (from cs in _context.CourseStudents 
                                             where cs.CourseId==newEvaluation.CourseId && cs.StudentId==newEvaluation.StudentId
                                             select cs.Id).FirstOrDefault();
                evaluation.CourseStudentId = courseStudentId;
                if (courseStudentId == Guid.Empty)
                {
                    CourseStudent courseStudent = new CourseStudent
                    {
                        CourseId = newEvaluation.CourseId,
                        StudentId = newEvaluation.StudentId,
                        Id = Guid.NewGuid()
                    };
                    _context.CourseStudents.Add(courseStudent);
                    _context.SaveChanges();
                    evaluation.CourseStudentId = courseStudent.Id;
               }
                _context.Add(evaluation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseStudentId"] = new SelectList(_context.CourseStudents, "Id", "Id", newEvaluation.CourseStudentId);
            return View(newEvaluation);
        }

        // GET: Evaluations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Evaluations == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }
            ViewData["CourseStudentId"] = new SelectList(_context.CourseStudents, "Id", "Id", evaluation.CourseStudentId);
            return View(evaluation);
        }

        // POST: Evaluations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CourseStudentId,Stars,Description,CreationDate")] Evaluation evaluation)
        {
            if (id != evaluation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluationExists(evaluation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseStudentId"] = new SelectList(_context.CourseStudents, "Id", "Id", evaluation.CourseStudentId);
            return View(evaluation);
        }

        // GET: Evaluations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Evaluations == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations
                .Include(e => e.CourseStudent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluation == null)
            {
                return NotFound();
            }

            return View(evaluation);
        }

        // POST: Evaluations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Evaluations == null)
            {
                return Problem("Entity set 'SchoolContext.Evaluations'  is null.");
            }
            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation != null)
            {
                _context.Evaluations.Remove(evaluation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluationExists(Guid id)
        {
          return (_context.Evaluations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
