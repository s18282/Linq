using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqConsoleApp
{
    public class LinqSamples
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        public LinqSamples()
        {
            LoadData();
        }

        public void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion

        }


        /*
            Celem ćwiczenia jest uzupełnienie poniższych metod.
         *  Każda metoda powinna zawierać kod C#, który z pomocą LINQ'a będzie realizować
         *  zapytania opisane za pomocą SQL'a.
         *  Rezultat zapytania powinien zostać wyświetlony za pomocą kontrolki DataGrid.
         *  W tym celu końcowy wynik należy rzutować do Listy (metoda ToList()).
         *  Jeśli dane zapytanie zwraca pojedynczy wynik możemy je wyświetlić w kontrolce
         *  TextBox WynikTextBox.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public void Przyklad1()
        {
            var result = from emp in Emps
                      where emp.Job == "Backend programmer"
                      select new
                      {
                          Nazwisko = emp.Ename,
                          Zawod = emp.Job
                      };
            foreach (var emp in result)
                Console.WriteLine(emp);

            Console.WriteLine("2. Lambda and Extension methods");
            result = Emps.Where(e => e.Job == "Backend programmer").Select(e => new { naziwsko = e.Ename, praca = e.Job }).ToList();
            foreach (var emp in result)
                Console.WriteLine(emp);
        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public void Przyklad2()
        {
            var result = from emp in Emps
                       where emp.Job == "Frontend programmer" && emp.Salary > 1000
                       orderby emp.Ename descending
                       select emp;
            foreach (var r in result)
                Console.WriteLine(r);

            result = Emps.Where(e => e.Job == "Frontend programmer" && e.Salary > 1000).OrderByDescending(e => e.Ename).ToList();
            foreach (var r in result)
                Console.WriteLine(r);

        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public void Przyklad3()
        {
            var result = (from emp in Emps
                        select emp.Salary).Max();
            Console.WriteLine(result);

            result = (from emp in Emps select emp).Max(emp => emp.Salary);
            Console.WriteLine(result);
        }

        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public void Przyklad4()
        {
            var result = from emp in Emps
                       where emp.Salary == (from emp1 in Emps
                                            select emp1.Salary).Max()
                       select new
                       {
                           Nazwisko = emp.Ename,
                           Zawod = emp.Job

                       };
            foreach (var r in result)
                Console.WriteLine(r);
;
            result = Emps.Where(e => e.Salary == Emps.Max(emp1 => emp1.Salary)).Select(e => new { nazwisko = e.Ename, praca = e.Job }).ToList();
            foreach (var r in result)
                Console.WriteLine(r);
        }

        /// <summary>
        /// SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public void Przyklad5()
        {
            var result = from emp in Emps
                       select new
                       {
                           Nazwisko = emp.Ename,
                           Praca = emp.Job
                       };
            foreach (var r in result)
                Console.WriteLine(r);

            result = Emps.Select(e => new { Nazwisko = e.Ename, Praca = e.Job }).ToList();
            foreach (var r in result)
                Console.WriteLine(r);
        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public void Przyklad6()
        {
            var result = from emp in Emps
                        join dept in Depts
                        on emp.Deptno equals dept.Deptno

                        select new
                        {
                            Nazwisko = emp.Ename,
                            Praca = emp.Job,
                            Departament = dept.Dname
                        };
            foreach (var r in result)
                Console.WriteLine(r);

            result = Emps.Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (e, d) => new { e.Ename, e.Job, d.Dname }).ToList();
            foreach (var r in result)
                Console.WriteLine(r);
        }

        /// <summary>
        /// SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public void Przyklad7()
        {
            var result = from emp in Emps
                        group emp by emp.Job into pracownicy
                        select new
                        {
                            Praca = pracownicy.Key,
                            LiczbaPracownikow = pracownicy.Count()
                        };
            foreach (var r in result)
                Console.WriteLine(r);

            result = Emps.GroupBy(e => e.Job).Select(e => new { Praca = e.Key, LiczbaPracownikow = e.Key.Length }).ToList();
            foreach (var r in result)
                Console.WriteLine(r);
        }

        /// <summary>
        /// Zwróć wartość "true" jeśli choć jeden
        /// z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public void Przyklad8()
        {
            var result = (from emp in Emps
                         where emp.Job == "Backend programmer"
                         select emp).Any();
            Console.WriteLine(Convert.ToBoolean(result));

            result = Emps.Any(e => e.Job == "Backend programmer");
            Console.WriteLine(Convert.ToBoolean(result));
        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "Brak wartości", null, null;
        /// </summary>
        public void Przyklad10()
        {
            var result = from emp in Emps.Union(new List<Emp>
            { new Emp {
                Ename = "Brak wartości", Job = null, HireDate = null
            } })
                        select new
                        {
                            emp.Ename,
                            emp.Job,
                            emp.HireDate
                        };
            foreach (var record in result)
                Console.WriteLine(record);

            result = Emps
                .Union(new List<Emp> { new Emp { Ename = "Brak wartości", Job = null, HireDate = null } })
                .Select(emp => new { emp.Ename, emp.Job, emp.HireDate });
            foreach (var record in result)
                Console.WriteLine(record);
        }

        //Znajdź pracownika z najwyższą pensją wykorzystując metodę Aggregate()
        public void Przyklad11()
        {
            var result = Emps.Aggregate((emp1, emp2) =>
            {
                if (emp1.Salary > emp2.Salary)
                    return emp1;
                else
                    return emp2;
            });
            Console.WriteLine(result);

        }

        //Z pomocą języka LINQ i metody SelectMany wykonaj złączenie
        //typu CROSS JOIN
        public void Przyklad12()
        {
            var result = Emps.SelectMany(emp => Depts, (e, d) => new { pracownik = e, departament = d });
            foreach (var emp in result)
                Console.WriteLine(emp);

        }
    }
}