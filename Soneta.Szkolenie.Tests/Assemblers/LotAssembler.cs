using Soneta.Business;
using Soneta.Test;
using Soneta.Types;

namespace Soneta.Szkolenie.Tests
{
    static class LotAssembler
    {
        internal static IRowBuilder<Lot> KodUslugi(this IRowBuilder<Lot> builder, string value)
           => builder.Enqueue(l => l.KodUslugi = value);

        internal static IRowBuilder<Lot> Nazwa(this IRowBuilder<Lot> builder, string value)
           => builder.Enqueue(l => l.Nazwa = value);

        internal static IRowBuilder<Lot> LokalizacjaMiejscowosc(this IRowBuilder<Lot> builder, string value)
           => builder.Enqueue(l => l.LokalizacjaMiejscowosc = value);

        internal static IRowBuilder<Lot> Cena(this IRowBuilder<Lot> builder, Currency value )
           => builder.Enqueue(l => l.Cena= value);
    }
}
