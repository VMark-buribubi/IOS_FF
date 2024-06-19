using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FrontendMaui
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Microsoft.Maui.Controls;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public partial class UpdateStudentViewModel : ObservableObject, IQueryAttributable
    {
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string neptun;

        [ObservableProperty]
        private string errorMessage;

        public UpdateStudentViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [RelayCommand]
        public async Task UpdateStudentAsync()
        {
            var updatedStudent = new StudentCreateRequest
            {
                Id = Id,
                Name = Name,
                Neptun = Neptun
            };

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Student", updatedStudent);

                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.GoToAsync(".."); // Navigate back
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ErrorMessage = $"Error updating student: {response.ReasonPhrase}. Details: {responseContent}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Student", out var studentObj) && studentObj is Student student)
            {
                Id = student.Id;
                Name = student.Name;
                Neptun = student.Neptun;
            }
        }
    }


}
