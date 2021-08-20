using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Media;
using System.Drawing.Text;
using System.Linq;

namespace MenuDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            for (int i = 2; i < 70; i+=2)
            {
                toolStripComboBox1.Items.Add(i);
            }
            toolStripComboBox1.Text = "10";

            //прописуємо назву формату тексту в ComboBox
            var fonts = new InstalledFontCollection();
            toolStripComboBox2.Items.AddRange(fonts.Families.Select(x => x.Name).ToArray());
        }
        // налаштування кольору тексту
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // FontDialog
            // ColorDialog
            // SaveFileDialog
            // OpenFileDialog
            // FolderBrowserDialog

            //налаштовуємо втиснення клавіши
            var fontColorBtn = (sender as ToolStripButton);
            fontColorBtn.Checked = !fontColorBtn.Checked;

            var fontDialog = new FontDialog();
            fontDialog.ShowColor = true;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                if (richTextBox1.SelectedText.Length > 0)
                {
                    richTextBox1.SelectionFont = fontDialog.Font;
                    richTextBox1.SelectionColor = fontDialog.Color;
                }
                else
                {
                    //richTextBox1.SelectAll();
                    richTextBox1.Font = fontDialog.Font;
                    richTextBox1.ForeColor = fontDialog.Color;
                }
            }
        }
        // пункт меню збереження файла
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(saveDialog.FileName) == ".txt") // перевірка на розширення файла
                {
                    richTextBox1.SaveFile(saveDialog.FileName, RichTextBoxStreamType.UnicodePlainText); //enum RichTextBoxStreamType - дозволяє зберегти різний формат тексту
                }
                else
                {
                    richTextBox1.SaveFile(saveDialog.FileName);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // відкриття вікна OpenFileDialog, де по замовчуванням відкриваються "Мої документи"
            openDialog.Filter = "All files|*.*|Text documents|*.txt|RTF|*.rtf"; // фільтри для OpenFileDialog
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(openDialog.FileName);
            }
        }
        // курсивний текст
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //налаштовуємо втиснення клавіши
            var italicBtn = (sender as ToolStripButton);
            italicBtn.Checked = !italicBtn.Checked;
            if (richTextBox1.SelectedText.Length > 0)
            {
                if (richTextBox1.SelectionFont.Italic)
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style & ~FontStyle.Italic);
                }
                else
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style | FontStyle.Italic);
                }
            }
            else
            {
                richTextBox1.Font = new Font(richTextBox1.Font, FontStyle.Italic);
            }
        }
        // жирний текст
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // FontStyle.Regular
            // enum 0 1 2
            // 00000000 regular
            // 00000001 italic
            // 00000010 bold

            // italic | bold
            // 11111100
            //налаштовуємо втиснення клавіши
            var boldBtn = (sender as ToolStripButton);
            boldBtn.Checked = !boldBtn.Checked;

            if (richTextBox1.SelectedText.Length > 0)
            {
                if (richTextBox1.SelectionFont.Bold)
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style & ~FontStyle.Bold);
                }
                else
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style | FontStyle.Bold);
                }
            }
            else
            {
                richTextBox1.Font = new Font(richTextBox1.Font, FontStyle.Bold);
            }
        }
        //підкреслювання тексту
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //налаштовуємо втиснення клавіши
            var underlineBtn = (sender as ToolStripButton);
            underlineBtn.Checked = !underlineBtn.Checked;

            if (richTextBox1.SelectedText.Length > 0)
            {
                if (richTextBox1.SelectionFont.Underline)
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style & ~FontStyle.Underline);
                }
                else
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, richTextBox1.SelectionFont.Style | FontStyle.Underline);
                }
            }
            else
            {
                richTextBox1.Font = new Font(richTextBox1.Font, FontStyle.Underline);
            }
        }
        //пункт меню, який закриває форму
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //зміна кольору фону текста
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //налаштовуємо втиснення клавіши
            var backColorBtn = (sender as ToolStripButton);
            backColorBtn.Checked = !backColorBtn.Checked;

            var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (richTextBox1.SelectedText.Length > 0)
                {
                    richTextBox1.SelectionBackColor = colorDialog.Color;
                }
                else
                {
                    richTextBox1.BackColor = colorDialog.Color;
                }
            }
        }

        // Пункт меню, який очищає richTextBox1
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
        //Розмір шрифта через подію
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, float.Parse(toolStripComboBox1.Text), richTextBox1.SelectionFont.Style);
            }
            else
            {
                richTextBox1.Font = new Font(richTextBox1.SelectionFont.FontFamily, float.Parse(toolStripComboBox1.Text), richTextBox1.SelectionFont.Style);
            }
        }
        // відображення тексту з ліва
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //налаштовуємо втиснення клавіши
            var leftTextBtn = (sender as ToolStripButton);
            leftTextBtn.Checked = !leftTextBtn.Checked;
            if (richTextBox1.SelectedText.Length > 0)
            {
                //richTextBox1.SelectAll();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            }
        }

        // відображення тексту по центру
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            //налаштовуємо втиснення клавіши
            var centerTextBtn = (sender as ToolStripButton);
            centerTextBtn.Checked = !centerTextBtn.Checked;
            if (richTextBox1.SelectedText.Length > 0)
            {
                //richTextBox1.SelectAll();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            }
        }

        // відображення тексту з права
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            //налаштовуємо втиснення клавіши
            var leftTextBtn = (sender as ToolStripButton);
            leftTextBtn.Checked = !leftTextBtn.Checked;
            if (richTextBox1.SelectedText.Length > 0)
            {
                //richTextBox1.SelectAll();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
            }
        }
        //за допомогою події в toolStripComboBox2 виділеному тексту змінюємо формат тексту
        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(toolStripComboBox2.SelectedItem.ToString(), richTextBox1.SelectionFont.Size);
        }
        //кнопка включення/виключення форматування абзаців як списку
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            //налаштовуємо втиснення клавіши
            var abzacTextBtn = sender as ToolStripButton;
            abzacTextBtn.Checked = !abzacTextBtn.Checked;
            richTextBox1.SelectionIndent += 20;
        }

        //пункт меню Copy
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText);
        }

        //пункт меню Paste
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = Clipboard.GetText();
        }

        //пункт меню CutOut (вирізати)
        private void coutOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        //пункт меню SelectAll (Виділити все)
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }
    }
}
