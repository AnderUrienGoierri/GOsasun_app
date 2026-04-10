namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Errezeta eta botika baten arteko lotura (dosia eta maiztasuna) biltzen duen klasea.
    /// 'errezeta_botikak' taulari dagokio.
    /// </summary>
    public class ErrezetaBotika
    {
        public int LoturaId { get; set; }
        public int ErrezetaId { get; set; }
        public int BotikaId { get; set; }
        public string? Dosia { get; set; }
        public string? Maiztasuna { get; set; }
        
        public string? BotikaIzena { get; set; }

        public ErrezetaBotika() { }

        public ErrezetaBotika(  int loturaId, int errezetaId, int botikaId,
                                string? dosia, string? maiztasuna)
        {
            LoturaId = loturaId;
            ErrezetaId = errezetaId;
            BotikaId = botikaId;
            Dosia = dosia;
            Maiztasuna = maiztasuna;
        }
    }
}
