using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DataBase
{
    namespace Tables
    {
        public class DetailedSchedule
        {
            public int? Schedule_Id { get; set; }
            public string Schedule_Name { get; set; }
            public string Schedule_Time { get; set; }
            public string Schedule_Classroom { get; set; }
            public int? Group_Id { get; set; }
            public string Group_Name { get; set; }
            public int? Teacher_Id { get; set; }
            public string Teacher_Name { get; set; }
            public int? Discipline_Id { get; set; }
            public string Discipline_Name { get; set; }
        }
        public class Teachers
        {
            [Key]
            public int Teacher_Id { get; set; }
            public string Teacher_Name { get; set; }
            public string Teacher_Surname { get; set; }
            public string Teacher_Email { get; set; }
            public string Teacher_Phone { get; set; }
            public Dictionary<string, string> toString()
            {
                return new Dictionary<string, string>
                {
                    ["Id"] = $"{Teacher_Id}",
                    ["Name"] = $"{Teacher_Name}",
                    ["Surname"] = $"{Teacher_Surname}",
                    ["Email"] = $"{Teacher_Email}",
                    ["Phone"] = $"{Teacher_Phone}",
                };
            }
            public void Stringto(Dictionary<string, string> values)
            {
                Teacher_Id = Int32.Parse(values["Id"]);
                Teacher_Name = values["Name"];
                Teacher_Surname = values["Surname"];
                Teacher_Email = values["Email"];
                Teacher_Phone = values["Phone"];
            }
        }
        public class Disciplines
        {
            [Key]
            public int Discipline_Id { get; set; }
            public string Discipline_Name { get; set; }
            public Dictionary<string, string> toString()
            {
                return new Dictionary<string, string>
                {
                    ["Id"] = $"{Discipline_Id}",
                    ["Name"] = $"{Discipline_Name}",
                };
            }
            public void Stringto(Dictionary<string, string> values)
            {
                Discipline_Id = Int32.Parse(values["Id"]);
                Discipline_Name = values["Name"];
            }

        }
        public class Faculties
        {
            [Key]
            public int Faculty_Id { get; set; }
            public string Faculty_Name { get; set; }
            public string Faculty_Short_Name { get; set; }
            public Dictionary<string, string> toString()
            {
                return new Dictionary<string, string>
                {
                    ["Id"] = $"{Faculty_Id}",
                    ["Name"] = $"{Faculty_Name}",
                    ["Short_Name"] = $"{Faculty_Short_Name}",
                };
            }
            public void Stringto(Dictionary<string, string> values)
            {
                Faculty_Id = Int32.Parse(values["Id"]);
                Faculty_Name = values["Name"];
                Faculty_Short_Name = values["Short_Name"];
            }
        }
        public class Departaments
        {
            [Key]
            public int Departament_Id { get; set; }
            public string Departament_Name { get; set; }
            public string Departament_Short_Name { get; set; }

            public int? Faculty_Id { get; set; }
            Faculties Faculties { get; set; }
            public Dictionary<string, string> toString()
            {
                return new Dictionary<string, string>
                {
                    ["Id"] = $"{Departament_Id}",
                    ["Name"] = $"{Departament_Name}",
                    ["Short_Name"] = $"{Departament_Short_Name}",
                    ["Faculty_Id"] = $"{Faculty_Id}",
                };
            }
            public void Stringto(Dictionary<string, string> values)
            {
                Departament_Id = Int32.Parse(values["Id"]);
                Departament_Name = values["Name"];
                Departament_Short_Name = values["Short_Name"];
                Faculty_Id = Int32.Parse(values["Faculty_Id"]);
            }
        }
        public class Groups
        {
            [Key]
            public int Group_Id { get; set; }
            public string Group_Name { get; set; }
            public int Group_Course { get; set; }

            public int? Departament_Id { get; set; }
            Departaments Departaments { get; set; }

            public Dictionary<string, string> toString()
            {
                return new Dictionary<string, string>
                {
                    ["Id"] = $"{Group_Id}",
                    ["Name"] = $"{Group_Name}",
                    ["Course"] = $"{Group_Course}",
                    ["Departament_Id"] = $"{Departament_Id}",
                };
            }
            public void Stringto(Dictionary<string, string> values)
            {
                Group_Id = Int32.Parse(values["Id"]);
                Group_Name = values["Name"];
                Group_Course = Int32.Parse(values["Course"]);
                Departament_Id = Int32.Parse(values["Departament_Id"]);
            }
        }
        public class Students
        {
            [Key]
            public int Student_Id { get; set; }
            public string Student_Name { get; set; }
            public string Student_Email { get; set; }
            public string Student_Phone { get; set; }

            public int? Group_Id { get; set; }
            Groups Groups { get; set; }
            public Dictionary<string, string> toString()
            {
                return new Dictionary<string, string>
                {
                    ["Id"] = $"{Student_Id}",
                    ["Name"] = $"{Student_Name}",
                    ["Email"] = $"{Student_Email}",
                    ["Phone"] = $"{Student_Phone}",
                    ["Group_Id"] = $"{Group_Id}",
                };
            }
            public void Stringto(Dictionary<string, string> values)
            {
                Student_Id = Int32.Parse(values["Id"]);
                Student_Name = values["Name"];
                Student_Email = values["Email"];
                Student_Phone = values["Phone"];
                Group_Id = Int32.Parse(values["Group_Id"]);
            }
        }
        public class Schedule
        {
            [Key]
            public int? Schedule_Id { get; set; }
            public string Schedule_Name { get; set; }
            public string Schedule_Time { get; set; }
            public string Schedule_Classroom { get; set; }

            public int? Group_Id { get; set; }
            Groups Groups { get; set; }

            public int? Teacher_Id { get; set; }
            Teachers Teachers { get; set; }

            public int? Discipline_Id { get; set; }
            Disciplines Disciplines { get; set; }

            public Dictionary<string, string> toString()
            {
                return new Dictionary<string, string>
                {
                    ["Id"] = $"{Schedule_Id}",
                    ["Name"] = $"{Schedule_Name}",
                    ["Time"] = $"{Schedule_Time}",
                    ["Classroom"] = $"{Schedule_Classroom}",
                    ["Group_Id"] = $"{Group_Id}",
                    ["Discipline_Id"] = $"{Discipline_Id}",
                    ["Teacher_Id"] = $"{Teacher_Id}",

                };
            }
            public void Stringto(Dictionary<string, string> values)
            {
                Schedule_Id = Int32.Parse(values["Id"]);
                Schedule_Name = values["Name"];
                Schedule_Time = values["Time"];
                Schedule_Classroom = values["Classroom"];
                Group_Id = Int32.Parse(values["Group_Id"]);
                Discipline_Id = Int32.Parse(values["Discipline_Id"]);
                Teacher_Id = Int32.Parse(values["Teacher_Id"]);
            }
        }
        public class Setting_University
        {
            [Key]
            public int University_Id { get; set; }
            public string University_Name { get; set; }
            public string University_Short_Name { get; set; }
            public string University_Adress { get; set; }
            public string University_Phone { get; set; }
            public string University_Site { get; set; }
            public Dictionary<string, string> toString()
            {
                return new Dictionary<string, string>
                {
                    ["Id"] = $"{University_Id}",
                    ["Name"] = $"{University_Name}",
                    ["Short_Name"] = $"{University_Short_Name}",
                    ["Adress"] = $"{University_Adress}",
                    ["Phone"] = $"{University_Phone}",
                    ["Site"] = $"{University_Site}",
                };
            }
            public void Stringto(Dictionary<string, string> values)
            {
                University_Id = Int32.Parse(values["Id"]);
                University_Name = values["Name"];
                University_Short_Name = values["Short_Name"];
                University_Adress = values["Adress"];
                University_Phone = values["Phone"];
                University_Site = values["Site"];
            }
        }

    }
}
