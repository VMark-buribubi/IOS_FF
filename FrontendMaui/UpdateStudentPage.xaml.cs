
namespace FrontendMaui;

public partial class UpdateStudentPage : ContentPage
{
    private UpdateStudentViewModel viewModel;

    public UpdateStudentPage(UpdateStudentViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = this.viewModel;
    }
}

