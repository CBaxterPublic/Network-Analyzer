using OpenTap;
using System.Collections.Generic;
using System.Linq;

namespace OpenTap.Plugins.PNAX.LMS
{
    [Display("Get Single Trace", Groups: new[] { "Network Analyzer", "Load/Measure/Store" }, Description: "Reads a single trace from the PNA. The data is not stored or published yet.")]
    public class GetSingleTrace : TestStep
    {
        #region Settings
        [Display("PNA", Order: 0.1)]
        public PNAX PNAX { get; set; }

        [Display("Channel", Description: "Channel to read the trace from.", Group: "Trace", Order: 1)]
        public int Channel { get; set; }

        [Display("MNum", Description: "Measurement number of the trace.", Group: "Trace", Order: 2)]
        public int MNum { get; set; }
        #endregion

        public GetSingleTrace()
        {
            Channel = 1;
            MNum = 1;
        }

        public override void Run()
        {
            UpgradeVerdict(Verdict.NotSet);

            // Index 0 is the X values, index 1 is the Y values (two values per point for complex formats).
            List<List<string>> data = PNAX.StoreTraceData(Channel, MNum);
            PNAX.WaitForOperationComplete();

            double[] x = data[0].Select(double.Parse).ToArray();
            double[] y = data[1].Select(double.Parse).ToArray();

            // Intentionally does nothing further with the trace for now.
            Log.Info($"Read trace from channel {Channel}, mnum {MNum}: {x.Length} X values, {y.Length} Y values.");

            UpgradeVerdict(Verdict.Pass);
        }
    }
}
