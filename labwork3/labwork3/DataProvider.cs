using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace labwork3
{
    public static class DataProvider
    {
        public class Student
        {
            public string Name { get; set; }
            public string School { get; set; }
        }

        public class ZNO
        {
            public int Id { get; set; }
            public string Subject { get; set; }

            public int Mark { get; set; }

            public string DateTime { get; set; }
        }

        public static Dictionary<int, Student> Students { get; private set; }
        public static Dictionary<int, List<ZNO>> ZNOs { get; private set; }
        public static List<ZNO> ZNOsList { get; private set; }

        const string DataDir = @"C:\Users\Іринка\source\repos\labwork3\";

        public static void ReadData()
        {
            Students = new Dictionary<int, Student>();
            ZNOs = new Dictionary<int, List<ZNO>>();
            ZNOsList = new List<ZNO>();
            foreach (var line in File.ReadAllLines(DataDir + "Students.txt", Encoding.GetEncoding(1251)))
            {
                var words = Regex.Split(line.Trim(), @"\s+");
                var id = int.Parse(words[0]);
                Students.Add(id, new Student { Name = words[1], School = words[2]});
            }

            foreach (var line in File.ReadAllLines(DataDir + "ZNO.txt", Encoding.GetEncoding(1251)))
            {
                var words = Regex.Split(line.Trim(), @"\s+");
                var id = int.Parse(words[0]);
                List<ZNO> existing;
                if (!ZNOs.TryGetValue(id, out existing))
                {
                    existing = new List<ZNO>();
                    ZNOs[id] = existing;
                }
                ZNO zno = new ZNO
                {
                    Id = id,
                    Subject = words[1],
                    Mark = int.Parse(words[2]),
                    DateTime = words.Length == 4 ? DateTime.Parse(words[3]).ToShortDateString() : "-------------"
                };
                existing.Add(zno);
                ZNOsList.Add(zno);
            }
        }
    }
}