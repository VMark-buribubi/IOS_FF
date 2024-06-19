namespace FrontendMaui;

public partial class AddNewStudentPage : ContentPage
{
    private readonly CreateStudentViewModel viewModel;
    public AddNewStudentPage(CreateStudentViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = this.viewModel;
    }
}