using System.Collections.Concurrent;

namespace GOsasun_app.Interfazea.Kontrolak
{
    internal static class IrudiCachea
    {
        private static readonly ConcurrentDictionary<string, Bitmap> BitmapCachea = new ConcurrentDictionary<string, Bitmap>(StringComparer.OrdinalIgnoreCase);

        public static Bitmap? LortuBitmapa(string? bidea)
        {
            if (string.IsNullOrWhiteSpace(bidea) || !File.Exists(bidea))
            {
                return null;
            }

            string cacheGakoa = Path.GetFullPath(bidea);
            if (BitmapCachea.TryGetValue(cacheGakoa, out Bitmap? cachekoIrudia))
            {
                return new Bitmap(cachekoIrudia);
            }

            using Image jatorrizkoa = Image.FromFile(cacheGakoa);
            Bitmap bitmapa = new Bitmap(jatorrizkoa);
            BitmapCachea.TryAdd(cacheGakoa, new Bitmap(bitmapa));
            return bitmapa;
        }
    }
}