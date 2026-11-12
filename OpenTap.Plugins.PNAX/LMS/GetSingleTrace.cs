using ChuckPlugins.TraceUtilities;
using OpenTap;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace OpenTap.Plugins.PNAX.LMS
{
    [Display("Get Single Trace", Groups: [ "Network Analyzer", "Load/Measure/Store" ], Description: "Reads a single trace from the PNA. The data is not stored or published yet.")]
    public class GetSingleTrace : TestStep, ITraceGenerator
    {
        #region Settings

        [Display("PNA", Order: 0.1)]
        public PNAX PNAX { get; set; }

        [Display("Channel", Description: "Channel to read the trace from.", Group: "Trace", Order: 1)]
        public int Channel { get; set; }

        [Display("MNum", Description: "Measurement number of the trace.", Group: "Trace", Order: 2)]
        public int MNum { get; set; }

        #endregion

        [Browsable(false)]
        [XmlIgnore]
        public Trace GeneratedTrace { get; set; }

        public GetSingleTrace()
        {
            Channel = 1;
            MNum = 1;
        }

        public override void Run()
        {
            UpgradeVerdict(Verdict.NotSet);
            GeneratedTrace = null;

            GeneratedTrace = PNAX.GetTrace(Channel, MNum);

            UpgradeVerdict(Verdict.Pass);
        }
    }
}
