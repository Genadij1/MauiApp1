using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoadButtonClicked(object sender, EventArgs e)
        {
            string url = "https://www.gutenberg.org/files/1524/1524-0.txt";
            string hamletText = await LoadTextFromUrl(url);
            contentEditor.Text = hamletText;
        }

        private async Task<string> LoadTextFromUrl(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                catch (HttpRequestException e)
                {
                    await DisplayAlert("Request Error", e.Message, "OK");
                    return string.Empty;
                }
            }
        }
    }
}
