using System.Collections.Generic;
using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;

namespace GOsasun_app.Kontrola
{
    public class ErrezetaKontrolatzailea
    {
        private readonly ErrezetaDB _errezetaDb = new ErrezetaDB();

        public List<Errezeta> LortuErrezetaGuztiak(bool soilikAktiboak = true)
        {
            return _errezetaDb.LortuErrezetaGuztiak(soilikAktiboak);
        }

        public List<Errezeta> LortuOsasunLangilearenErrezetak(int langileId, bool soilikAktiboak = true)
        {
            return _errezetaDb.LortuOsasunLangilearenErrezetak(langileId, soilikAktiboak);
        }

        public List<Errezeta> LortuPazientearenErrezetak(int pazienteId, bool soilikAktiboak = true)
        {
            return _errezetaDb.LortuPazientearenErrezetak(pazienteId, soilikAktiboak);
        }

        public bool SortuErrezeta(Errezeta errezeta)
        {
            return _errezetaDb.SortuErrezeta(errezeta);
        }

        public bool EguneratuErrezeta(Errezeta errezeta)
        {
            return _errezetaDb.EguneratuErrezeta(errezeta);
        }

        public bool EzabatuErrezeta(int errezetaId)
        {
            return _errezetaDb.EzabatuErrezeta(errezetaId);
        }
    }
}