using Soneta.Business;
using Soneta.Test;

namespace Soneta.Szkolenie.Tests
{
    static class MaszynaAssembler
    {
        internal static IRowBuilder<Maszyna> NrBoczny(this IRowBuilder<Maszyna> builder, string value)
           => builder.Enqueue(m => m.NrBoczny = value);

        internal static IRowBuilder<Maszyna> Producent(this IRowBuilder<Maszyna> builder, string value)
           => builder.Enqueue(m => m.Producent = value);

        internal static IRowBuilder<Maszyna> Model(this IRowBuilder<Maszyna> builder, string value)
           => builder.Enqueue(m => m.Model = value);

        internal static IRowBuilder<Maszyna> DataProdukcji(this IRowBuilder<Maszyna> builder, string value)
           => builder.Enqueue(m => m.DataProd = Types.Date.Parse(value));
    }
}
