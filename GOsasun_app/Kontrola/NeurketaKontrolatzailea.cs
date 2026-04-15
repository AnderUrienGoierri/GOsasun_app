using System.Collections.Generic;
using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;
using System.IO;
using System.Xml.Linq;
using System;

namespace GOsasun_app.Kontrola
{
    /// <summary>
    /// Neurketen kudeaketarako kontrolatzailea.
    /// </summary>
    public class NeurketaKontrolatzailea
    {
        private readonly NeurketaDB _db = new NeurketaDB();

        /// <summary>
        /// Paziente baten neurketa guztien zerrenda lortzen du.
        /// </summary>
        public List<Neurketa> LortuPazientearenNeurketak(int pazienteId)
        {
            return _db.LortuPazientearenNeurketak(pazienteId);
        }

        /// <summary>
        /// Neurketa berri bat gordetzen du datu-basean.
        /// </summary>
        public bool GordeNeurketa(Neurketa neurketa)
        {
            return _db.GordeNeurketa(neurketa);
        }

        /// <summary>
        /// Neurketa baten datuak XML formatuan esportatzen ditu web zerbitzariaren karpetara.
        /// </summary>
        public void EsportatuXML(Neurketa n)
        {
            try
            {
                var neurketaNode = new XElement("Neurketa",
                    new XElement("erregistro_data", n.ErregistroData.ToString("yyyy-MM-dd HH:mm:ss")),
                    new XElement("paziente_id", n.PazienteId)
                );

                if (n.TentsioSistolikoa.HasValue) neurketaNode.Add(new XElement("tentsio_sistolikoa", n.TentsioSistolikoa.Value));
                if (n.TentsioDiastolikoa.HasValue) neurketaNode.Add(new XElement("tentsio_diastolikoa", n.TentsioDiastolikoa.Value));
                if (n.PultsuaPpm.HasValue) neurketaNode.Add(new XElement("pultsua_ppm", n.PultsuaPpm.Value));
                if (n.PisuaKg.HasValue) neurketaNode.Add(new XElement("pisua_kg", n.PisuaKg.Value));
                if (n.Altuera.HasValue) neurketaNode.Add(new XElement("altuera", n.Altuera.Value));

                XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Neurketak", neurketaNode));
                
                string path = @"C:\Apache24-64\htdocs\GOsasun_web\xml_paziente_neurketak";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                
                string izena = $"NEURKETA_{n.PazienteId}_{n.ErregistroData:yyyyMMdd_HHmmss}.xml";
                doc.Save(Path.Combine(path, izena));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"XML esportazioan errorea: {ex.Message}");
            }
        }
    }
}
