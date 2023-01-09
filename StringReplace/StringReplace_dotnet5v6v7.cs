using System.Net.Http;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;

namespace StringReplace
{
    [Config(typeof(Config))]
    [SimpleJob(RuntimeMoniker.Net50, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [HideColumns(Column.Job, Column.RatioSD, Column.AllocRatio)]
    public class StringReplaceBenchmarks
    {
        private string _str = string.Empty;

        [GlobalSetup]
        public async Task Setup()
        {
            using var hc = new HttpClient();
            _str = await
                hc.GetStringAsync("https://www.gutenberg.org/cache/epub/3200/pg3200.txt");

            // The Entire Project Gutenberg Works of Mark Twain
        }

        [Benchmark]
        public string Yell() => _str.Replace(".", "!");

        [Benchmark]
        public string ConcatLines() => _str.Replace("\n", "");

        [Benchmark]
        public string NormalizeEndings() => _str.Replace("\r\n", "\n");

        [Benchmark]
        public string Ireland() => _str.Replace("Ireland", "IRELAND"); //19 occurances

        [Benchmark]
        public string England() => _str.Replace("England", "ENGLAND"); //638 occurances

        private class Config : ManualConfig
        {
            public Config()
            {
                SummaryStyle =
                    SummaryStyle.Default.WithRatioStyle(RatioStyle.Percentage);
            }
        }
    }
}
