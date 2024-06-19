using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMaui
{
    public class StudentDetailViewModel : ObservableObject
    {
        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set => SetProperty(ref _selectedStudent, value);
        }

        public StudentDetailViewModel(Student selectedStudent)
        {
            SelectedStudent = selectedStudent;
        }
    }
}
