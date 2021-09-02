using NUnit.Framework;
using Soneta.Szkolenie.UI;
using Soneta.Test;
using Soneta.Types;

namespace Soneta.Szkolenie.Tests
{
    public class UstawRabatWorkerTests : DbTransactionTestBase
    {
        public override void TestSetup()
        {
            LoadAssembly("Soneta.Szkolenie");
            base.TestSetup();
        }

        [Test]
        public void UstawRabatWorkerTest()
        {
            var worker = Context.CreateObject(null, typeof(UstawRabatWorker),
                new UstawRabatWorkerParams(Context)
                {
                    Rabat = Percent.Parse("30%"),
                }) as UstawRabatWorker;

            Assert.DoesNotThrow(() => worker.UstawRabat(), "Wywołanie akcji \"Ustaw rabat\" spowodowało błąd.");
        }
    }
}
