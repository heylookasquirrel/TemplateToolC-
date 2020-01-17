using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Windows.Controls;
using Newtonsoft.Json;
using static TemplateTool.Ticket;


namespace TemplateTool

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    
    public partial class MainWindow : Window
    {
        private DefaultText defaults;
        private List<Ticket> history;
        public MainWindow()
        {
            InitializeComponent();


            //Dropdown Init
            this.LocationDropdown.Items.Add("Los Angeles");
            this.LocationDropdown.Items.Add("Miami");
            this.LocationDropdown.Items.Add("San Antonio");
            this.LocationDropdown.Items.Add("New York");
            this.LocationDropdown.SelectedIndex = 0;

            for (int i = 0; i < 10; i++) {
                this.AlertsDropdown.Items.Add(i);
                this.PreDropdown.Items.Add(i);
            }

            this.AlertsDropdown.SelectedIndex = 0;
            this.PreDropdown.SelectedIndex = 0;
            //Initial Values
            CreateNewNote();
            LoadList();

            defaults = new DefaultText
            {
                Comments = "What did they say?",
                Trail = "What did they click?",
                Location = "Location",
                Schedule = "Schedule",
                Manager = "Manager@email.com",
                Rto = "None"
            };

            ClearForms();

        }


        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            string cpBuffer = "";
            cpBuffer += "Alerts: " + this.AlertsDropdown.Text + "\n";
            cpBuffer += "Previous Inc: " + this.PreDropdown.Text + "\n";
            cpBuffer += "Input 1: " + this.CommentsField.Text + "\n";
            cpBuffer += "Input 2: " + this.TrailField.Text + "\n";
            cpBuffer += "Location: " + this.LocationDropdown.Text + "\n\n";

            foreach (object step in this.NoteSteps.Children) {
                string note = null;
                if (step is TextBox) {
                    note = (step as TextBox).Text;
                    cpBuffer += "-" + note + "\n";
                }
            }

            cpBuffer += "\n";

            if (this.Escalation.IsChecked.Value && this.Escalation.IsChecked.HasValue) {
                cpBuffer += "Input 3: " + this.LocationField.Text + "\n";
                cpBuffer += "Input 4: " + this.ScheduleField.Text + "\n";
                cpBuffer += "Input 5: " + this.ManagerField.Text + "\n";
                cpBuffer += "Input 6: " + this.rtoField.Text + "\n";
            };
            Clipboard.SetText(cpBuffer);
            Console.WriteLine("Copy Successful");




        }

        private void LoadList()
        {

            ListView list = this.TemplateList;
            //reads the json file locally 
            using (StreamReader r = new StreamReader(@"..\..\database.json")) {
                string data = r.ReadToEnd();

                //creates a list object for our ListView to use
                List<Template> obj = JsonConvert.DeserializeObject<List<Template>>(data);

                //Assign the object full of template values to the ListView
                list.ItemsSource = obj;

            }

        }

        private void RemoteButton_Click(object sender, RoutedEventArgs e) {
            try {
                using (Process myProcess = new Process()) {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = @"C:\Windows\System32\msra.exe";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.Arguments = "/Offerra " + this.IPField.Text;

                    myProcess.Start();

                }
            }
            catch (Exception error) {

                Console.WriteLine(error.Message);
            }
        }

        private void PingButton_Click(object sender, RoutedEventArgs e) {
            try {
                using (Process myProcess = new Process()) {
                    myProcess.StartInfo.FileName = @"C:\Windows\System32\ping.exe";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.Arguments = "/t " + this.IPField.Text;
                    myProcess.Start();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        private TextBox CreateNewNote(string input = "") {
            TextBox NewNote = new TextBox();
            //Sets margin and spacing for new notes
            Thickness margin = NewNote.Margin;
            margin.Top = 5;
            NewNote.Margin = margin;
            //Creates the box in the right size and font
            NewNote.FontSize = 16;
            NewNote.Height = 30;
            NewNote.Width = 230;
            NewNote.VerticalContentAlignment = VerticalAlignment.Center;
            NewNote.Text = input;
            this.NoteSteps.Children.Add(NewNote);

            return NewNote;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewNote();

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            int count = this.NoteSteps.Children.Count - 1;
            if (count == 0) {
                return;
            }
            this.NoteSteps.Children.RemoveAt(count);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.NoteSteps.Children.Clear();
            ClearForms();
            CreateNewNote();
        }

        private void ClearForms()
        {
           
    
            CommentsField.Text = defaults.Comments;
            TrailField.Text = defaults.Trail;
            LocationField.Text = defaults.Location;
            ScheduleField.Text = defaults.Schedule;
            ManagerField.Text = defaults.Manager;
            rtoField.Text = defaults.Rto;
        }

        private void LocationField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LocationField.Text == defaults.Location)
            {
                
                LocationField.Text = "";
            }
        }

        private void LocationField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LocationField.Text))
            {
                LocationField.Text = defaults.Location;
            }
        }

        private void ScheduleField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ScheduleField.Text == defaults.Schedule)
            {

                ScheduleField.Text = "";
            }
        }

        private void ScheduleField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ScheduleField.Text))
            {
                ScheduleField.Text = defaults.Schedule;
            }
        }

        private void TemplateList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Find what object was clicked
            int index = TemplateList.SelectedIndex;
            if(index == -1) { return; }
            //Grab the steps for that item
            string[] steps = (TemplateList.Items[index] as Template).Steps;
            //Clear out current notes
            NoteSteps.Children.Clear();
            foreach (string value in steps)
            {
                //Add a new note for each step in the template
                CreateNewNote(value);
            }
        }

        private void ManagerField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ManagerField.Text == defaults.Manager)
            {

                ManagerField.Text = "";
            }
        }

        private void ManagerField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ManagerField.Text))
            {
                ManagerField.Text = defaults.Manager;
            }
        }

        private void CommentsField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CommentsField.Text == defaults.Comments)
            {

                CommentsField.Text = "";
            }
            
        }

        private void CommentsField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CommentsField.Text))
            {
                CommentsField.Text = defaults.Comments;
            }
        }

        private void TrailField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TrailField.Text == defaults.Trail)
            {

                TrailField.Text = "";
            }
        }

        private void TrailField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TrailField.Text))
            {
                TrailField.Text = defaults.Trail;
            }
        }

        private void RtoField_GotFocus(object sender, RoutedEventArgs e)
        {
            if (rtoField.Text == defaults.Rto)
            {

                rtoField.Text = "";
            }
        }

        private void RtoField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtoField.Text))
            {
                rtoField.Text = defaults.Rto;
            }
        }
    }

    public class Kb
    {
        [JsonProperty("name")] public string name { get; set; }
        [JsonProperty("url")] public string url { get; set; }
    }

    public class Template
    {
        [JsonProperty("KB")] public Kb[] KB { get; set; }
        [JsonProperty("notes")] public string[] Steps { get; set; }
        
        [JsonProperty("name")]public string Name { get; set; }
        [JsonProperty("Description")] public string Description { get; set; }
    }
}
