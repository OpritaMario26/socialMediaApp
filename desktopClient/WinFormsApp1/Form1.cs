using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Net.Mail;
using System.Net;


namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private SqlConnection connection;
        private SqlDataAdapter dataAdapter;
        private DataTable dataTable;
        private BindingSource bindingSource;
        PostService postService;
        List<Post> postList;
        public Form1()
        {
            InitializeComponent();

            postService = new PostService();
            postService.createConnection();

            LoadActiveUsers();

            var usersGrid = postService.GetActiveUsers();
            emailGridView.DataSource = usersGrid;


        }

        private void LoadActiveUsers()
        {
            var activeUsers = postService.GetActiveUsers();
            usersList.Items.Clear();

            foreach (var user in activeUsers)
            {
                usersList.Items.Add(user);
            }
        }

        private void usersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            User selectedUser = (User)usersList.SelectedItem;

            if (selectedUser != null)
            {

                var postsForSelectedUser = postService.GetPendingPosts()
                    .Where(p => p.user.id == selectedUser.id)
                    .ToList();


                dataGridView1.DataSource = postsForSelectedUser;
            }
        }





        private void approveButton_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {

                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                Post selectedPost = (Post)selectedRow.DataBoundItem;


                selectedPost.status = PostStatus.PUBLISHED;


                dataGridView1.Refresh();


                UpdatePostStatusOnServer(selectedPost);
            }
            else
            {
                MessageBox.Show("Selectează un post pentru a-l aproba.");
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {

                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                Post selectedPost = (Post)selectedRow.DataBoundItem;


                selectedPost.status = PostStatus.REMOVED;


                dataGridView1.Refresh();


                UpdatePostStatusOnServer(selectedPost);
            }
            else
            {
                MessageBox.Show("Selectează un post pentru a-l șterge.");
            }
        }

        private void UpdatePostStatusOnServer(Post post)
        {


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8083/");  // Adresa serverului tău
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var jsonContent = JsonSerializer.Serialize(post);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            HttpResponseMessage response = client.PutAsync($"post/{post.id}", content).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Statusul postului a fost actualizat cu succes.");
            }
            else
            {
                MessageBox.Show("A apărut o eroare la actualizarea statusului.");
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string keyword = searchBox.Text.Trim(); // Căutăm cuvântul din TextBox

            if (!string.IsNullOrEmpty(keyword))
            {
                // Obținem posturile care conțin cuvântul cheie
                List<Post> posts = postService.GetPostsByKeyword(keyword);

                if (posts != null && posts.Count > 0)
                {
                    // Afișăm posturile în DataGridView
                    searchDataGridView.DataSource = posts;
                }

            }
            else
            {
                MessageBox.Show("Please enter a keyword to search.");
            }
        }

        private void sendEmailButton_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (emailGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selectați cel puțin un utilizator.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                if (string.IsNullOrWhiteSpace(subjectTextBox.Text) || string.IsNullOrWhiteSpace(bodyTextBox.Text))
                {
                    MessageBox.Show("Completați subiectul și corpul mesajului.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                List<User> selectedUsers = new List<User>();
                foreach (DataGridViewRow row in emailGridView.SelectedRows)
                {
                    var user = row.DataBoundItem as User;
                    if (user != null)
                    {
                        selectedUsers.Add(user);
                    }
                }

                List<string> emailAddresses = selectedUsers.Select(u => u.email).ToList();

                // Creează obiectul EmailService cu setările corespunzătoare
                EmailService emailService = new EmailService("smtp.mail.yahoo.com", 587, "opritamario@yahoo.com", "ixmalfjqiegkymax");

                // Trimite e-mailuri către toți utilizatorii selectați
                foreach (var email in emailAddresses)
                {
                    // Trimite emailul folosind EmailService
                    emailService.SendEmail("opritamario@yahoo.com", email, subjectTextBox.Text, bodyTextBox.Text);
                }

                // Afișează un mesaj de succes
                MessageBox.Show("E-mailurile au fost trimise cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}