using System.Collections.Generic;
using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;
using System.IO;
using System.Xml.Linq;
using System;

namespace GOsasun_app.Kontrola
{
    /// <summary>
    /// Jarraipenen (lehengo neurketak) kudeaketarako kontrolatzailea.
    /// </summary>
    public class JarraipenaKontrolatzailea
    {
        // ---------------------------SORTU OBJETUA------------------------------------------------------
        private readonly JarraipenaDB _db = new JarraipenaDB();
        private readonly DokumentuaDB _dokumentuaDb = new DokumentuaDB();

        // ---------------------------LORTU------------------------------------------------------

        /// <summary>
        /// Paziente baten jarraipen guztien zerrenda lortzen du.
        /// </summary>
        public List<Jarraipena> LortuPazientearenJarraipenak(int pazienteId)
        {
            return _db.LortuPazientearenJarraipenak(pazienteId);
        }

        public List<Jarraipena> LortuJarraipenGuztiak(string? bilaketa = null, DateTime? hasieraData = null, DateTime? amaieraData = null, int? pazienteId = null)
        {
            return _db.LortuJarraipenGuztiak(bilaketa, hasieraData, amaieraData, pazienteId);
        }

        public Jarraipena? LortuJarraipena(int jarraipenaId)
        {
            return _db.LortuJarraipena(jarraipenaId);
        }

        /// <summary>
        /// Jarraipen berri bat gordetzen du datu-basean.
        /// </summary>
        public bool GordeJarraipena(Jarraipena jarraipena)
        {
            return _db.GordeJarraipena(jarraipena);
        }

        public int? GordeJarraipenaEtaLortuId(Jarraipena jarraipena)
        {
            return _db.GordeJarraipenaEtaLortuId(jarraipena);
        }

        public bool EzabatuJarraipena(int jarraipenaId)
        {
            return _db.EzabatuJarraipena(jarraipenaId);
        }

        public List<Dokumentua> LortuJarraipenarenDokumentuak(int jarraipenaId)
        {
            return _dokumentuaDb.LortuJarraipenarenDokumentuak(jarraipenaId);
        }

        public bool GordeDokumentua(Dokumentua dokumentua)
        {
            return _dokumentuaDb.GordeDokumentua(dokumentua);
        }

        /// <summary>
        /// Jarraipen baten datuak XML formatuan esportatzen ditu web zerbitzariaren karpetara.
        /// </summary>
        public void EsportatuXML(Jarraipena n)
        {
            try
            {
                var jarraipenaNode = new XElement("Jarraipena",
                                    new XElement("erregistro_data", n.ErregistroData.ToString("yyyy-MM-dd HH:mm:ss")),
                                    new XElement("paziente_id", n.PazienteId)
                );

                if (n.TentsioSistolikoa.HasValue) jarraipenaNode.Add(new XElement("tentsio_sistolikoa", n.TentsioSistolikoa.Value));
                if (n.TentsioDiastolikoa.HasValue) jarraipenaNode.Add(new XElement("tentsio_diastolikoa", n.TentsioDiastolikoa.Value));
                if (n.PultsuaPpm.HasValue) jarraipenaNode.Add(new XElement("pultsua_ppm", n.PultsuaPpm.Value));
                if (n.PisuaKg.HasValue) jarraipenaNode.Add(new XElement("pisua_kg", n.PisuaKg.Value));
                if (n.Altuera.HasValue) jarraipenaNode.Add(new XElement("altuera", n.Altuera.Value));

                XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Jarraipenak", jarraipenaNode));

                string path = @"C:\Apache24-64\htdocs\GOsasun_web\xml_paziente_neurketak";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                string izena = $"JARRAIPENA_{n.PazienteId}_{n.ErregistroData:yyyyMMdd_HHmmss}.xml";
                doc.Save(Path.Combine(path, izena));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"XML esportazioan errorea: {ex.Message}");
            }
        }
    }
}
