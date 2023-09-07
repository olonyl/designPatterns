using System.ComponentModel.DataAnnotations;

namespace DesignPatternsPrototypes.Inheritance
{
    public class Employee : Person, IDeepCopyable<Employee>
    {
        public int Salary;

        public Employee()
        {

        }

        public Employee(string[] names, Address address, int salary) : base(names, address)
        {
            Salary = salary;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(Salary)}: {Salary}";
        }
        public void CopyTo(Employee target)
        {
            base.CopyTo(target); ;
            target.Salary = Salary;
        }
    }
}
