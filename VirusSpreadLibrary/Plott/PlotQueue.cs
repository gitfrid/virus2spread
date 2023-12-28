
using System.Collections.Concurrent;
using System.Windows.Forms;

namespace VirusSpreadLibrary.Plott;

public class PlotQueue
{
    // with a list of ten random Y-double values to  transfer between forms

    // create a FIFO threadsave ConcurrentQueue
    // to save and exchange doubles list to plot ten lines on PlotForm
    readonly private ConcurrentQueue<List<long>> queue1 = new();

    public void EnqueueList(List<long> values)
    {
        // add a doubles list to queue
        queue1.Enqueue(values);
    }
    public bool TryDequeueList(out List<long> values) =>
        // remove a doubles list from queue
        queue1.TryDequeue(result: out values!);
}
