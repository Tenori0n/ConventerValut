using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ConventerValut
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ConventerValutViewModel();
        }
    }

    public class ConventerValutViewModel : INotifyPropertyChanged
    {
        private string? entryText;
        private string? labelText;

        public ConventerValutViewModel()
        {
            DatePickerSelect = DateTime.Today;
            LastDatePickerSelect = DatePickerSelect;
            Valuti = new ObservableCollection<ValyutaInfo>();
            _ = GetJson();
            Debug.WriteLine("DONE");
            Convert = new Command<string>(async x => {
                if (LastDatePickerSelect != DatePickerSelect)
                {
                    await UpdateJson();
                    Debug.WriteLine("Updated");
                }
                LabelText = (double.Parse(x) * (PickerSelect1.Value/PickerSelect1.Nominal) / (PickerSelect2.Value / PickerSelect2.Nominal)).ToString(); }, x => string.IsNullOrWhiteSpace(x) == false);
        }
        public ICommand Convert { get; }
        public async Task GetJson()
        {
            var date = DatePickerSelect;
            string MainUrl = "https://www.cbr-xml-daily.ru" + "/archive/" + date.Year + "/" + date.Month.ToString().PadLeft(2, '0') + "/" + date.Day.ToString().PadLeft(2, '0') + "/daily_json.js";
            try
            {
                using (var client = new HttpClient())
                { 
                    HttpResponseMessage response = await client.GetAsync(MainUrl);
                    int i = 1;
                    while (response.StatusCode != HttpStatusCode.OK)
                    {
                        MainUrl = "https://www.cbr-xml-daily.ru" + "/archive/" + date.Year + "/" + date.Month.ToString().PadLeft(2, '0') + "/" + (date.Day-i).ToString().PadLeft(2, '0') + "/daily_json.js";
                        response = await client.GetAsync(MainUrl);
                        i++;
                    }
                    using (HttpContent content = response.Content)
                    {
                        var temp = await response.Content.ReadFromJsonAsync<Rootobject>();
                        {
                            Valuti.Add(new ValyutaInfo("R00001", "001", "RUB", 1, "Российский рубль", 1, 1));
                            Valuti.Add(temp.Valute.AED);
                            Valuti.Add(temp.Valute.AMD);
                            Valuti.Add(temp.Valute.AUD);
                            Valuti.Add(temp.Valute.AZN);
                            Valuti.Add(temp.Valute.BGN);
                            Valuti.Add(temp.Valute.BRL);
                            Valuti.Add(temp.Valute.BYN);
                            Valuti.Add(temp.Valute.CAD);
                            Valuti.Add(temp.Valute.CHF);
                            Valuti.Add(temp.Valute.CNY);
                            Valuti.Add(temp.Valute.CZK);
                            Valuti.Add(temp.Valute.DKK);
                            Valuti.Add(temp.Valute.EGP);
                            Valuti.Add(temp.Valute.EUR);
                            Valuti.Add(temp.Valute.GBP);
                            Valuti.Add(temp.Valute.GEL);
                            Valuti.Add(temp.Valute.HKD);
                            Valuti.Add(temp.Valute.HUF);
                            Valuti.Add(temp.Valute.IDR);
                            Valuti.Add(temp.Valute.INR);
                            Valuti.Add(temp.Valute.JPY);
                            Valuti.Add(temp.Valute.KGS);
                            Valuti.Add(temp.Valute.KRW);
                            Valuti.Add(temp.Valute.KZT);
                            Valuti.Add(temp.Valute.MDL);
                            Valuti.Add(temp.Valute.NOK);
                            Valuti.Add(temp.Valute.NZD);
                            Valuti.Add(temp.Valute.PLN);
                            Valuti.Add(temp.Valute.QAR);
                            Valuti.Add(temp.Valute.RON);
                            Valuti.Add(temp.Valute.RSD);
                            Valuti.Add(temp.Valute.SEK);
                            Valuti.Add(temp.Valute.SGD);
                            Valuti.Add(temp.Valute.THB);
                            Valuti.Add(temp.Valute.TJS);
                            Valuti.Add(temp.Valute.TMT);
                            Valuti.Add(temp.Valute.TRY);
                            Valuti.Add(temp.Valute.UAH);
                            Valuti.Add(temp.Valute.USD);
                            Valuti.Add(temp.Valute.UZS);
                            Valuti.Add(temp.Valute.VND);
                            Valuti.Add(temp.Valute.XDR);
                            Valuti.Add(temp.Valute.ZAR);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
        }
        public async Task UpdateJson()
        {
            var date = DatePickerSelect;
            string MainUrl = "https://www.cbr-xml-daily.ru" + "/archive/" + date.Year + "/" + date.Month.ToString().PadLeft(2, '0') + "/" + date.Day.ToString().PadLeft(2, '0') + "/daily_json.js";
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(MainUrl);
                    int i = 1;
                    while (response.StatusCode != HttpStatusCode.OK)
                    {
                        MainUrl = "https://www.cbr-xml-daily.ru" + "/archive/" + date.Year + "/" + date.Month.ToString().PadLeft(2, '0') + "/" + (date.Day - i).ToString().PadLeft(2, '0') + "/daily_json.js";
                        response = await client.GetAsync(MainUrl);
                        i++;
                    }
                    using (HttpContent content = response.Content)
                    {
                        var temp = await response.Content.ReadFromJsonAsync<Rootobject>();
                        {
                            Valuti[1].Value = temp.Valute.AED.Value;
                            Valuti[2].Value = temp.Valute.AMD.Value;
                            Valuti[3].Value = temp.Valute.AUD.Value;
                            Valuti[4].Value = temp.Valute.AZN.Value;
                            Valuti[5].Value = temp.Valute.BGN.Value;
                            Valuti[6].Value = temp.Valute.BRL.Value;
                            Valuti[7].Value = temp.Valute.BYN.Value;
                            Valuti[8].Value = temp.Valute.CAD.Value;
                            Valuti[9].Value = temp.Valute.CHF.Value;
                            Valuti[10].Value = temp.Valute.CNY.Value;
                            Valuti[11].Value = temp.Valute.CZK.Value;
                            Valuti[12].Value = temp.Valute.DKK.Value;
                            Valuti[13].Value = temp.Valute.EGP.Value;
                            Valuti[14].Value = temp.Valute.EUR.Value;
                            Valuti[15].Value = temp.Valute.GBP.Value;
                            Valuti[16].Value = temp.Valute.GEL.Value;
                            Valuti[17].Value = temp.Valute.HKD.Value;
                            Valuti[18].Value = temp.Valute.HUF.Value;
                            Valuti[19].Value = temp.Valute.IDR.Value;
                            Valuti[20].Value = temp.Valute.INR.Value;
                            Valuti[21].Value = temp.Valute.JPY.Value;
                            Valuti[22].Value = temp.Valute.KGS.Value;
                            Valuti[23].Value = temp.Valute.KRW.Value;
                            Valuti[24].Value = temp.Valute.KZT.Value;
                            Valuti[25].Value = temp.Valute.MDL.Value;
                            Valuti[26].Value = temp.Valute.NOK.Value;
                            Valuti[27].Value = temp.Valute.NZD.Value;
                            Valuti[28].Value = temp.Valute.PLN.Value;
                            Valuti[29].Value = temp.Valute.QAR.Value;
                            Valuti[30].Value = temp.Valute.RON.Value;
                            Valuti[31].Value = temp.Valute.RSD.Value;
                            Valuti[32].Value = temp.Valute.SEK.Value;
                            Valuti[33].Value = temp.Valute.SGD.Value;
                            Valuti[34].Value = temp.Valute.THB.Value;
                            Valuti[35].Value = temp.Valute.TJS.Value;
                            Valuti[36].Value = temp.Valute.TMT.Value;
                            Valuti[37].Value = temp.Valute.TRY.Value;
                            Valuti[38].Value = temp.Valute.UAH.Value;
                            Valuti[39].Value = temp.Valute.USD.Value;
                            Valuti[40].Value = temp.Valute.UZS.Value;
                            Valuti[41].Value = temp.Valute.VND.Value;
                            Valuti[42].Value = temp.Valute.XDR.Value;
                            Valuti[43].Value = temp.Valute.ZAR.Value;
                        }
                        LastDatePickerSelect = DatePickerSelect;
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
        }
        public ObservableCollection<ValyutaInfo> Valuti { get ; set;}
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string? EntryText { get => entryText; set { entryText = value; OnPropertyChanged(); } }
        public string? LabelText { get => labelText; set { labelText = value; OnPropertyChanged(); } }
        public ValyutaInfo PickerSelect1 { get; set;}
        public ValyutaInfo PickerSelect2 { get; set;}
        public DateTime DatePickerSelect { get; set;}
        public DateTime LastDatePickerSelect { get; set;}
    }
    public class Rootobject
    {
        public DateTime Date { get; set; }
        public DateTime PreviousDate { get; set; }
        public string PreviousURL { get; set; }
        public DateTime Timestamp { get; set; }
        public Valute Valute { get; set; }
    }
    public class Valute
    {
        public ValyutaInfo AUD { get; set; }
        public ValyutaInfo AZN { get; set; }
        public ValyutaInfo GBP { get; set; }
        public ValyutaInfo AMD { get; set; }
        public ValyutaInfo BYN { get; set; }
        public ValyutaInfo BGN { get; set; }
        public ValyutaInfo BRL { get; set; }
        public ValyutaInfo HUF { get; set; }
        public ValyutaInfo VND { get; set; }
        public ValyutaInfo HKD { get; set; }
        public ValyutaInfo GEL { get; set; }
        public ValyutaInfo DKK { get; set; }
        public ValyutaInfo AED { get; set; }
        public ValyutaInfo USD { get; set; }
        public ValyutaInfo EUR { get; set; }
        public ValyutaInfo EGP { get; set; }
        public ValyutaInfo INR { get; set; }
        public ValyutaInfo IDR { get; set; }
        public ValyutaInfo KZT { get; set; }
        public ValyutaInfo CAD { get; set; }
        public ValyutaInfo QAR { get; set; }
        public ValyutaInfo KGS { get; set; }
        public ValyutaInfo CNY { get; set; }
        public ValyutaInfo MDL { get; set; }
        public ValyutaInfo NZD { get; set; }
        public ValyutaInfo NOK { get; set; }
        public ValyutaInfo PLN { get; set; }
        public ValyutaInfo RON { get; set; }
        public ValyutaInfo XDR { get; set; }
        public ValyutaInfo SGD { get; set; }
        public ValyutaInfo TJS { get; set; }
        public ValyutaInfo THB { get; set; }
        public ValyutaInfo TRY { get; set; }
        public ValyutaInfo TMT { get; set; }
        public ValyutaInfo UZS { get; set; }
        public ValyutaInfo UAH { get; set; }
        public ValyutaInfo CZK { get; set; }
        public ValyutaInfo SEK { get; set; }
        public ValyutaInfo CHF { get; set; }
        public ValyutaInfo RSD { get; set; }
        public ValyutaInfo ZAR { get; set; }
        public ValyutaInfo KRW { get; set; }
        public ValyutaInfo JPY { get; set; }
    }
    public class ValyutaInfo
    {
        public string? ID {get; set;}
        public string? NumCode {get; set;}
        public string? CharCode {get; set; }
        public int Nominal {get; set;}
        public string? Name{get; set;}
        public float? Value { get; set; }
        public float? Previous {get; set;}

        public ValyutaInfo(string? iD, string? numCode, string? charCode, int nominal, string? name, float? value, float? previous)
        {
            ID = iD;
            NumCode = numCode;
            CharCode = charCode;
            Nominal = nominal;
            Name = name;
            Value = value;
            Previous = previous;
        }
    }
}
