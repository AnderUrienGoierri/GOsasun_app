# 3.3. GOsasun App-aren Diseinua: Kodearen Egitura eta Arkitektura

Mahaigaineko aplikazioa garatzeko **C# programazio-hizkuntza** eta **Visual Studio** erabili dira, bereziki **Windows Forms (WinForms)** teknologiarekin. Aplikazio hau MVC (Model-View-Controller) eredu klasikoaren egokitzapen batean oinarritzen da, non datuak, logika eta interfazea argi eta garbi banatuta dauden.

Jarraian, diseinu arkitektoniko hau nola inplementatu den aztertuko dugu benetako kodearen "pantailazoak" (adibideak) eta azalpen zehatzak emanez.

## 1. Eredua (Model): Datuen Kapsulatzea
Modeloko klaseak datu-baseko entitateen errepresentazio zuzena dira memorian. Klase hauetan ez dago negozio-logikarik, datuak garraiatzeko egiturak (DTO - Data Transfer Objects) baizik ez dira.

**Kode pantailazoa: `Jarraipena.cs` eredua**
```csharp
using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Osasun jarraipen baten datuak biltzen dituen klasea.
    /// 'jarraipenak' taulari dagokio.
    /// </summary>
    public class Jarraipena
    {
        // Propietate automatikoak (Getters & Setters)
        public int Id { get; set; }
        public int PazienteId { get; set; }
        public string PazienteNan { get; set; } = string.Empty;
        public string PazienteIzena { get; set; } = string.Empty;
        public string PazienteAbizenak { get; set; } = string.Empty;
        
        // Nulugarriak (Nullable) diren eremuak datu-baseko balio nuluekin bat egiteko
        public int? OsasunLangileId { get; set; }
        public int? TentsioSistolikoa { get; set; }
        public int? TentsioDiastolikoa { get; set; }
        public decimal? PisuaKg { get; set; }
        public DateTime ErregistroData { get; set; } = DateTime.Now;

        // Propietate kalkulatua (Irakurtzeko soilik)
        public string PazienteIzenOsoa => $"{PazienteAbizenak}, {PazienteIzena}";

        public Jarraipena() { } // Eraikitzaile hutsa
    }
}
```
**Azalpena**:
- **Propietateak (`Get/Set`)**: Aldagai pribatuak ezkutatu eta haiek atzitzeko bide segurua eskaintzen dute. C#-ren propietate automatikoek kodea izugarri garbitzen dute.
- **Nullable motak (`int?`, `decimal?`)**: Galdera ikurrak aldagai batek `NULL` balioa har dezakeela adierazten du. Hau kritikoa da, paziente guztiei ez zaielako zertan pultsua edo altuera hartu jarraipen batean.
- **Propietate Kalkulatuak (`=>`)**: `PazienteIzenOsoa` bezalako propietateek ez dute memoriarik okupatzen; deitzen direnean kalkulatzen dira taula batean (DataGridView) erraz erakusteko.

## 2. Kontrolatzailea (Controller): Negozio-Logika
Kontrolatzaileek erabiltzailearen eta datu-basearen (Repositorioen) arteko zubi lana egiten dute. Beraiek kudeatzen dute erabiltzailearen ekintza bat baliozkoa den ala ez.

**Kode pantailazoa: `ErabiltzaileKontrolatzailea.cs` (Login Logika)**
```csharp
using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;
using GOsasun_app.Kontrola.Zerbitzuak;

namespace GOsasun_app.Kontrola
{
    public class ErabiltzaileKontrolatzailea
    {
        // Menpekotasunak hasieratzea
        private readonly ErabiltzaileDB _erabiltzaileDb = new ErabiltzaileDB();
        private readonly LoginBlokeoZerbitzua _loginBlokeoZerbitzua = new LoginBlokeoZerbitzua();

        /// <summary>
        /// Erabiltzailea datu-basean egiaztatzen du email eta pasahitz bidez.
        /// </summary>
        public LoginEmaitza Login(string emaila, string pasahitza)
        {
            // 1. Blokeo sistema egiaztatu
            LoginSegurtasunEgoera unekoEgoera = _loginBlokeoZerbitzua.LortuEgoera();
            if (unekoEgoera.Blokeatuta) {
                return new LoginEmaitza { Egoera = unekoEgoera };
            }

            // 2. Datu-basera joan erabiltzailea balioztatzeko
            Erabiltzailea? erabiltzailea = _erabiltzaileDb.Login(emaila, pasahitza);
            if (erabiltzailea != null) {
                _loginBlokeoZerbitzua.Berrezarri(); // Hutsegiteak garbitu
                return new LoginEmaitza {
                    Erabiltzailea = erabiltzailea,
                    Egoera = _loginBlokeoZerbitzua.LortuEgoera()
                };
            }

            // 3. Pasahitza okerra bada, hutsegitea erregistratu
            return new LoginEmaitza {
                Egoera = _loginBlokeoZerbitzua.ErregistratuHutsegitea()
            };
        }
    }
}
```
**Azalpena**:
Kontrolatzaile honek erakusten du aplikazioak ez duela zuzenean saioa ixten edo irekitzen UI mailan. Segurtasun kapa bat gehitzen dio: `LoginBlokeoZerbitzua`-ren bidez erasotzaile baten saio-hasiera intentoak mugatzen dira (Brute-force erasoen aurka). `Login()` metodoak objektu konplexu bat (`LoginEmaitza`) itzultzen du UI-ak mezu egokia erakutsi dezan.

## 3. Interfazea (View): Visual Studioko UI-a kodez kudeatzea
Visual Studioko diseinatzaile grafikoaz (Designer) gain, garrantzitsua da portaera dinamikoak kode bidez manipulatzea (Event-Driven Programming).

**Kode pantailazoa: `PazienteenZerrenda.cs` UI pertsonalizazioa**
```csharp
private void HasieratuPaginazioa()
{
    // Botoi baten diseinu modernoa (Flat UI) kode bidez
    _btnAurrekoOrria.Text = "Aurreko 10ak";
    _btnAurrekoOrria.Size = new Size(150, 40);
    _btnAurrekoOrria.FlatStyle = FlatStyle.Flat;
    _btnAurrekoOrria.FlatAppearance.BorderSize = 0;
    _btnAurrekoOrria.BackColor = Color.FromArgb(52, 73, 94); // Kolore korporatiboa
    _btnAurrekoOrria.ForeColor = Color.White;
    _btnAurrekoOrria.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
    
    // Botoiari klik gertaera (Event) lotzea
    _btnAurrekoOrria.Click += BtnAurrekoOrria_Click;
}

public void KonfiguratuGertaerak()
{
    // C#-ren ekintza entzuleak (Listeners)
    txtBilatu.TextChanged += TxtBilatu_TextChanged;
    chkPazienteGuztiak.CheckedChanged += PazienteMotaFiltroa_CheckedChanged;
    btnPazienteBerria.Click += BtnPazienteBerria_Click;
}
```
**Azalpena**: 
Kode bidezko diseinu honek esker UI-a edozein unetan alda daiteke logikaren arabera. `+=` sintaxiarekin, erabiltzailearen ekintzak (`Click`, `TextChanged`) zuzenean backend-eko metodoekin harremantzen dira. `FlatStyle.Flat` bezalako propietateek Windows form tradizionalen itxura "zaharra" kendu eta diseinu laua eta gaurkotua ematen diote aplikazioari.
