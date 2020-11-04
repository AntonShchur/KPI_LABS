using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.DataBase.Tables;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{

    public class ScheduleController : Controller
    {
        [HttpGet]
        [Route("api/university/teachers")]
        public async Task<JsonResult> GetTeachers()
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Teachers.ToList().Count() > 0)
                    {
                        List<Teachers> teachers = db.Teachers.ToList();
                        return Json(teachers);
                    }
                    else return Json(new Error() { error_code = 404, error_message = "Викладачів не знайдено" });
                }
            }
            catch (Exception)
            {
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }

        }
        [HttpGet]
        [Route("api/university/teachers/{id}")]
        public async Task<JsonResult> GetTeacher(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Teachers.ToList().Count() > 0)
                    {
                        Teachers teacher = db.Teachers.Where(x => x.Teacher_Id == id).FirstOrDefault();
                        Response.StatusCode = 200;
                        return Json(teacher);
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return Json(new Error() { error_code = 404, error_message = "Даного викладача не знайдено" });
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }

        }

        [HttpGet]
        [Route("api/university/faculties")]
        public async Task<JsonResult> GetFaculties()
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Faculties.ToList().Count() > 0)
                    {
                        List<Faculties> faculties = db.Faculties.ToList();
                        return Json(faculties);
                    }
                    else return Json(new Error() { error_code = 404, error_message = "Факультетів не знайдено" });
                }
            }
            catch (Exception)
            {
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }

        }
        [HttpGet]
        [Route("api/university/faculties/{id}")]
        public async Task<JsonResult> GetFaculty(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Teachers.ToList().Count() > 0)
                    {
                        Faculties teacher = db.Faculties.Where(x => x.Faculty_Id == id).FirstOrDefault();
                        Response.StatusCode = 200;
                        return Json(teacher);
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return Json(new Error() { error_code = 404, error_message = "Даного факультету не знайдено" });
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }

        }
        [HttpGet]
        [Route("api/university/Disciplines")]
        public async Task<JsonResult> GetDisciplines()
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Disciplines.ToList().Count() > 0)
                    {
                        List<Disciplines> disciplines = db.Disciplines.ToList();
                        return Json(disciplines);
                    }
                    else return Json(new Error() { error_code = 404, error_message = "Дисциплін не знайдено" });
                }
            }
            catch (Exception)
            {
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }

        }
        [HttpGet]
        [Route("api/university/Disciplines/{id}")]
        public async Task<JsonResult> GetDiscipline(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Disciplines.ToList().Count() > 0)
                    {
                        Disciplines disciplines = db.Disciplines.Where(x => x.Discipline_Id == id).FirstOrDefault();
                        Response.StatusCode = 200;
                        return Json(disciplines);
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return Json(new Error() { error_code = 404, error_message = "Даної дисципліни не знайдено" });
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }

        }
        [HttpGet]
        [Route("api/university/Departaments")]
        public async Task<JsonResult> GetDepartaments()
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Departaments.ToList().Count() > 0)
                    {
                        List<Departaments> departaments = db.Departaments.ToList();
                        return Json(departaments);
                    }
                    else return Json(new Error() { error_code = 404, error_message = "Департаментів не знайдено" });
                }
            }
            catch (Exception)
            {
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }

        }
        [HttpGet]
        [Route("api/university/Disciplines/{id}")]
        public async Task<JsonResult> GetDepartament(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Departaments.ToList().Count() > 0)
                    {
                        Departaments departament = db.Departaments.Where(x => x.Departament_Id == id).FirstOrDefault();
                        Response.StatusCode = 200;
                        return Json(departament);
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return Json(new Error() { error_code = 404, error_message = "Даного департаменту не знайдено" });
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }

        }
        [HttpGet]
        [Route("api/university/Groups")]
        public async Task<JsonResult> GetGroups()
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Groups.ToList().Count() > 0)
                    {
                        List<Groups> groups = db.Groups.ToList();
                        return Json(groups);
                    }
                    else return Json(new Error() { error_code = 404, error_message = "Груп не знайдено" });
                }
            }
            catch (Exception)
            {
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }

        }
        [HttpGet]
        [Route("api/university/Groups/{id}")]
        public async Task<JsonResult> GetGroup(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Groups.ToList().Count() > 0)
                    {
                        Groups group = db.Groups.Where(x => x.Group_Id == id).FirstOrDefault();
                        Response.StatusCode = 200;
                        return Json(group);
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return Json(new Error() { error_code = 404, error_message = "Даної групи  не знайдено" });
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }

        }
        [HttpGet]
        [Route("api/university/Students")]
        public async Task<JsonResult> GetStudents()
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Students.ToList().Count() > 0)
                    {
                        List<Students> students = db.Students.ToList();
                        return Json(students);
                    }
                    else return Json(new Error() { error_code = 404, error_message = "Студентів не знайдено" });
                }
            }
            catch (Exception)
            {
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }

        }
        [HttpGet]
        [Route("api/university/Groups/{id}")]
        public async Task<JsonResult> GetStudent(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Students.ToList().Count() > 0)
                    {
                        Students student = db.Students.Where(x => x.Student_Id == id).FirstOrDefault();
                        Response.StatusCode = 200;
                        return Json(student);
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return Json(new Error() { error_code = 404, error_message = "Даного студента не знайдено" });
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }

        }
        [HttpGet]
        [Route("api/university/Schedules")]
        public async Task<JsonResult> GetSchedules()
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Schedule.ToList().Count() > 0)
                    {
                        List<Schedule> schedule = db.Schedule.ToList();
                        return Json(schedule);
                    }
                    else return Json(new Error() { error_code = 404, error_message = "Розкладу не знайдено" });
                }
            }
            catch (Exception)
            {
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }

        }
        [HttpGet]
        [Route("api/university/Schedules/{id}")]
        public async Task<JsonResult> GetSchedule(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    if (db.Students.ToList().Count() > 0)
                    {
                        Schedule schedule = db.Schedule.Where(x => x.Schedule_Id == id).FirstOrDefault();
                        string group_name = db.Groups.Where(x => x.Group_Id == schedule.Group_Id).FirstOrDefault().Group_Name;
                        string discipline_name = db.Disciplines.Where(x => x.Discipline_Id == schedule.Discipline_Id).FirstOrDefault().Discipline_Name;
                        string teacher_name = db.Teachers.Where(x => x.Teacher_Id == schedule.Teacher_Id).FirstOrDefault().Teacher_Name;

                        DetailedSchedule detailedSchedule = new DetailedSchedule();

                        detailedSchedule.Schedule_Id = schedule.Schedule_Id;
                        detailedSchedule.Schedule_Name = schedule.Schedule_Name;
                        detailedSchedule.Schedule_Classroom = schedule.Schedule_Classroom;
                        detailedSchedule.Schedule_Time = schedule.Schedule_Time;
                        detailedSchedule.Teacher_Id = schedule.Teacher_Id;
                        detailedSchedule.Teacher_Name = teacher_name;
                        detailedSchedule.Discipline_Id = schedule.Discipline_Id;
                        detailedSchedule.Discipline_Name = discipline_name;
                        detailedSchedule.Group_Id = schedule.Group_Id;
                        detailedSchedule.Group_Name = group_name;

                        Response.StatusCode = 200;
                        return Json(detailedSchedule);
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return Json(new Error() { error_code = 404, error_message = "Даного розкладу не знайдено" });
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }
        }
        [HttpDelete]
        [Route("api/university/delete/Schedules/{id}")]
        public async Task<JsonResult> DeleteSchedule(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    Schedule schedule = db.Schedule.Where(x => x.Schedule_Id == id).FirstOrDefault();
                    db.Schedule.Remove(schedule);
                    db.SaveChanges();
                    Response.StatusCode = 200;
                    return Json(new Success() { success_code = 200, success_message = "Успішно видалено" });
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return Json(new Error() { error_code = 404, error_message = "Розклад з даним ідетифікатором не знайдено, можливо, його вже видалили" });
            }
        }
        [HttpPost]
        [Route("api/university/add/Schedule")]
        public JsonResult CreateSchedule([FromBody] Schedule schedule)
        {

            using (DataBaseContext db = new DataBaseContext())
            {
                List<Schedule> schedules = db.Schedule.ToList();
                if((from s in schedules where 
                      s.Schedule_Name == schedule.Schedule_Name 
                   && s.Schedule_Time == schedule.Schedule_Time
                   && s.Schedule_Classroom == schedule.Schedule_Classroom
                   && s.Teacher_Id == schedule.Teacher_Id
                   && s.Group_Id == schedule.Group_Id
                   && s.Discipline_Id == schedule.Discipline_Id select s).Count() != 0)
                {
                    Response.StatusCode = 200;
                    return Json(new Success() { success_code = 200, success_message = "Даний розклад вже створено" });
                }
                if (db.Teachers.Where(x => x.Teacher_Id == schedule.Teacher_Id).Count() != 0
                    && db.Groups.Where(x => x.Group_Id == schedule.Group_Id).Count() != 0
                    && db.Disciplines.Where(x => x.Discipline_Id == schedule.Discipline_Id).Count() != 0)
                {
                    db.AddAsync(schedule);
                    db.SaveChanges();
                    Response.StatusCode = 200;
                    return Json(new Success() { success_code = 200, success_message = "Розклад успішно створено" });
                }
                else
                {
                    Response.StatusCode = 400;
                    return Json(new Error() { error_code = 400, error_message = "Спершу треба додати всі залежності(внести в БД групу, викладача, предмет)." });
                }
            }
        }
        [HttpPut]
        [Route("api/university/update/Schedule/{id}")]
        public JsonResult UpdateSchedule([FromBody] Schedule schedule, int id)
        {

            using (DataBaseContext db = new DataBaseContext())
            {
                db.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
                Schedule result = db.Schedule.SingleOrDefault(x => x.Schedule_Id == id);
                if(result != null)
                {
                    result = schedule;
                    db.Schedule.Update(result);
                    db.SaveChanges();
                    Response.StatusCode = 200;
                    return Json(new Success() { success_code = 200, success_message = "Розклад успішно оновлено" });
                }
                else
                {
                    Response.StatusCode = 404;
                    return Json(new Error() { error_code = 404, error_message = "Розклад не знайдено" });
                }
            }
        }


    } 
    public class Error
    {
        public int error_code { get; set; }
        public string error_message { get; set; }
    }
    public class Success
    {
        public int success_code { get; set; }
        public string success_message { get; set; }
    }
}
