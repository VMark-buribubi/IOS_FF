namespace FrontendMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("studentdetails", typeof(StudentDetailPage));
            Routing.RegisterRoute("addnewstudent", typeof(AddNewStudentPage));
            Routing.RegisterRoute("updatestudent", typeof(UpdateStudentPage));
        }
    }
}
