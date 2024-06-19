using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FrontendMaui
{
    public partial class Subject : ObservableObject
    {
        [ObservableProperty]
        string id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string neptun;

        [ObservableProperty]
        int credit;

        [ObservableProperty]
        bool exam;
    }
}
