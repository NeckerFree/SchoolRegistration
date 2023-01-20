namespace SR.Models
{
    public partial class Student
    {
        public string FullName { get { return  Name + " " + LastName; } }
    }
}
