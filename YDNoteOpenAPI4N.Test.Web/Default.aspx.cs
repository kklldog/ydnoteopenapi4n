using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDNoteOpenAPI4N.DataModel;
using YDNoteOpenAPI4N.YDAPI;
using YDNoteOpenAPI4N.YDConsumer;

namespace YDNoteOpenAPI4N.Test.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        private string AccessToken
        {
            get { return (string)Session["AccessToken"]; }
            set { Session["AccessToken"] = value; }
        }


        private YDTokenManager TokenManager
        {
            get
            {
                var tokenManager = (YDTokenManager)Application["tokenManager"];
                if (tokenManager == null)
                {
                    string consumerKey = YDNoteOpenAPI4N.YDAuthBaseInfo.ConsumerKey;
                    string consumerSecret = YDNoteOpenAPI4N.YDAuthBaseInfo.ConsumerSecret;
                    if (!string.IsNullOrEmpty(consumerKey))
                    {
                        tokenManager = new YDTokenManager(consumerKey, consumerSecret);
                        Application["tokenManager"] = tokenManager;
                    }
                }

                return tokenManager;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);

                var accessTokenResponse = youDao.ProcessUserAuthorization();
                if (accessTokenResponse != null)
                {
                    this.AccessToken = accessTokenResponse.AccessToken;
                    this.lbl.Text = "Token:" + this.AccessToken + " Screct:" + this.TokenManager.GetTokenSecret(this.AccessToken);
                }
                else if (this.AccessToken == null)
                {
                    // If we don't yet have access, immediately request it.
                    YDWebConsumer.RequestAuthorization(youDao);
                }
            }
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);
            var api = new YDUserAPI(youDao,this.AccessToken);
            var ydUser = api.GetUserInfo();
            this.lbl.Text = "user:" + ydUser.user;

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);
            var api = new YDNoteBookAPI(youDao,this.AccessToken);
            var books = api.GetAllNoteBooks();
           
            foreach (var ydNoteBook in books)
            {
                this.lbl.Text += ydNoteBook.path+"<\br>";
            }
           
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);
            var api = new YDNoteBookAPI(youDao, this.AccessToken);
            var noteBooks = api.GetAllNoteBooks();
            var noteBook = noteBooks[1];

            var notes = api.GetNotesInBook(noteBook.path);
            var note = notes[0];
            this.lbl.Text = note.content;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);
            var api = new YDNoteBookAPI(youDao, this.AccessToken);
            var newBook = api.CreateNoteBook(DateTime.Now.ToString());
            this.lbl.Text = newBook.name;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);
            var api = new YDNoteBookAPI(youDao, this.AccessToken);
            var books = api.GetAllNoteBooks();
            api.DeleteNoteBook(books[1].path);
            this.lbl.Text = "DELETE SUCCESS";
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);
            var noteApi = new YDNoteAPI(youDao, this.AccessToken);
            var note = new YDNote()
                           {
                              source="",author = "kklldog",
                              content = "<content>内容</content><br/><finishtime>ttt</finishtime>",
                              notebook ="",title = "testNote1"
                           };
            var newNote = noteApi.CreateNote(note);
            this.lbl.Text = newNote.content + newNote.create_time;
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);
            var noteApi = new YDNoteAPI(youDao,this.AccessToken);
            string path = "/6E45D26BEB6943C4BF4CAF420AD03022/CC13776676444A3AA7416465266AE6BE";
            var newNote = noteApi.GetNote(path);
            this.lbl.Text = newNote.content;
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);
            var noteApi = new YDNoteAPI(youDao,this.AccessToken);
            string path = "/6E45D26BEB6943C4BF4CAF420AD03022/CC13776676444A3AA7416465266AE6BE";
            var newNote = noteApi.GetNote(path);
            newNote.content = "<html><body>去你妈的</body></html>";
            newNote.title = "test";
            noteApi.UpdateNote(newNote);
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);
            var noteApi = new YDNoteAPI(youDao, this.AccessToken);
            this.lbl.Text = noteApi.MoveNote("/6E45D26BEB6943C4BF4CAF420AD03022/CC13776676444A3AA7416465266AE6BE", "/8F970E0A60B743408C9F4B85B2FC081E").modify_time.ToString();
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);
            var noteApi = new YDNoteAPI(youDao,this.AccessToken);
            var note = noteApi.GetNote("/8F970E0A60B743408C9F4B85B2FC081E/CC13776676444A3AA7416465266AE6BE");
            var noteBookApi = new YDNoteBookAPI(youDao, this.AccessToken);
            var books = noteBookApi.GetAllNoteBooks();
            this.lbl.Text = noteApi.MoveNote(note, books[0]).path;
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);
            var bookApi = new YDNoteBookAPI(youDao, this.AccessToken);
            var books = bookApi.GetAllNoteBooks();
            var notes = bookApi.GetNotesInBook(books[0].path);
            var noteApi = new YDNoteAPI(youDao, this.AccessToken);
            noteApi.DeleteNote(notes[0]);
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);
            var bookApi = new YDNoteBookAPI(youDao, this.AccessToken);
            var books = bookApi.GetAllNoteBooks();
            var notes = bookApi.GetNotesInBook(books[0].path);
            var noteApi = new YDNoteAPI(youDao, this.AccessToken);
            var note = noteApi.GetNote(notes[0].path);
            noteApi.DeleteNote(note);
        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile.ContentLength > 0)
            {
                var youDao = new YDWebConsumer(YDAuthBaseInfo.ServiceDescription, this.TokenManager);
                var resourceApi = new YDResourceAPI(youDao, this.AccessToken);

                var resource = resourceApi.Upload(Path.GetFileName(FileUpload1.PostedFile.FileName),
                   FileUpload1.PostedFile.ContentType, FileUpload1.PostedFile.InputStream);

                if (resource!=null)
                {
                    Button13.Text = resource.url;
                }
            }  
        }
        


    }
}
