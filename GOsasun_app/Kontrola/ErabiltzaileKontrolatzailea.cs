using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;
using GOsasun_app.Kontrola.Zerbitzuak;

namespace GOsasun_app.Kontrola
{
    /// <summary>
    /// Autentifikazio-kontrolatzailea eta bateragarritasun-fachada.
    /// Saio-hasiera hemen mantentzen da, eta gainerako erabiltzaile-eragiketak
    /// rol espezifikoko kontrolatzaileetara delegatzen dira pixkanakako migraziorako.
    /// </summary>
    public class ErabiltzaileKontrolatzailea
    {
        // ---------------------------SORTU OBJETUA------------------------------------------------------    
        private readonly ErabiltzaileDB _erabiltzaileDb = new ErabiltzaileDB();
        private readonly LoginBlokeoZerbitzua _loginBlokeoZerbitzua = new LoginBlokeoZerbitzua();

        // ---------------------------LORTU------------------------------------------------------        

        /// <summary>
        /// Erabiltzailea datu-basean egiaztatzen du email eta pasahitz bidez.
        /// </summary>
        public LoginEmaitza Login(string emaila, string pasahitza)
        {
            LoginSegurtasunEgoera unekoEgoera = _loginBlokeoZerbitzua.LortuEgoera();
            if (unekoEgoera.Blokeatuta)
            {
                return new LoginEmaitza { Egoera = unekoEgoera };
            }

            Erabiltzailea? erabiltzailea = _erabiltzaileDb.Login(emaila, pasahitza);
            if (erabiltzailea != null)
            {
                _loginBlokeoZerbitzua.Berrezarri();
                return new LoginEmaitza
                {
                    Erabiltzailea = erabiltzailea,
                    Egoera = _loginBlokeoZerbitzua.LortuEgoera()
                };
            }

            return new LoginEmaitza
            {
                Egoera = _loginBlokeoZerbitzua.ErregistratuHutsegitea()
            };
        }

        public LoginSegurtasunEgoera LortuLoginBlokeoEgoera()
        {
            return _loginBlokeoZerbitzua.LortuEgoera();
        }
    }
}
