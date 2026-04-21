# 4.2. GOsasun App-aren Inplementazioa

Mahaigaineko aplikazioaren garapenean, diseinu fasean zehaztutako **MVC (Model-View-Controller)** arkitektura zintzoki jarraitu dugu C# eta .NET 10 erabiliz. Inplementazio fase honen ardura izan da diseinatutako klase-diagramak hartu eta erabilgarri den logika bat garatzea, datu-basearekin eta hardwarearekin (BM58) sinkronizatuz.

## MVC Arkitekturaren Defentsa Inplementazioan
Aplikazioak milaka lerroko kodea badu ere, inplementazioa oso garbia da MVC-ri esker. Interfaze grafikoak (View) ez du inoiz SQL kontsultarik egiten; beti kontrolatzailera (Controller) deitzen du, eta honek Repositoriora (Model/DB).

**Kode pantailazoa: MVC eredua martxan (`PazienteenZerrenda.cs` -> `ErabiltzaileKontrolatzailea`)**
```csharp
// 1. VIEW (Interfazea): Erabiltzaileak "Paziente Berria" botoia sakatzen du
private void BtnPazienteBerria_Click(object sender, EventArgs e)
{
    // View-ak UI datuak biltzen ditu, baina ez ditu datu-basean zuzenean sartzen
    Pazientea pazienteBerria = new Pazientea {
        Nan = txtNan.Text,
        Izena = txtIzena.Text,
        Sexua = cmbSexua.SelectedItem.ToString()
    };
    
    // 2. CONTROLLER-ari deitzen zaio logika exekutatzeko
    bool arrakasta = _pazienteKontrolatzailea.SortuPazientea(pazienteBerria);
    
    if(arrakasta) {
        MessageBox.Show("Pazientea ongi gorde da.");
        KargatuPazienteakTaulara();
    }
}
```

**Kode pantailazoa: DB Repositorioaren exekuzioa (`PazienteaDB.cs`)**
```csharp
// 3. MODEL/DB (Repositorioa): Kontrolatzaileak deituta, SQL-a exekutatzen da era seguruan
public bool SortuPazientea(Pazientea p)
{
    using (MySqlConnection conn = Konexioa.LortuKonexioa())
    {
        // Parameterized query erabiliz inplementatu dugu, SQL Injection saihesteko
        string query = @"INSERT INTO pacienteak (paciente_id, sexua, odol_taldea) 
                         VALUES (@id, @sexua, @odolTaldea)";
                         
        MySqlCommand cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@id", p.Id);
        cmd.Parameters.AddWithValue("@sexua", p.Sexua);
        cmd.Parameters.AddWithValue("@odolTaldea", p.OdolTaldea);
        
        return cmd.ExecuteNonQuery() > 0;
    }
}
```
**Azalpena**: Inplementazio bikoitz honek diseinua defendatzen du: formuletako kodea arina da eta logika gogorra klase espezializatuetan isolatuta dago. Honek mantentze-lanak asko erraztuko ditu etorkizunean (adibidez, SQL taula aldatuko balitz, View-a ez litzateke ukitu behar).

## Hardwarearen Inplementazioa: Tentsiometroa
Diseinuko erronka nagusienetako bat BM58 tentsiometroaren inportazioa zen. C# barruan `HidLibrary` bezalako teknologiak erabili ditugu USB protokoloa aztertzeko eta byte gordinak osasun-datu (tentsioa, pultsua) bihurtzeko `BM58Driver` klasearen bidez. Hau da GOsasun aplikazioaren inplementazio esanguratsuena berrikuntza aldetik.
