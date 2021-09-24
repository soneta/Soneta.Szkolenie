using Soneta.Business;
using Soneta.Test;
using Soneta.Types;
using Soneta.CRM;

namespace Soneta.Szkolenie.Tests
{
    static class KontrahentAssembler
    {
        internal static IRowBuilder<Kontrahent> Kod(this IRowBuilder<Kontrahent> builder, string value)
           => builder.Enqueue(k => k.Kod = value);

        internal static IRowBuilder<Kontrahent> Nazwa(this IRowBuilder<Kontrahent> builder, string value)
           => builder.Enqueue(k => k.Nazwa = value);

        internal static IRowBuilder<Kontrahent> Rabat(this IRowBuilder<Kontrahent> builder, string value)
           => builder.Enqueue(k => k.RabatTowaru = Percent.Parse(value));

    }
}
