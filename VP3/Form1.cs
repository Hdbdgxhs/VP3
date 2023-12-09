using Newtonsoft.Json;

namespace VP3
{
    public partial class Form1 : Form
    {
        Json1 json = new Json1();
        private string Filename;
        public Form1()
        {
            InitializeComponent();
            this.Filename = null;
            foreach (Control c in this.Controls) // this is the form object on which Controls is the ControlCollection
            {
                if ((c is Button) && (c.TabIndex < 16)) //(c.TabIndex>3)&&(c.TabIndex < 20)
                {
                    c.BackColor = Color.White;
                    json.Color[c.TabIndex] = Color.White;
                }
            }
        }

        private void New_file()
        {
            var result = MessageBox.Show("Сохранить", "Сохранить файл?",
                                 MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) { Save_file(); }
            this.json = new Json1();
            this.Filename = null;
            foreach (Control c in this.Controls)
            {
                if ((c is Button) && (c.TabIndex < 16))
                {
                    c.BackColor = Color.White;
                    json.Color[c.TabIndex] = Color.White;
                }
            }
        }

        private void Open_file()
        {
            var result = MessageBox.Show("Сохранить", "Сохранить файл?",
                                             MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) { Save_file(); }
            openFileDialog1.ShowDialog();
            this.Filename = openFileDialog1.FileName;
            string Text = File.ReadAllText(this.Filename);
            this.json = JsonConvert.DeserializeObject<Json1>(Text);
            Color Col = new Color();
            foreach (Control c in this.Controls) // this is the form object on which Controls is the ControlCollection
            {
                if ((c is Button)&&(c.TabIndex<16))
                {
                    Col = json.Color[c.TabIndex];
                    c.BackColor = Col;
                }
            }
        }
        private void Save_file()
        {
            if (this.Filename == null) { Save_how_file(); return; }
            string Text = JsonConvert.SerializeObject(json);
            File.WriteAllText(this.Filename, Text);
        }

        private void Save_how_file()
        {
            saveFileDialog1.ShowDialog();
            this.Filename = saveFileDialog1.FileName;
            string Text = JsonConvert.SerializeObject(json);
            File.WriteAllText(this.Filename, Text);
        }

        private void newbtn_Click(object sender, EventArgs e)
        {
            New_file();
        }

        private void openbtn_Click(object sender, EventArgs e)
        {
            Open_file();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            Save_file();
        }

        private void savehowbtn_Click(object sender, EventArgs e)
        {
            Save_how_file();
        }

        private void Set_color(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            ((Button)sender).BackColor = colorDialog1.Color;
            json.Color[((Button)sender).TabIndex] = colorDialog1.Color;
        }
    }
    [Serializable]
        public class Json1
        {
            public Color[] Color = new Color[16];
        }
}
