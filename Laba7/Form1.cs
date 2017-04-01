using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Laba7
{
    public partial class Form1 : Form
    {
        private List<Owner> listOwners;
        private List<Operation> listOperations;
        private List<Account> listAccounts;

        readonly XmlSerializer serializerOwners = new XmlSerializer(typeof(List<Owner>));
        readonly XmlSerializer serializerOperations = new XmlSerializer(typeof(List<Operation>));
        readonly XmlSerializer serializerAccounts = new XmlSerializer(typeof(List<Account>));

        private readonly string Owners = @"Owners.xml";
        private readonly string Operations = @"Operations.xml";
        private readonly string Accounts = @"Accounts.xml";

        public Form1()
        {
            InitializeComponent();
            trackBar1.Scroll += scroll_Click;
            label1.Text = "0";
            listOwners = new List<Owner>();
            listOperations = new List<Operation>();
            listAccounts = new List<Account>();
            try
            {
                using (FileStream stream = new FileStream(Owners, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    listOwners = serializerOwners.Deserialize(stream) as List<Owner>;

                }
                using (FileStream stream = new FileStream(Operations, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    listOperations = serializerOperations.Deserialize(stream) as List<Operation>;

                }
                using (FileStream stream = new FileStream(Accounts, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    listAccounts = serializerAccounts.Deserialize(stream) as List<Account>;

                }
                comboBox1.DataSource = listOwners;
                comboBox2.DataSource = listOperations;
            }
            catch (Exception)
            {
                
               
            }
        }

        private void scroll_Click(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Owner tempOwner = new Owner();
                if (textBox1.Text == "")
                {
                    throw new Exception("Enter FLS");
                }
                else
                {
                    tempOwner.FLS = textBox1.Text;
                }
                if (monthCalendar3.SelectionStart >= DateTime.Now )
                {
                    throw new Exception("Enter Birthday");
                }
                else
                {
                    tempOwner.DataOfBirthday = monthCalendar3.SelectionStart.Date.ToString();
                }
                if (textBox3.Text == "")
                {
                    throw new Exception("Enter Pasport");
                }
                else
                {
                    tempOwner.PasportInf = textBox3.Text;
                }
                listOwners.Add(tempOwner);
                comboBox1.DataSource = listOwners.ToArray();
                MessageBox.Show("Owner added");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Operation tempOperation = new Operation();
                if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
                {
                    throw new Exception("Choose TypeOfOperation");
                }
                else
                {
                    foreach (var i in groupBox1.Controls)
                    {
                        if (((RadioButton) i).Checked)
                        {
                            tempOperation.TypeOfOperation = ((RadioButton) i).Text;
                        }
                    }
                    
                }
                if (monthCalendar1.SelectionStart < DateTime.Now)
                {
                    throw new Exception("Enter DateOfOperation");
                }
                else
                {
                    tempOperation.DateOfOperation = monthCalendar1.SelectionStart.Date.ToString();
                }
                if (trackBar1.Value == 0)
                {
                    throw new Exception("Enter Sum");
                }
                else
                {
                    tempOperation.Sum = trackBar1.Value;
                }
                listOperations.Add(tempOperation);
                comboBox2.DataSource = listOperations.ToArray();
                MessageBox.Show("Operation added");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Account account = new Account();
                if (textBox4.Text == "")
                {
                    throw new Exception("Enter Balance");
                }
                else
                {
                    account.Balance = Convert.ToInt32(textBox4.Text);
                }
                if (monthCalendar2.SelectionStart >= DateTime.Now)
                {
                    throw new Exception("Enter Date Of Open");
                }
                else
                {
                   account.DataOfOpen = monthCalendar2.SelectionStart.Date.ToString();
                }
                if (textBox5.Text == "")
                {
                    throw new Exception("Enter Number of account");
                }
                else
                {
                    account.Number = Convert.ToInt32(textBox5.Text);
                }
                if (!radioButton8.Checked && !radioButton9.Checked && !radioButton10.Checked)
                {
                    throw new Exception("Choose TypeOfAccount");
                }
                else
                {
                    foreach (var i in groupBox4.Controls)
                    {
                        if (((RadioButton)i).Checked)
                        {
                             account.TypeofAmount= ((RadioButton)i).Text;
                        }
                    }

                }
                if (!radioButton5.Checked && !radioButton6.Checked)
                {
                    throw new Exception("Choose Sms Information");
                }
                else
                {
                    if (radioButton5.Checked)
                    {
                        account.Sms = false;
                    }
                    else account.Sms = true;

                }
                if (!radioButton4.Checked && !radioButton7.Checked)
                {
                    throw new Exception("Choose Sms Information");
                }
                else
                {
                    if (radioButton4.Checked)
                    {
                        account.InternetBanking = false;
                    }
                    else account.InternetBanking = true;

                }
                if (comboBox1.Items.Count == 0)
                {
                    throw new Exception("Owners are not added");
                }
                else
                {
                    account.OwnerTemp = comboBox1.SelectedItem as Owner;
                }
                if (comboBox2.Items.Count == 0)
                {
                    throw new Exception("Operation are not added");
                }
                else
                {
                    account.OperationTemp = comboBox2.SelectedItem as Operation;
                }
                listAccounts.Add(account);
                MessageBox.Show("Account added");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (FileStream stream = new FileStream(Owners, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    serializerOwners.Serialize(stream, listOwners);

                }
                using (FileStream stream = new FileStream(Operations, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    serializerOperations.Serialize(stream, listOperations); 

                }
                using (FileStream stream = new FileStream(Accounts, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    serializerAccounts.Serialize(stream, listAccounts);

                }
            }
            catch (Exception)
            {


            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = listAccounts;
        }
    }
}
