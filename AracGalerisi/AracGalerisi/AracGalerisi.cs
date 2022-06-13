using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracGalerisi
{
    #region ENUM Listelerimiz
    public enum Marka
    {
        FIAT, FROD, RENO, VW
    }

    public enum Model
    {
        DOBLO, LINEA, POLO, FIESTA, FOCUS, MUSTANG,
        CLIO, FLEUNCE, KANGOO, GOLF, PASSAT, CADDY
    }
    public enum AracTuru
    {
        Binek, Ticari
    }

    public enum SanzimanTuru
    {
        Düz, Otomatik, YariOtomatik
    }
    public enum YakitTuru
    { Benzinli, Dizel, LPG, Hibrit, Elektirkli }

    public enum KasaTipi
    {
        Kamyonet, Otobüs, Minibüs, CityVan, Coupe, Sedan, MPV, SUV, Station
    }
    #endregion

    class AracGalerisi
    {
        #region Enum Tipliler
        public Marka Markasi { get; set; }
        public Model Modeli { get; set; }
        public AracTuru AracinTuru { get; set; }
        public SanzimanTuru Sanzimani { get; set; }
        public YakitTuru YakitTipi { get; set; }
        public KasaTipi KasaTip { get; set; }
        #endregion

        #region Bir işleme tabi tutulmayacaklar
        public bool IsGaranti { get; set; }
        public Color Renk { get; set; }
        public Image AracResmi { get; set; }
        #endregion

        #region Kontrollu get set olanlar
        private int _modelYili;
        public int ModelYili
        {
            get { return _modelYili; }
            set
            {
                if (value < 2005)
                {
                    throw new Exception("2005 yılından eski araç kabul etmiyoruz");
                }
                else
                {
                    _modelYili = value;
                }
            }
        }

        private int _motorGucu;
        public int MotorGucu
        {
            get { return _motorGucu; }
            set
            {
                if (value < 1000)
                {
                    throw new Exception("Aracın motoru 1000cc\'den küçük olamaz.");
                }
                else
                {
                    _motorGucu = value;
                }
            }
        }
        #endregion

        #region Virtual ToString() metodunun ezilmesi
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Markasi);
            sb.Append(" ");
            sb.Append(Modeli);
            sb.Append(" ");
            sb.Append(AracinTuru);
            sb.Append(" ");
            sb.Append(YakitTipi);
            return sb.ToString();
        }
        #endregion

        #region Constructor'lar
        public AracGalerisi()
        { }

        public AracGalerisi(Marka marka, Model model)
        {
            this.Markasi = marka;
            this.Modeli = model;
        }

        public AracGalerisi(Color renk, int modelYili, Image aracResmi, AracTuru aracTuru = AracTuru.Ticari, Marka marka = Marka.VW, Model model = Model.CADDY, SanzimanTuru sanziman = SanzimanTuru.Otomatik, YakitTuru yakitTipi = YakitTuru.Dizel, KasaTipi kasaTipi = KasaTipi.Kamyonet, bool isGaranti = true)
        {
            this.Renk = renk;
            this.ModelYili = modelYili;
            this.AracResmi = aracResmi;
            this.AracinTuru = aracTuru;
            this.Markasi = marka;
            this.Modeli = model;
            this.Sanzimani = sanziman;
            this.YakitTipi = yakitTipi;
            this.KasaTip = kasaTipi;
            this.IsGaranti = isGaranti;
        }
        #endregion
    }
}
