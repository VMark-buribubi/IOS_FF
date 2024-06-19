using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace FrontendMaui
{
    public partial class CreateStudentViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string neptun;

        [ObservableProperty]
        private string errorMessage;

        public CreateStudentViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [RelayCommand]
        public async Task CreateStudentAsync()
        {
            var newStudent = new StudentCreateRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Neptun = Neptun
            };

            try
            {
                // Ensure the URL is fully qualified and points to your backend endpoint
                var response = await _httpClient.PostAsJsonAsync("Student", newStudent);

                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.GoToAsync(".."); // Navigate back
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ErrorMessage = $"Error creating student: {response.ReasonPhrase}. Details: {responseContent}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
            }
        }
    }

}
