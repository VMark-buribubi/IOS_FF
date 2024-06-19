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
    public partial class SubjectPageViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Subject> SubjectList { get; private set; } = new ObservableCollection<Subject>();

        [ObservableProperty]
        Subject selectedListItem;

        public SubjectPageViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task LoadSubjectAsync()
        {

            try
            {
                var subjects = await _httpClient.GetFromJsonAsync<Subject[]>($"Subject");
                if (subjects != null)
                {
                    SubjectList.Clear();
                    foreach (var subject in subjects)
                    {
                        SubjectList.Add(subject);
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
