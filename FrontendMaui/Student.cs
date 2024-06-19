using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FrontendMaui
{
    public partial class Student : ObservableObject
    {
        [ObservableProperty]
        string id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string neptun;

        [JsonIgnore]
        public string Image { get; set; }
    }
}
