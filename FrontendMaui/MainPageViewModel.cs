using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMaui
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Student> StudentList { get; private set; } = new ObservableCollection<Student>();

        [ObservableProperty]
        Student selectedListItem;

        public MainPageViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [RelayCommand]
        public async Task NavigateToCreateStudentPageAsync()
        {
            await Shell.Current.GoToAsync("addnewstudent");
        }
        [RelayCommand]
        public async Task ShowStudentDetailsAsync()
        {
            if (selectedListItem != null)
            {
                var param = new ShellNavigationQueryParameters
                {
                    {"Student",selectedListItem}
                };
                await Shell.Current.GoToAsync("studentdetails", param);
            }

        }
        //[RelayCommand]
        //public async Task UpdatePersonAsync()
        //{
        //    await Shell.Current.GoToAsync("updateperson");
        //}
        [RelayCommand]
        public async Task DeleteStudentAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"student/{id}");

            if (response.IsSuccessStatusCode)
            {
                await LoadStudentAsync();
            }

        }
        public async Task LoadStudentAsync()
        {

            try
            {
                var students = await _httpClient.GetFromJsonAsync<Student[]>($"Student");
                if (students != null)
                {
                    StudentList.Clear();
                    foreach (var student in students)
                    {
                        Random random = new Random();
                        int num = random.Next(1, 53);
                        student.Image = $"https://xsgames.co/randomusers/assets/avatars/pixel/{num}.jpg";
                        StudentList.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
        }
    }
}
