namespace FrontendMaui;
[QueryProperty(nameof(Student), "Student")]

public partial class StudentDetailPage : ContentPage
{
	Student student;
    public Student Student
    {
        get => student;
        set
        {
            student = value;
            OnPropertyChanged();
        }
    }
    public StudentDetailPage()
	{
		InitializeComponent();
        BindingContext = this;
    }
}