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
        /// <summary>
        /// Отримує всіх наявних викладачів університету
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     [
        ///         {
        ///           "teacher_Id": Id
        ///           "teacher_Same": "Name",
        ///           "teacher_Surname": "Surname",
        ///           "teacher_Email":"Email",
        ///           "teacher_Phone":"Phone number"
        ///         },
        ///           "teacher_Id": Id
        ///           "teacher_Same": "Name",
        ///           "teacher_Surname": "Surname",
        ///           "teacher_Email":"Email",
        ///           "teacher_Phone":"Phone number"
        ///         }
        ///     ]
        /// 
        /// </remarks>
        /// <response code="200">Буде повернено, якщо було знайдено хоча б одного викладача</response>
        /// <response code="404">Буде повернено, якщо не було знайдено жодного викладача</response>
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

        /// <summary>
        /// Отримує наявного викладача університету за його ідентифікатором
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     {
        ///         "teacher_Id": Id
        ///         "teacher_Same": "Name",
        ///         "teacher_Surname": "Surname",
        ///         "teacher_Email":"Email",
        ///         "teacher_Phone":"Phone number"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id">Ідентифікатор викладача</param>
        /// <response code="200">Буде повернено, якщо викладача не було знайдено </response>
        /// <response code="404">Буде повернено, якщо не було знайдено викладача</response>
        [HttpGet]
        [Route("api/university/teachers/{id}")]
        public async Task<JsonResult> GetTeacher(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    Teachers teacher = db.Teachers.Where(x => x.Teacher_Id == id).FirstOrDefault();
                    if (teacher != null)
                    { 
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
        /// <summary>
        /// Отримує всі наявні факультети університета
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     [
        ///         {
        ///             "faculty_Id": Id,
        ///             "faculty_Name": "Full_Name",
        ///             "faculty_Short_Name": "Short_Name"
        ///         },
        ///         {
        ///             "faculty_Id": Id,
        ///             "faculty_Name": "Full_Name",
        ///             "faculty_Short_Name": "Short_Name"
        ///         }   
        ///     ]
        /// 
        /// </remarks>
        /// <response code="200">Буде повернено, якщо було знайдено і повернено хоча б один факультет</response>
        /// <response code="404">Буде повернено, якщо не було знайдено жодного факультету </response>
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
        /// <summary>
        /// Отримує факультет університета за вказаним ідентифікатором
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     
        ///     {
        ///         "faculty_Id": Id,
        ///         "faculty_Name": "Full_Name",
        ///         "faculty_Short_Name": "Short_Name"
        ///     }
        ///     
        /// 
        /// </remarks>
        /// <param name="id">Ідентифікатор факультету</param>
        /// <response code="200">Буде повернено, якщо було знайдено і повернено факультет за вказаним ідентифікатором </response>
        /// <response code="404">Буде повернено, якщо не було знайдено факультету за вказаним ідентифікатором </response>
        [HttpGet]
        [Route("api/university/faculties/{id}")]
        public async Task<JsonResult> GetFaculty(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    Faculties faculties = db.Faculties.Where(x => x.Faculty_Id == id).FirstOrDefault();
                    if (faculties != null)
                    {
                        Response.StatusCode = 200;
                        return Json(faculties);
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
        /// <summary>
        /// Отримує всі наявні дисципліни університета
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     [
        ///         {
        ///             "discipline_Id": Id,
        ///             "discipline_Name": "Name",
        ///         },
        ///         {
        ///             "discipline_Id": Id,
        ///             "discipline_Name": "Name",
        ///         },
        ///     ]
        /// 
        /// </remarks>
        /// <response code="200">Буде повернено, якщо було знайдено і повернено хоча б одну дисципліну</response>
        /// <response code="404">Буде повернено, якщо не було знайдено жодної дисципліни </response>
        [HttpGet]
        [Route("api/university/disciplines")]
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
        /// <summary>
        /// Отримує дисципліну за вказаним ідентифікатором
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     
        ///     {
        ///         "discipline_Id": Id,
        ///         "discipline_Name": "Name",
        ///     }
        ///     
        /// 
        /// </remarks>
        /// <param name="id">Ідентифікатор дисципліни</param>
        /// <response code="200">Буде повернено, якщо було знайдено і повернено дисципліну за вказаним ідентифікатором</response>
        /// <response code="404">Буде повернено, якщо не було знайдено дисципліни за вказаним ідентифікатором </response>
        [HttpGet]
        [Route("api/university/disciplines/{id}")]
        public async Task<JsonResult> GetDiscipline(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    Disciplines disciplines = db.Disciplines.Where(x => x.Discipline_Id == id).FirstOrDefault();
                    if (disciplines != null)
                    {                    
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
        /// <summary>
        /// Отримує всі наявні кафедри університета
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     [
        ///         {
        ///             "departament_Id": Id,
        ///             "departament_Name": "Full_Name",
        ///             "departament_Short_Name": "Short_Name",
        ///             "faculty_Id": Id
        ///         },
        ///         {
        ///             "departament_Id": Id,
        ///             "departament_Name": "Full_Name",
        ///             "departament_Short_Name": "Short_Name",
        ///             "faculty_Id": Id
        ///         }
        ///     ]
        /// 
        /// </remarks>
        /// <response code="200">Буде повернено, якщо було знайдено і повернено хоча б одну кафедру</response>
        /// <response code="404">Буде повернено, якщо не було знайдено жодної кафедри </response>
        [HttpGet]
        [Route("api/university/departaments")]
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
        /// <summary>
        /// Отримує кафедру університета за вказаним ідентифікатором
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     
        ///     {
        ///         "departament_Id": Id,
        ///         "departament_Name": "Full_Name",
        ///         "departament_Short_Name": "Short_Name",
        ///         "faculty_Id": Id
        ///     }
        ///     
        /// 
        /// </remarks>
        /// <param name="id">Ідентифікатор кафедри</param>
        /// <response code="200">Буде повернено, якщо було знайдено і повернено кафедру за вказаним ідентифікатором</response>
        /// <response code="404">Буде повернено, якщо не було знайдено кафедри за вказаним ідентифікатором </response>
        [HttpGet]
        [Route("api/university/departaments/{id}")]
        public async Task<JsonResult> GetDepartament(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    Departaments departament = db.Departaments.Where(x => x.Departament_Id == id).FirstOrDefault();
                    if (departament != null)
                    {
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
        /// <summary>
        /// Отримує всі наявні групи університета
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     [
        ///         {
        ///             "group_Id": Id,
        ///             "group_Name": "Name",
        ///             "group_Course": Course,
        ///             "departament_Id": Id
        ///         },
        ///         {
        ///             "group_Id": Id,
        ///             "group_Name": "Name",
        ///             "group_Course": Course,
        ///             "departament_Id": Id
        ///         }
        ///     ]
        /// 
        /// </remarks>
        /// <response code="200">Буде повернено, якщо було знайдено і повернено хоча б одну групу</response>
        /// <response code="404">Буде повернено, якщо не було знайдено жодної групи </response>
        [HttpGet]
        [Route("api/university/groups")]
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
        /// <summary>
        /// Отримує групу за вказаним ідентифікатором
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     {
        ///         "group_Id": Id,
        ///         "group_Name": "Name",
        ///         "group_Course": Course,
        ///         "departament_Id": Id
        ///     }
        /// 
        /// </remarks>
        /// <param name="id">Ідентифікатор групи</param>
        /// <response code="200">Буде повернено, якщо було знайдено і повернено групу</response>
        /// <response code="404">Буде повернено, якщо не було знайдено групу </response>
        [HttpGet]
        [Route("api/university/groups/{id}")]
        public async Task<JsonResult> GetGroup(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    Groups group = db.Groups.Where(x => x.Group_Id == id).FirstOrDefault();
                    if (group != null)
                    { 
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
        /// <summary>
        /// Отримує всіх наявних студентів університета
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     [
        ///         {
        ///             "student_Id": Id,
        ///             "student_Name": "Name",
        ///             "student_Email": "Emali",
        ///             "student_Phone": "Phone number",
        ///             "group_Id": Id
        ///         },
        ///         {
        ///             "student_Id": Id,
        ///             "student_Name": "Name",
        ///             "student_Email": "Emali",
        ///             "student_Phone": "Phone number",
        ///             "group_Id": Id
        ///         }
        ///     ]
        /// 
        /// </remarks>
        /// <response code="200">Буде повернено, якщо було знайдено і повернено хоча б одного студента</response>
        /// <response code="404">Буде повернено, якщо не було знайдено жодного студента </response>
        [HttpGet]
        [Route("api/university/students")]
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
        /// <summary>
        /// Отримує студента за вказаним ідентифікатором
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     
        ///     {
        ///         "student_Id": Id,
        ///         "student_Name": "Name",
        ///         "student_Email": "Emali",
        ///         "student_Phone": "Phone number",
        ///         "group_Id": Id
        ///     }
        ///     
        /// 
        /// </remarks>
        /// <param name="id">Ідентифікатор студента</param>
        /// <response code="200">Буде повернено, якщо студента було знайдено і повернено</response>
        /// <response code="404">Буде повернено, якщо студента не було знайдено </response>
        [HttpGet]
        [Route("api/university/students/{id}")]
        public async Task<JsonResult> GetStudent(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    Students student = db.Students.Where(x => x.Student_Id == id).FirstOrDefault();
                    if (student != null)
                    {
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
        /// <summary>
        /// Отримує всі наявні розклади університета
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     [
        ///         {
        ///             "schedule_Id": Id,
        ///             "schedule_Name": "Name",
        ///             "schedule_Time": "Emali",
        ///             "schedule_Classroom": "Phone number",
        ///             "group_Id": Id,
        ///             "teacher_Id": Id,
        ///             "discipline_Id": Id
        ///         },
        ///         {
        ///             "schedule_Id": Id,
        ///             "schedule_Name": "Name",
        ///             "schedule_Time": "Emali",
        ///             "schedule_Classroom": "Phone number",
        ///             "group_Id": Id,
        ///             "teacher_Id": Id,
        ///             "discipline_Id": Id
        ///         }
        ///     ]
        /// 
        /// </remarks>
        /// <response code="200">Буде повернено, якщо було знайдено і повернено хоча б один розклад</response>
        /// <response code="404">Буде повернено, якщо не було знайдено жодного розкладу </response>
        [HttpGet]
        [Route("api/university/schedules")]
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
        /// <summary>
        /// Отримує детальний розклад за вказаним ідентифікатором
        /// </summary>
        /// <remarks>
        /// Отримана відповідь буде у вигляді:
        ///  
        ///     
        ///     {
        ///         "schedule_Id": Id,
        ///         "schedule_Name": "Name",
        ///         "schedule_Time": "Time",
        ///         "schedule_Classroom": "Classroom",
        ///         "group_Id": group_Id,
        ///         "group_Name": "group_Name",
        ///         "teacher_Id": teacher_Id,
        ///         "teacher_Name": "teacher_Name",
        ///         "discipline_Id": discipline_Id,
        ///         "discipline_Name": "discipline_Name"
        ///     }
        ///     
        /// 
        /// </remarks>
        /// <param name="id">Ідентифікатор розкладу</param>
        /// <response code="200">Буде повернено, якщо розклад знайдено і повернено</response>
        /// <response code="404">Буде повернено, якщо розкладу не було знайдено </response>
        [HttpGet]
        [Route("api/university/schedules/{id}")]
        public async Task<JsonResult> GetSchedule(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    Schedule schedule = db.Schedule.Where(x => x.Schedule_Id == id).FirstOrDefault();
                    if (schedule != null)
                    {
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
        /// <summary>
        /// Видаляє розклад за вказаним ідентифікатором
        /// </summary>
        /// <param name="id">Ідентифікатор розкладу</param>
        /// <response code="200">Буде повернено, якщо розклад знайдено і видалено </response>
        /// <response code="404">Буде повернено, якщо розкладу не було знайдено або його вже видалили </response>
        [HttpDelete]
        [Route("api/university/delete/Schedules/{id}")]
        public async Task<JsonResult> DeleteSchedule(int id)
        {
            try
            {     
                using (DataBaseContext db = new DataBaseContext())
                {
                    Schedule schedule = db.Schedule.Where(x => x.Schedule_Id == id).FirstOrDefault();
                    if (schedule != null)
                    {
                        db.Schedule.Remove(schedule);
                        db.SaveChanges();
                        Response.StatusCode = 200;
                        return Json(new Success() { success_code = 200, success_message = "Успішно видалено" });
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return Json(new Error() { error_code = 404, error_message = "Розклад з даним ідетифікатором не знайдено, можливо, його вже видалили" });
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return Json(new Error() { error_code = 404, error_message = "Щось пішло не так" });
            }
        }
        /// <summary>
        /// Створює розклад 
        /// </summary>
        /// <response code="200">Буде повернено, якщо розклад було успішно створено</response>
        /// <response code="400">Буде повернено, якщо даний розклад не коректний </response>
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
                    schedule.Schedule_Id = null;
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
        /// <summary>
        /// Оновлює розклад
        /// </summary>
        /// <param name="id">Ідентифікатор розкладу</param>
        /// <response code="200">Буде повернено, якщо розклад було успішно оновлено</response>
        /// <response code="404">Буде повернено, якщо розклад, який треба оновити не знайдено </response>
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
                    result.Schedule_Id = id;
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
