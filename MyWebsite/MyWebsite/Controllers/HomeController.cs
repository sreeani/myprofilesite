using MyWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyWebsite.Controllers
{

    [Authorize(Users = "sreeayyala123@gmail.com")]
    [RequireHttps]
    public class HomeController : BaseController
    {
       [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page1.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact Page.";

            using (var ctx = new StudentEntities())
            {
                var isUserAuthenticatedAndAnAdmin = ctx.People.Any(p => p.Email == User.Identity.Name
                                                    && p.IsAdmin && User.Identity.IsAuthenticated);
                ViewBag.IsAdmin = isUserAuthenticatedAndAnAdmin;
            }

            return View();
        }
        [AllowAnonymous]
        public ActionResult MyDetails()
        {
            using (var ctx = new StudentEntities())
            {
                var user = ctx.People.FirstOrDefault(p => p.Email == User.Identity.Name);

                var model = new UserViewModel()
                {
                    UserID = Properties.Settings.Default.DefaultUserID,
                    Email = User.Identity.Name,
                    IsAdmin = User.Identity.IsAuthenticated && user!=null && user.IsAdmin
                };
                return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult EducationDetails()
        {
            using (var ctx = new StudentEntities())
            {
                var isAdmin = ctx.People.Any(p => p.Email == User.Identity.Name && User.Identity.IsAuthenticated && p.IsAdmin);
                var userID = Properties.Settings.Default.DefaultUserID;
                var model = ctx.Educations.Where(e => e.PersonID == userID)
                                        .Select(s=> new EducationViewModel()
                                        {
                                            EducationID = s.EducationID,
                                            DegreeName = s.DegreeName,
                                            SpecializedConcentrations = s.SpecializedConcentrations,
                                            PersonID = s.PersonID,
                                            Major = s.Major,
                                            GraduationDate = s.GraduationDate,
                                            InstitutionName = s.InstitutionName,
                                            Percentage = s.Percentage,
                                            IsAdmin = isAdmin
                                        }).ToList();

                return PartialView("_EducationDetails",model);
            }
        }

        public ActionResult EducationCreate()
        {
            var model = new EducationViewModel();

            return PartialView("_EducationCreate", model);
        }


        [HttpPost]
        public ActionResult EducationCreate(EducationViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new StudentEntities())
                {
                    var education = ctx.Educations.Create();
                    var userID = Properties.Settings.Default.DefaultUserID;

                    education.PersonID = userID;
                    education.DegreeName = model.DegreeName;
                    education.SpecializedConcentrations = model.SpecializedConcentrations;
                    education.Percentage = model.Percentage;
                    education.Major = model.Major;
                    education.GraduationDate = model.GraduationDate;
                    education.InstitutionName = model.InstitutionName;
                    education.Percentage = model.Percentage;

                    ctx.Educations.Add(education);
                    ctx.SaveChanges();
                    return Json(new { success = true });
                }
            }
            return View("_EducationCreate",model);
        }

        public ActionResult EducationEdit(int id)
        {
            using (var ctx = new StudentEntities())
            {
                var model = ctx.Educations.Where(e => e.EducationID == id)
                               .Select(s => new EducationViewModel()
                               {
                                   EducationID = s.EducationID,
                                   DegreeName = s.DegreeName,
                                   SpecializedConcentrations = s.SpecializedConcentrations,
                                   Percentage = s.Percentage,
                                   Major = s.Major,
                                   GraduationDate = s.GraduationDate,
                                   InstitutionName = s.InstitutionName,
                                  
                                   

                               }).FirstOrDefault();

                return PartialView("_EducationEdit", model);
            }
        }

        [HttpPost]
        public ActionResult EducationEdit(EducationViewModel model)
        {
            using (var ctx = new StudentEntities())
            {
                var education = ctx.Educations.Find(model.EducationID);

                education.EducationID = model.EducationID;
                education.DegreeName = model.DegreeName;
                education.Major = model.Major;
                education.GraduationDate = model.GraduationDate;
                education.InstitutionName = model.InstitutionName;
                education.SpecializedConcentrations = model.SpecializedConcentrations;
                education.Percentage = model.Percentage;


                ctx.SaveChanges();

                return Json(new { success = true });
            }
        }

        [AllowAnonymous]
        public ActionResult CertificationDetails()
        {
            using (var ctx = new StudentEntities())
            {
                var isAdmin = ctx.People.Any(p => p.Email == User.Identity.Name && User.Identity.IsAuthenticated && p.IsAdmin);
                var userID = Properties.Settings.Default.DefaultUserID;
                var model = ctx.Certifications.Where(c => c.PersonID == userID)
                                        .Select(s => new CertificationViewModel()
                                        {
                                            CertificationID = s.CertificationID,
                                            CertificationNumber = s.CertificationNumber,
                                            CertificationName = s.CertificationName,
                                            IssueDate = s.Isuuedate,
                                            ExpirationDate = s.ExpirationDate,
                                            IssuingOrganisation = s.IssuingOrganisation,
                                            Location = s.Location,
                                            Experience = s.Experience,
                                            IsAdmin = isAdmin

                                        }).ToList();

                return PartialView("_CertificationDetails", model);
            }
        }


        public ActionResult CertificationCreate()
        {
            var model = new CertificationViewModel();

            return PartialView("_CertificationCreate", model);
        }

        [HttpPost]
        public ActionResult CertificationCreate(CertificationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var ctx = new StudentEntities())
                    {
                        var certification = ctx.Certifications.Create();
                        var userID = Properties.Settings.Default.DefaultUserID;

                        certification.PersonID = userID;
                        certification.CertificationName = model.CertificationName;
                        certification.CertificationNumber = model.CertificationNumber;
                        certification.IssuingOrganisation = model.IssuingOrganisation;
                        certification.Isuuedate = model.IssueDate;
                        certification.ExpirationDate = model.ExpirationDate;
                        certification.Location = model.Location;
                        certification.Experience = model.Experience;

                        ctx.Certifications.Add(certification);
                        ctx.SaveChanges();
                        return Json(new { success = true });
                    }
                }
                return View("_CertificationCreate", model);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        public ActionResult CertificationEdit(int id)
        {
            using (var ctx = new StudentEntities())
            {
                var model = ctx.Certifications.Where(c => c.CertificationID == id)
                               .Select(s => new CertificationViewModel()
                               {
                                   CertificationID = s.CertificationID,
                                   CertificationNumber = s.CertificationNumber,
                                   CertificationName = s.CertificationName,
                                   IssueDate = s.Isuuedate,
                                   ExpirationDate = s.ExpirationDate,
                                   IssuingOrganisation = s.IssuingOrganisation,
                                   Location = s.Location,
                                   Experience = s.Experience,
                                   


                               }).FirstOrDefault();

                return PartialView("_CertificationEdit", model);
            }
        }

        [HttpPost]
        public ActionResult CertificationEdit(CertificationViewModel model)
        {
            using (var ctx = new StudentEntities())
            {
                var certification = ctx.Certifications.Find(model.CertificationID);

                certification.CertificationID = model.CertificationID;
                certification.CertificationName = model.CertificationName;
                certification.CertificationNumber = model.CertificationNumber;
                certification.IssuingOrganisation = model.IssuingOrganisation;
                certification.Isuuedate = model.IssueDate;
                certification.ExpirationDate = model.ExpirationDate;
                certification.Location = model.Location;
                certification.Experience = model.Experience;


                ctx.SaveChanges();

                return Json(new { success = true });
            }
        }
        [AllowAnonymous]
        public ActionResult ProjectDetails()
        {
            using (var ctx = new StudentEntities())
            {
                var isAdmin = ctx.People.Any(p => p.Email == User.Identity.Name && User.Identity.IsAuthenticated && p.IsAdmin);
                var userID = Properties.Settings.Default.DefaultUserID;
                var model = ctx.Projects.Where(p => p.PersonID == userID)
                                        .Select(s => new ProjectViewModel()
                                        {
                                            ProjectID = s.ProjectID,
                                            ProjectName = s.ProjectName,
                                            ProjectDescription = s.ProjectDescription,
                                            TeamSize = s.TeamSize,
                                            IsAdmin = isAdmin

                                        }).ToList();

                return PartialView("_ProjectDetails", model);
            }
        }

        public ActionResult ProjectCreate()
        {
            var model = new ProjectViewModel();

            return PartialView("_ProjectCreate", model);
        }

        [HttpPost]
        public ActionResult ProjectCreate(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new StudentEntities())
                {
                    var project = ctx.Projects.Create();
                    var userID = Properties.Settings.Default.DefaultUserID;

                    project.PersonID = userID;
                    project.ProjectName = model.ProjectName;
                    project.ProjectDescription = model.ProjectDescription;
                    project.TeamSize = model.TeamSize;
                    project.Role = model.Role;

                    ctx.Projects.Add(project);
                    ctx.SaveChanges();
                    return Json(new { success = true });
                }
            }
            return View("_ProjectCreate", model);
        }
        public ActionResult ProjectEdit(int id)
        {
            using (var ctx = new StudentEntities())
            {
                var model = ctx.Projects.Where(p => p.ProjectID == id)
                               .Select(s => new ProjectViewModel()
                               {
                                   ProjectID = s.ProjectID,
                                   ProjectName = s.ProjectName,
                                   ProjectDescription = s.ProjectDescription,
                                   TeamSize = s.TeamSize,
                                   Role = s.Role,


                               }).FirstOrDefault();

                return PartialView("_ProjectEdit", model);
            }
        }

        [HttpPost]
        public ActionResult ProjectEdit(ProjectViewModel model)
        {
            using (var ctx = new StudentEntities())
            {
                var project = ctx.Projects.Find(model.ProjectID);

                project.ProjectID = model.ProjectID;
                project.ProjectName = model.ProjectName;
                project.ProjectDescription = model.ProjectDescription;
                project.TeamSize = model.TeamSize;
                project.Role = model.Role;


                ctx.SaveChanges();

                return Json(new { success = true });
            }
        }
        [AllowAnonymous]
        public ActionResult WorkDetails()
        {
            using (var ctx = new StudentEntities())
            {
                var isAdmin = ctx.People.Any(p => p.Email == User.Identity.Name && User.Identity.IsAuthenticated && p.IsAdmin);
                var userID = Properties.Settings.Default.DefaultUserID;
                var model = ctx.Works.Where(w => w.PersonID == userID)
                                        .Select(s => new WorkViewModel()
                                        {
                                            WorkID = s.WorkID,
                                            Role = s.Role,
                                            Duration =s.Duration,
                                            Duties =s.Duties,
                                            ReasonForLeaving =s.ReasonForLeaving,
                                            Location =s.Location,
                                            company=s.Company,
                                            Accomplishments =s.Accomplishments,
                                            IsAdmin = isAdmin

                                        }).ToList();

                return PartialView("_WorkDetails", model);
            }
        }

        public ActionResult WorkCreate()
        {
            var model = new WorkViewModel();

            return PartialView("_WorkCreate", model);
        }

        [HttpPost]
        public ActionResult WorkCreate(WorkViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var ctx = new StudentEntities())
                    {
                        var work = ctx.Works.Create();
                        var userID = Properties.Settings.Default.DefaultUserID;

                        work.PersonID = userID;
                        work.Role = model.Role;
                        work.Duration = model.Duration;
                        work.Duties = model.Duties;
                        work.ReasonForLeaving = model.ReasonForLeaving;
                        work.Location = model.Location;
                        work.Company = model.company;
                        work.Accomplishments = model.Accomplishments;
                        work.Expertise = model.Expertise;


                        ctx.Works.Add(work);
                        ctx.SaveChanges();
                        return Json(new { success = true });
                    }
                }
                return View("_WorkCreate", model);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        public ActionResult WorkEdit(int id)
        {
            using (var ctx = new StudentEntities())
            {
                var model = ctx.Works.Where(w => w.WorkID == id)
                               .Select(s => new WorkViewModel()
                               {
                                   WorkID = s.WorkID,
                                   Role = s.Role,
                                   company = s.Company,
                                   Duties = s.Duties,
                                   Duration = s.Duration


                               }).FirstOrDefault();

                return PartialView("_WorkEdit", model);
            }
        }

        [HttpPost]
        public ActionResult WorkEdit(WorkViewModel model)
        {
            using (var ctx = new StudentEntities())
            {
                var work = ctx.Works.Find(model.WorkID);

                work.WorkID = model.WorkID;
                work.Role = model.Role;
                work.Company = model.company;
                work.Duties = model.Duties;
                work.Duration = model.Duration;


                ctx.SaveChanges();

                return Json(new { success = true });
            }
        }

    }
}