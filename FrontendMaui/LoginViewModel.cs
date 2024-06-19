using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;


namespace FrontendMaui
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        private string email; // Changed from userName to email

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string errorMessage;

        public LoginViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        [RelayCommand]
        public async Task LoginAsync()
        {
            ErrorMessage = string.Empty;

            var loginModel = new { Email, Password }; // Changed from UserName to Email

            try
            {
                var response = await _httpClient.PostAsJsonAsync("Auth/login", loginModel);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var result = JsonSerializer.Deserialize<LoginResponse>(responseContent);

                        if (result != null)
                        {
                            await SecureStorage.SetAsync("auth_token", result.Token);

                            await Shell.Current.GoToAsync("//MainPage");
                        }
                    }
                    catch (JsonException ex)
                    {
                        ErrorMessage = $"Failed to deserialize the response: {ex.Message}";
                    }
                }
                else
                {
                    ErrorMessage = $"Login failed: {response.ReasonPhrase}";
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error content: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
            }
        }
    }
                

    public class LoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("expiration")]
        public DateTime Expiration { get; set; }
    }

}
