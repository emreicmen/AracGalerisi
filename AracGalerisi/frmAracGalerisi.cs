using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracGalerisi
{
    public partial class frmArac : Form
    {

        private List<AracGalerisi> aracListesi = null;
        public frmArac()
        {
            InitializeComponent();
        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            //Listeye ekleyen fonksiyon
            AracGalerisi arac = new AracGalerisi()
            {
                AracinTuru = (AracTuru)cmbArac.SelectedIndex,
                IsGaranti = chkGaranti.Checked,
                KasaTip = (KasaTipi)cmbKasa.SelectedIndex,
                Markasi = (Marka)cmbMarka.SelectedIndex,
                Modeli = (Model)cmbModel.SelectedIndex,
                ModelYili = (int)numYil.Value,
                MotorGucu = (int)numMotor.Value,
                Renk = (Color)lblRenk.BackColor,
                Sanzimani = (SanzimanTuru)cmbSanziman.SelectedIndex,
                YakitTipi = (YakitTuru)cmbYakit.SelectedIndex,
                AracResmi = pictureBox.Image

            };
            lstArac.Items.Add(arac);

        }

        private void frmArac_Load(object sender, EventArgs e)
        {

            //Enviroment.CurrentDirectory projenın kayıtlı olduğu dosya klasörüdür. .Parent deyince üst klasöre çıkar.ÜSt klasör solution directory'dir.
            //FullName komutu da c\Kulanıcılar\Destktop gibi kalsör dizininin adını alır.

            string rsmYol = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            rsmYol += "/Image ";                         // klasör açılacak yolu alıyor. Altına Image klasörü ekleneceği bilgisi veriliyor. 

            if (!Directory.Exists(rsmYol))
            {
                Directory.CreateDirectory(rsmYol);      // Alınan klasör path'inde klasör açtı.
            }

            foreach (Control item in grbArac.Controls)
            {
                if (item is ComboBox)
                {
                    (item as ComboBox).DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }

            openFileDialog1.InitialDirectory = Application.ExecutablePath;              // bilgisayatın dozya gezgını acıp soya secmemızı sağlar.InıtalDirectory İlk acıldıgında hangı klasoru gostersın ılk.
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;                      // pıcturebox ıcındekı resım nasıl gosterılecek.Dolduracak mı kesecek mı
            btnRenk.TextAlign = ContentAlignment.BottomLeft;                            
            lblRenk.BorderStyle = BorderStyle.FixedSingle;                              //FizedSzie'da büyütülmez.Sabit kalır
            colorDialog1.FullOpen = true;                                               // color dialog açılınca tüm renkleri gösterecek şelilde aç komutu
            numYil.Maximum = DateTime.Now.Year + 1;
            numYil.Minimum = 2005;
            numMotor.Maximum = 5000;
            numMotor.Minimum = 1000;
            numMotor.Increment = 100;                                                   //ok tuşlarına basınca kaçar kaçar artsın

            EnumYukle(Enum.GetNames(typeof(AracTuru)),cmbArac);
            EnumYukle(Enum.GetNames(typeof(Marka)), cmbMarka);
            EnumYukle(Enum.GetNames(typeof(Model)), cmbModel);
            EnumYukle(Enum.GetNames(typeof(SanzimanTuru)), cmbSanziman);
            EnumYukle(Enum.GetNames(typeof(YakitTuru)), cmbYakit);
            EnumYukle(Enum.GetNames(typeof(KasaTipi)), cmbKasa);

        }

        private void EnumYukle(string[] Enums, ComboBox cmb)
        {
            cmb.Items.AddRange(Enums);
            cmb.SelectedIndex = 0;
        }

        private void btnRenk_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            lblRenk.BackColor = colorDialog1.Color;
        }

        private void chkGaranti_CheckedChanged(object sender, EventArgs e)
        {
            chkGaranti.Text = chkGaranti.Checked ? "Garantisi var" : "Garantisi yok";                               // ? if   : else demek
        }

        private void lstArac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                lstArac.Items.Remove(lstArac.SelectedItem);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Image resim = Image.FromFile(openFileDialog1.FileName);             //File'dan alınan image'i resim değerine atıyor
            pictureBox.Image = btnKucukResim.BackgroundImage = resim;
        }

        private void btnResim_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void btnKucukResim_DragEnter(object sender, DragEventArgs e)
        {
            object suruklenenSey = e.Data.GetData(DataFormats.FileDrop);
            try
            {
                Image resimDosyasi = Image.FromFile(((string[])suruklenenSey)[0]);
                btnKucukResim.BackgroundImage = resimDosyasi;
                btnKucukResim.BackgroundImageLayout = ImageLayout.Stretch;

                pictureBox.Image = resimDosyasi;
                pictureBox.BackgroundImageLayout = ImageLayout.Stretch;


            }
            catch (Exception)
            {

                MessageBox.Show("Atılan dosyanın bir resim dosyası olduğundan emin misiniz.\nLütfen tekrar deneyiniz!", "Hata");
            }
        }

        private void lstArac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstArac.SelectedIndex != -1)
            {
                AracClassDoldur();
            }
        }

        private void AracClassDoldur()
        {
            AracGalerisi secilenArac = (AracGalerisi)lstArac.SelectedItem;
            cmbArac.SelectedIndex = (int)secilenArac.AracinTuru;
            chkGaranti.Checked = secilenArac.IsGaranti;
            cmbKasa.SelectedIndex = (int)secilenArac.KasaTip;
            cmbMarka.SelectedIndex = (int)secilenArac.Markasi;
            cmbModel.SelectedIndex = (int)secilenArac.Modeli;
            numYil.Value = secilenArac.ModelYili;
            numMotor.Value = secilenArac.MotorGucu;
            lblRenk.BackColor = secilenArac.Renk;
            cmbSanziman.SelectedIndex = (int)secilenArac.Sanzimani;
            cmbYakit.SelectedIndex = (int)secilenArac.YakitTipi;
            pictureBox.Image = btnKucukResim.BackgroundImage = secilenArac.AracResmi;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            lstArac.Items.Remove(lstArac.SelectedItem);

        }
        private void FillProductListBox()
        {
            lstArac.Items.Clear();

            foreach (AracGalerisi item in aracListesi)
            {
                lstArac.Items.Add(("\t"));
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            AracGalerisi arac = new AracGalerisi()
            {
                AracinTuru = (AracTuru)cmbArac.SelectedIndex,
                IsGaranti = chkGaranti.Checked,
                KasaTip = (KasaTipi)cmbKasa.SelectedIndex,
                Markasi = (Marka)cmbMarka.SelectedIndex,
                Modeli = (Model)cmbModel.SelectedIndex,
                ModelYili = (int)numYil.Value,
                MotorGucu = (int)numMotor.Value,
                Renk = (Color)lblRenk.BackColor,
                Sanzimani = (SanzimanTuru)cmbSanziman.SelectedIndex,
                YakitTipi = (YakitTuru)cmbYakit.SelectedIndex,
                AracResmi = pictureBox.Image
            };
            lstArac.Items[lstArac.SelectedIndex] = arac;
        }

        private void cmbArac_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
